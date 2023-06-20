namespace DiamondKata.Console.Services;

/// <inheritdoc cref="IDiamondGenerator"/>
public class DiamondGenerator : IDiamondGenerator
{
    /// <summary>
    /// English alphabet
    /// </summary>
    public static IList<char> Alphabet => new[]
    {
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
    };

    /// <summary>
    /// Char used for padding.
    /// </summary>
    private static char PaddingSeparator => '_';

    public string Generate(char targetLetter)
    {
        if (targetLetter == 'A')
        {
            return "A";
        }

        var targetLetterIndex = Alphabet.IndexOf(targetLetter);
        if (targetLetterIndex < 0) // sanity check - should be covered in higher layer, but just in case it wasn't - we check that here as well
        {
            throw new InvalidOperationException("Provided char is not a letter in an English alphabet. Please provide a valid letter");
        }

        var diamondLines = new List<string>(targetLetterIndex * 2 + 1); // specify initial capacity (improved memory consumption)
        var sidePadding = targetLetterIndex;
        var midPadding = 1;

        // first row
        diamondLines.Add(GetLine(0, midPadding, sidePadding));
        sidePadding--;

        // top half with mid row
        for (int i = 1; i <= targetLetterIndex; i++)
        {
            diamondLines.Add(GetLine(i, midPadding, sidePadding));
            midPadding += 2;
            sidePadding--;
        }

        // bottom half
        for (int i = diamondLines.Count - 2; i >= 0; i--) // ('Count - 2', because we don't want to re-add mid-row and indexing starts at 0)
        {
            diamondLines.Add(diamondLines[i]); // add already existing string, since it's the same
        }

        return string.Join(Environment.NewLine, diamondLines); // internally, string.Join uses ValueStringBuilder - "improved" version of StringBuilder, which stores data on stack, instead of on the heap
    }

    private static string GetLine(int index, int midPadding, int sidePadding)
    {
        if (index == 0) // could be extracted to different method, to not do this 'if' in every loop, but it's more readable this way imo
        {
            return $"{new string(PaddingSeparator, sidePadding)}{Alphabet[index]}{new string(PaddingSeparator, sidePadding)}";
        }
        return $"{new string(PaddingSeparator, sidePadding)}{Alphabet[index]}{new string(PaddingSeparator, midPadding)}{Alphabet[index]}{new string(PaddingSeparator, sidePadding)}";
    }
}