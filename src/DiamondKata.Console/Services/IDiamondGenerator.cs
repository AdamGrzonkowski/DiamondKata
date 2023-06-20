namespace DiamondKata.Console.Services;

/// <summary>
/// Responsible for generating diamonds.
/// </summary>
public interface IDiamondGenerator
{
    /// <summary>
    /// Draws diamond shape on the screen.
    /// </summary>
    /// <param name="targetLetter">The "biggest" letter to appear in diamond</param>
    string Generate(char targetLetter);
}