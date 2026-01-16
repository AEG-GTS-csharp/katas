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
            string content = file.ReadToEnd();
            return ParseWords(content);
        }

        public static List<string> ParseWords(string content)
        {
            List<string> words = [];
            int i = 0;
            while (i < content.Length)
            {
                while (i < content.Length && char.IsWhiteSpace(content[i]))
                    i++;
                if (i >= content.Length)
                    break;
                int wordStart = i;
                while (i < content.Length && !char.IsWhiteSpace(content[i]))
                    i++;
                words.Add(content[wordStart..i]);
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