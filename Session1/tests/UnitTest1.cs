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

    public void ThrowsOutOfRangeEx()
    {
        string[] gameResult = new string[99];

        Assert.Throws<ArgumentException>(() => App.CalculatePoints(gameResult));
    }
}
