
namespace BankOCR.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ocrChars = OCRChar.CharDefinitions();
            var entries = ComputeFile("entries.txt", ocrChars);
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        public static string ComputeEntry(string[] entry, OCRChar[] charDefinitions)
        {
            char[] result = new char[entry[0].Length / 3];
            for (int i = 0; i < result.Length; i++)
            {
                var charRepresentation = OCRChar.GetCharReprAtIndex(entry, i);
                var ocrChar = charDefinitions.First(ocrChar => ocrChar.ReprEquals(charRepresentation));
                result[i] = ocrChar.Value;
            }
            return new string(result);
        }

        public static string[] ComputeFile(string filename, OCRChar[] charDefinitions)
        {
            string[] lines = File.ReadAllLines(filename);

            string[] result = new string[lines.Length / 4];
            for (int i = 0; i < result.Length; i++)
            {
                string[] entry = lines
                    .Skip(4 * i)
                    .Take(3)
                    .ToArray();

                result[i] = ComputeEntry(entry, charDefinitions);
            }
            return result;
        }
    }
}