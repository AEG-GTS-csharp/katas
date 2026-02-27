using BankOCR.App;

namespace BankOCR.UnitTest
{
    public class ProgramTests
    {
        private const string FileName = "entries.txt";

        [Test]
        public void LinesContain27Chars()
        {
            var lines = File.ReadAllLines(FileName);
            Assert.That(lines.All(line => line.Length == 27));
        }

        [Test]
        public void LineCountIsMultipleOf4()
        {
            var lines = File.ReadAllLines(FileName);
            Assert.That(lines.Length % 4 == 0);
        }

        [Test]
        [TestCase((string[])[
            " _  _  _     _  _  _  _  _ ",
            "  ||_| _|  | _|| || |  ||_|",
            "  ||_| _|  | _||_||_|  | _|"
            ], "783130079")]
        [TestCase((string[])[
            " _  _  _     _  _  _       ",
            "|_||_||_|  || | _|  ||_||_|",
            "|_||_||_|  ||_| _|  |  |  |"
            ], "888103744")]
        [TestCase((string[])[
            "    _  _  _  _  _  _  _  _ ",
            "  ||_ |_ |_| _| _| _|  ||_ ",
            "  | _||_| _||_ |_  _|  ||_|"
            ], "156922376")]
        public void ComputeEntryReturnsStringReprOfEntry(string[] entry, string representation)
        {
            var ocrChars = OCRChar.CharDefinitions();
            Assert.That(Program.ComputeEntry(entry, ocrChars) == representation);
        }

        [Test]
        public void ComputeFileReturnsStringsOfLength9()
        {
            var ocrChars = OCRChar.CharDefinitions();
            var entries = Program.ComputeFile(FileName, ocrChars);
            Assert.That(entries.All(entry => entry.Length == 9));
        }
    }
}
