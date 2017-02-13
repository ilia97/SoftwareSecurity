using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTask.Services
{
    class EncryptionService
    {
        private readonly Dictionary<char, string> EncodedLettersTable = new Dictionary<char, string>()
        {
            { 'A', "11011" },
            { 'B', "10110" },
            { 'C', "10010" },
            { 'D', "10000" },
            { 'É', "11001" },
            { 'E', "11101" },
            { 'F', "10100" },
            { 'G', "10101" },
            { 'H', "10001" },
            { 'I', "11100" },
            { 'J', "10011" },
            { 'K', "00011" },
            { 'L', "00001" },
            { 'M', "00101" },
            { 'N', "00100" },
            { 'O', "11000" },
            { 'P', "00000" },
            { 'Q', "00010" },
            { 'R', "00110" },
            { 'S', "01110" },
            { 'T', "01010" },
            { 'U', "11010" },
            { 'V', "01000" },
            { 'W', "01100" },
            { 'X', "01101" },
            { 'Y', "11110" },
            { 'Z', "01001" },
            { '-', "01011" },
            { '1', "01111" },
            { '2', "10111" },
            { '3', "00111" },
            { '4', "11111" },
        };

        public string Encrypt(string text, string key)
        {
            var binaryText = TranslateTextToBodo(text);
            var binaryKey = TranslateTextToBodo(key);

            var encryptedBinary = new StringBuilder("");

            for (int i = 0; i < binaryText.Length; i++)
            {
                encryptedBinary.Append(Convert.ToInt32(binaryText[i]) ^ Convert.ToInt32(binaryKey[i]));
            }

            return TranslateTextFromBodo(encryptedBinary.ToString());
        }

        private string TranslateTextToBodo(string text)
        {
            var textBuilder = new StringBuilder("");

            foreach (char c in text)
            {
                if (this.EncodedLettersTable.ContainsKey(c))
                {
                    textBuilder.Append(this.EncodedLettersTable[c]);
                }
            }

            return textBuilder.ToString();
        }

        public bool XOR (bool a, bool b)
        {
            return (a || b) && (!a || !b);
        }

        private string TranslateTextFromBodo(string text)
        {
            var textBuilder = new StringBuilder("");
            var binaryBuilder = new StringBuilder("");

            for (int i = 0; i < text.Length; i++)
            {
                if (i > 0 && i % 5 == 0)
                {
                    var letter = this.EncodedLettersTable.First(x => x.Value == binaryBuilder.ToString()).Key;
                    textBuilder.Append(letter);

                    binaryBuilder = new StringBuilder("");
                }

                binaryBuilder.Append(text[i]);
            }

            return textBuilder.ToString();
        }
    }
}
