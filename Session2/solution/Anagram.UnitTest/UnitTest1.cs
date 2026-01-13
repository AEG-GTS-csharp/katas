using Anagram.App;

namespace Anagram.UnitTest
{
    public class UnitTest1
    {
        [Test]
        public void ReturnAllWordsLongerThanZero()
        {
            Assert.That(Program.LoadWordList().All(word => word.Length > 0));
        }

        [Test]
        public void ReturnAllWordsNotIncludingWhitespace()
        {
            Assert.That(Program.LoadWordList().All(word => !word.Any(char.IsWhiteSpace)));
        }

        [Test]
        [TestCase("master", "stream")]
        [TestCase("letters", "settler")]
        [TestCase("turner", "return")]
        public void ReturnTrueIfWordsAreAnagrams(string firstWord, string secondWord)
        {
            Assert.That(Program.AreAnagrams(firstWord, secondWord));
        }

        [Test]
        [TestCase("master", "turner")]
        [TestCase("letters", "example")]
        [TestCase("they", "return")]
        public void ReturnFalseIfWordsAreNotAnagrams(string firstWord, string secondWord)
        {
            Assert.That(!Program.AreAnagrams(firstWord, secondWord));
        }

        [Test]
        public void CheckAnagramsForMegacity()
        {
            var anagrams = Program.FindTwoWordAnagramsOf("megacity");
            Assert.That(
                (anagrams.Contains(("mega", "city")) || anagrams.Contains(("city", "mega"))) && 
                (anagrams.Contains(("game", "city")) || anagrams.Contains(("city", "game")))
            );
        }
    }
}
