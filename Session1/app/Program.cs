namespace App;

public class App
{
    public static void Main(string[] args)
    {
    }

    public static int CalculatePoints()
    {
        return 0;
    }

    public static int CalculatePoints(string[] gameResult)
    {
        if (gameResult.Length != 10)
        {
            throw new ArgumentException(nameof(gameResult));
        }
        return 0;
    }

    public static void CheckFormat(String gameResult)
    {
        if (gameResult == String.Empty)
        {
            throw new FormatException();
        }
    }
}