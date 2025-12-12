// HEHEHEHA
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

    public static int CalculatePoints(String[] gameResult)
    {
        if (gameResult.Length != 10)
        {
            throw new ArgumentException(nameof(gameResult));
        }
        foreach (string s in gameResult)
        {
            CheckFormat(s);
        }
        return 0;

    }

    public static void CheckFormat(String gameResult)
    {
        if (string.IsNullOrWhiteSpace(gameResult) || gameResult.Length > 3)
        {
            throw new FormatException();
        }
        int colons = 0;
        for (int i = 0; i < gameResult.Length; i++)
        {
            if (gameResult[i] == ':')
            {
                if (i != 1)
                {
                    throw new FormatException();
                }
                colons++;
            }
            else if (gameResult[i] > '4' || gameResult[i] < '1')
            {
                throw new FormatException();
            }
        }
        if (colons != 1) throw new FormatException();
    }
}