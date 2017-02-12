using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask.Services
{
    class CalculationsService
    {
        public const int roundDecimalsCount = 2;

        public CharactersFrequencyModel CalculateCharactersFrequency(string text)
        {
            CharactersFrequencyModel model = new CharactersFrequencyModel();

            foreach (var character in text)
            {
                if (char.IsLetter(character))
                {
                    if (!model.CharactersCount.ContainsKey(character))
                    {
                        model.CharactersCount[character] = 0;
                    }

                    model.CharactersCount[character]++;
                    model.TotalCharactersCount++;
                }
            }

            foreach (var letter in model.CharactersCount.Keys)
            {
                model.CharactersFrequency[letter] = Math.Round((model.CharactersCount[letter] / model.TotalCharactersCount * 100), roundDecimalsCount);
            }

            model.CharactersCount.OrderBy(x => x.Key);
            model.CharactersFrequency.OrderBy(x => x.Key);

            return model;
        }
    }
}
