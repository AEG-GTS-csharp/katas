namespace Anagram.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string word = "documenting";
            foreach (var (firstWord, secondWord) in FindTwoWordAnagramsOf(word))
            {
                Console.WriteLine($"'{firstWord}' and '{secondWord}' are two-word anagrams of '{word}'.");
            }
        }

        public static List<string> LoadWordList()
        {
            using var file = File.OpenText("words.txt");

            List<string> words = [];
            while (true)
            {
                string? line = file.ReadLine();
                if (line is null)
                    break;

                const int maxWordLength = 9;
                for (int i = 0; i < line.Length / maxWordLength; i++)
                {
                    string word = line.Substring(i * maxWordLength, maxWordLength).TrimEnd();
                    words.Add(word);
                }
            }

            return words;
        }

        public static bool AreAnagrams(string firstWord, string secondWord)
        {
            char[] firstWordChars = firstWord.ToCharArray();
            Array.Sort(firstWordChars);
            char[] secondWordChars = secondWord.ToCharArray();
            Array.Sort(secondWordChars);

            return firstWordChars.SequenceEqual(secondWordChars);
        }

        public static List<(string, string)> FindTwoWordAnagramsOf(string word)
        {
            var wordList = LoadWordList();

            List<(string, string)> anagrams = [];
            foreach (string firstWord in wordList)
            {
                foreach (string secondWord in wordList)
                {
                    if (AreAnagrams(word, firstWord + secondWord))
                    {
                        anagrams.Add((firstWord, secondWord));
                    }
                }
            }

            return anagrams;
        }
    }
}