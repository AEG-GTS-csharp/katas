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
    public void CheckFormat_IsResultLongerThan3()
    {
        Assert.Throws<FormatException>(() => App.CheckFormat("4:4:"));
    }

    [Test]
    [TestCase("4::")]
    [TestCase(":::")]
    [TestCase("::4")]
    [TestCase("444")]
    public void CheckFormat_ThrowsFormatExceptionIfNotExactlyOneColon(string result)
    {
        Assert.Throws<FormatException>(() => App.CheckFormat(result));
    }

    [Test]
    [TestCase(":44")]
    [TestCase("44:")]
    public void CheckFormat_ThrowsFormatExceptionIfColonMislocated(string result)
    {
        Assert.Throws<FormatException>(() => App.CheckFormat(result));
    }

    [Test]
    [TestCase("5:5")]
    [TestCase("6:6")]
    public void CheckFormat_ThrowsFormatExceptionIsScoreGreaterThanFour(string result)
    {
        Assert.Throws<FormatException>(() => App.CheckFormat(result));
    }

    [Test]
    public void CalculatePointsChecksFormat()
    {
        Assert.Throws<FormatException>(() => App.CalculatePoints(["HEHEHEHA", "LOLa", "xD", "Tituuuus!", "Tituuuus!", "Tituuuus!", "Tituuuus!", "Tituuuus!", "Tituuuus!", "Tituuuus!",]));
    }
}
