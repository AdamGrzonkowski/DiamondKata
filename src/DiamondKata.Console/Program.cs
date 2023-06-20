using DiamondKata.Console.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiamondKata.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        // add Generic Host for DI support
        using var host = Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<IDiamondGenerator, DiamondGenerator>();
            })
            .UseConsoleLifetime()
            .Build();

        var letter = GetValidatedInput(args);
        DrawDiamond(host.Services, letter);
    }

    private static char GetValidatedInput(string[] args)
    {
        if (args.Length != 1)
        {
            throw new ArgumentNullException(nameof(args), "Input, in form of a single, uppercase letter, is required; for example: C");
        }

        var letter = char.Parse(args[0]); // ensures one-character input was provided, otherwise it's gonna throw FormatException

        if (!char.IsAsciiLetterUpper(letter)) // we use A-Z alphabet, so ASCII can be used, as it covers it
        {
            throw new ArgumentException($"Provided character '{letter}' is not an uppercase letter.", nameof(args));
        }

        return letter;
    }

    private static void DrawDiamond(IServiceProvider serviceProvider, char letter)
    {
        using var scope = serviceProvider.CreateScope();
        var diamondGenerator = scope.ServiceProvider.GetRequiredService<IDiamondGenerator>();

        var diamondString = diamondGenerator.Generate(letter);

        System.Console.WriteLine(diamondString);
    }
}