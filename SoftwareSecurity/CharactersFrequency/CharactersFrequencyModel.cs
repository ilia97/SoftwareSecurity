using System.Collections.Generic;

namespace FirstTask
{
    class CharactersFrequencyModel
    {
        private readonly char[] Letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        public CharactersFrequencyModel()
        {
            this.CharactersCount = new Dictionary<char, float>();
            this.CharactersFrequency = new Dictionary<char, double>();

            foreach (var letter in this.Letters)
            {
                this.CharactersCount[letter] = 0;
                this.CharactersFrequency[letter] = 0;
            }
        }

        public Dictionary<char, float> CharactersCount { get; set; }

        public Dictionary<char, double> CharactersFrequency { get; set; }

        public float TotalCharactersCount { get; set; }
    }
}
