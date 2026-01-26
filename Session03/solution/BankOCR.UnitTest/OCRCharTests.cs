using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOCR.App;

namespace BankOCR.UnitTest
{
    public class OCRCharTests
    {
        [Test]
        public void CharDefinitionsReturnsAllRepresentationsHaveThreeCharsPerLine()
        {
            var charDefinitions = OCRChar.CharDefinitions();
            Assert.That(charDefinitions
                .All(ocrChar => ocrChar.Representation
                    .All(line => line.Length == 3)));
        }

        [Test]
        public void CharDefinitionsReturnsAllRepresentationsHaveThreeLines()
        {
            var charDefinitions = OCRChar.CharDefinitions();
            Assert.That(charDefinitions
                .All(ocrChar => ocrChar.Representation.Length == 3));
        }
    }
}
