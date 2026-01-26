using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR.App
{
    public record OCRChar(string[] Representation, char Value)
    {
        public static OCRChar[] CharDefinitions()
        {
            string[] chars = [
                " _     _  _     _  _  _  _  _ ",
                "| |  | _| _||_||_ |_   ||_||_|",
                "|_|  ||_  _|  | _||_|  ||_| _|"
            ];

            var ocrChars = new OCRChar[10];
            for (int i = 0; i < ocrChars.Length; i++)
            {
                var charRepresentation = GetCharReprAtIndex(chars, i);
                ocrChars[i] = new OCRChar(charRepresentation, i.ToString()[0]);
            }
            return ocrChars;
        }

        public static string[] GetCharReprAtIndex(string[] entry, int index)
        {
            var charRepresentation = new string[entry.Length];
            for (int j = 0; j < entry.Length; j++)
            {
                charRepresentation[j] = entry[j].Substring(3 * index, 3);
            }
            return charRepresentation;
        }

        public bool ReprEquals(string[] otherRepresentation)
        {
            return Representation.SequenceEqual(otherRepresentation);
        }
    }
}
