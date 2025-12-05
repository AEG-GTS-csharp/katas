namespace tests;

using App;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ReturnZeroPointsIfNoArrayGiven()
    {
        int result = App.CalculatePoints();
        Assert.That(result == 0);
    }

    [Test]
    public void TakesStringArray()
    {
        string[] gameResult = new string[10];
        int result = App.CalculatePoints(gameResult);
        Assert.That(result == 0);
    }

    [Test]
    public void ThrowsOutOfRangeExIfLengthIsGreater11()
    {
        Random random = new Random();
        var gameResult = new string[11];
        Assert.Throws<ArgumentException>(() => App.CalculatePoints(gameResult));
    }


    [Test]
    public void ThrowsOutOfRangeExIfLengthIsLower10()
    {
        Random random = new Random();
        var gameResult = new string[9];
        Assert.Throws<ArgumentException>(() => App.CalculatePoints(gameResult));
    }

    [Test]
    public void CheckGameResultFormatExists()
    {
        App.CheckFormat("1:4"); // LOS TITUS!!!
    }

    [Test]
    public void CheckFormat_ThrowsFormatExceptionIfResultIsEmpty()
    {
        Assert.Throws<FormatException>(() => App.CheckFormat(string.Empty));
    }
    [Test]
    public void CheckFormat_IsResultGreaterThan3()
    {
        Assert.Throws<FormatException>(() => App.CheckFormat("4:4:"));
    }

}
