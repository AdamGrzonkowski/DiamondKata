using DiamondKata.Console.Services;

namespace DiamondKata.Console.UnitTests.Services;

public class DiamondGeneratorTests
{
    [Theory]
    [InlineData('1')]
    [InlineData('_')]
    [InlineData('!')]
    [InlineData('?')]
    [InlineData('a')]
    [InlineData('c')]
    public void Generate_ProvidedCharIsNotAnUppercaseLetter_ThrowsException(char targetChar)
    {
        // Arrange
        var sut = new DiamondGenerator();

        // Act
        var result = () => sut.Generate(targetChar);

        // Assert
        result.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData('B')]
    [InlineData('F')]
    public void Generate_ProvidedCharIsAnUppercaseLetter_RunsSuccessfully(char targetChar)
    {
        // Arrange
        var sut = new DiamondGenerator();

        // Act
        var result = sut.Generate(targetChar);

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Contain(targetChar.ToString().ToUpper());
    }

    [Fact]
    public void Generate_ProvidedCharIsAnUppercaseLetter_ReturnsDiamondCorrectly()
    {
        // Arrange
        var sut = new DiamondGenerator();
        var targetChar = 'E';
        var expectedString = $"____A____{Environment.NewLine}___B_B___{Environment.NewLine}__C___C__{Environment.NewLine}_D_____D_{Environment.NewLine}E_______E{Environment.NewLine}_D_____D_{Environment.NewLine}__C___C__{Environment.NewLine}___B_B___{Environment.NewLine}____A____";

        // Act
        var result = sut.Generate(targetChar);

        // Assert
        result.Should().BeEquivalentTo(expectedString);
    }

    [Fact]
    public void Alphabet_Always_ContainsAllEnglishUppercaseLettersInCorrectOrder()
    {
        // Arrange & Act
        var englishAlphabetUppercase = new List<char>(26);

        for (char c = 'A'; c <= 'Z'; c++)
        {
            englishAlphabetUppercase.Add(c);
        }

        // Assert
        DiamondGenerator.Alphabet.Count.Should().Be(englishAlphabetUppercase.Count);
        DiamondGenerator.Alphabet.Should().ContainInConsecutiveOrder(englishAlphabetUppercase);
    }
}