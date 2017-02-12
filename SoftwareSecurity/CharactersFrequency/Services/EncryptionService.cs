using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask.Services
{
    class EncryptionService
    {
        private readonly List<char> Letters = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private readonly int Key = 3;

        private readonly CalculationsService calculationsService = new CalculationsService();

        public EncryptionService(int key = 0)
        {
            this.Key = key;
        }

        public string Encrypt(string text)
        {
            StringBuilder stringBuilder = new StringBuilder("");

            foreach (var character in text)
            {
                int currentPosition = this.Letters.IndexOf(character);

                if (currentPosition == -1)
                {
                    stringBuilder.Append(character);
                }
                else
                {
                    int newPosition = (currentPosition + this.Key) % this.Letters.Count;
                    stringBuilder.Append(this.Letters[newPosition]);
                }
            }

            return stringBuilder.ToString();
        }

        public string Decrypt(string text, int key)
        {
            StringBuilder stringBuilder = new StringBuilder("");

            foreach (var character in text)
            {
                int currentPosition = Letters.IndexOf(character);

                if (currentPosition == -1)
                {
                    stringBuilder.Append(character);
                }
                else
                {
                    int newPosition = (currentPosition - key + this.Letters.Count) % this.Letters.Count;
                    stringBuilder.Append(this.Letters[newPosition]);
                }
            }

            return stringBuilder.ToString();
        }

        public Dictionary<int, string> FindKey(CharactersFrequencyModel realCharactersFrequency, string encryptedText, int n)
        {
            var encryptedTextModel = this.calculationsService.CalculateCharactersFrequency(encryptedText);

            var iterationsCount = Math.Min(n, this.Letters.Count);

            var realTextLetters = realCharactersFrequency.CharactersCount.OrderByDescending(x => x.Value).Take(iterationsCount).Select(x => x.Key);
            var encryptedTextLetters = encryptedTextModel.CharactersCount.OrderByDescending(x => x.Value).Take(iterationsCount).Select(x => x.Key);

            Dictionary<int, int> keysFrequency = new Dictionary<int, int>();

            foreach (var realTextLetter in realTextLetters)
            {
                foreach (var encryptedTextLetter in encryptedTextLetters)
                {
                    var key = (this.Letters.IndexOf(encryptedTextLetter) - this.Letters.IndexOf(realTextLetter) + this.Letters.Count) % this.Letters.Count;

                    if (!keysFrequency.ContainsKey(key))
                    {
                        keysFrequency[key] = 1;
                    }
                    else
                    {
                        keysFrequency[key]++;
                    }
                }
            }

            var keys = keysFrequency.OrderByDescending(x => x.Value).Select(x => x.Key);

            var results = new Dictionary<int, string>();

            foreach (var key in keys)
            {
                var text = this.Decrypt(encryptedText, key);
                results.Add(key, text);
            }

            return results;
        }
    }
}
