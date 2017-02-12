using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstTask.Services;

namespace FirstTask
{
    class Program
    {
        static CalculationsService calculationsService = new CalculationsService();
        static EncryptionService encryptionService = new EncryptionService(5);
        static object locker = new object();

        static void Main(string[] args)
        {
            Console.WriteLine("1.1 Посчитать статистику встречаемости букв в разных жанрах литературы (художественной, технической и Библии).");
            var artTask = Task.Run(() => GetCharactersFrequercy("Art Literature", "Books/Harry Potter And The Sorcerer's Stone.txt"));
            var technicalTask = Task.Run(() => GetCharactersFrequercy("Technical Literature", "Books/Algebraic Topology.txt"));
            var bibleTask = Task.Run(() => GetCharactersFrequercy("Bible", "Books/Bible.txt"));

            Task.WaitAll(artTask, technicalTask, bibleTask);
            Console.WriteLine("----------------------------------------------------------");

            Console.WriteLine("1.2 Реализовать шифр Цезаря.");
            var text = "The Dursleys had everything they wanted, but they also had a secret, and their greatest fear was that somebody would discover it.They didn't think they could bear it if anyone found out about the Potters.Mrs. Potter was Mrs.Dursley's sister, but they hadn't met for several years; in fact, Mrs.Dursley pretended she didn't have a sister, because her sister and her good -for-nothing husband were as unDursleyish as it was possible to be.The Dursleys shuddered to think what the neighbors would say if the Potters arrived in the street. The Dursleys knew that the";
            var encryptedText = encryptionService.Encrypt(text.ToLower());
            Console.WriteLine("Original text:");
            Console.WriteLine(text);
            Console.WriteLine();
            Console.WriteLine("Encrypted text:");
            Console.WriteLine(encryptedText);
            Console.WriteLine("----------------------------------------------------------");

            Console.WriteLine("1.3 Научиться расшифровывать шифр Цезаря.");
            var texts = encryptionService.FindKey(artTask.Result, encryptedText, 10);

            foreach (var t in texts)
            {
                Console.WriteLine($"Key: { t.Key }");
                Console.WriteLine($"Decrypted text: { t.Value }");
                Console.WriteLine();
            }
            
            Console.WriteLine("----------------------------------------------------------");
        }

        static CharactersFrequencyModel GetCharactersFrequercy(string type, string path)
        {
            var text = File.ReadAllText(path);
            var literatureFrequency = calculationsService.CalculateCharactersFrequency(text.ToLower());

            lock (locker)
            {
                Console.WriteLine(path);
                Console.WriteLine($"Total characters count: {literatureFrequency.TotalCharactersCount }.");

                foreach (var letter in literatureFrequency.CharactersFrequency.Keys)
                {
                    Console.WriteLine($"{ letter }: { literatureFrequency.CharactersFrequency[letter] }% {literatureFrequency.CharactersCount[letter]}");
                }

                Console.WriteLine();
            }

            return literatureFrequency;
        }
    }
}
