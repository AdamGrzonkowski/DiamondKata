namespace DiamondKata.Console.UnitTests;

public class ProgramTests
{
    [Theory]
    [MemberData(nameof(GetInvalidArgsTestData))]
    public void Main_InvalidArgs_ThrowsException(string providedArgs)
    {
        // Arrange & Act Arrange
        var sut = () => Program.Main(new string[] { providedArgs });

        // Assert
        sut.Should().Throw<Exception>();
    }

    [Theory]
    [InlineData("A")]
    [InlineData("C")]
    public void Main_CorrectArgs_RunSuccessfully(string providedArgs)
    {
        // Arrange & Act Arrange
        var sut = () => Program.Main(new string[] { providedArgs });

        // Assert
        sut.Should().NotThrow<Exception>();
    }

    public static IEnumerable<object[]> GetInvalidArgsTestData()
    {
        yield return new string[] { string.Empty };
        yield return new string[] { "  " };
        yield return new string[] { "AAA" };
        yield return new string[] { "A E B" };
        yield return new string[] { "123" };
        yield return new string[] { "3" };
        yield return new string[] { "b" };
    }
}