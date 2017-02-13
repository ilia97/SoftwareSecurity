using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondTask.Services;

namespace SecondTask
{
    class Program
    {
        static EncryptionService service = new EncryptionService();

        static void Main(string[] args)
        {
            Console.WriteLine("2.1 Реализовать одноразовый блокнот.");
            string text = "ALLSWELLTHATENDSWELL"; // Encrypting "All's well that ends well"
            string key = "EVTIQWXQVVOPMCXREPRZ";
            Console.WriteLine($"Encrypting text {text} with key {key}.");
            var encryptedString = service.Encrypt(text, key); 
            Console.WriteLine($"Encrypted string: {encryptedString}");
            Console.WriteLine("--------------------------------------------------------------\n");

            Console.WriteLine("2.1 XOR.");
            Console.WriteLine($"true XOR true = { service.XOR(true, true) } ");
            Console.WriteLine($"true XOR false = { service.XOR(true, false) } ");
            Console.WriteLine($"false XOR true = { service.XOR(false, true) } ");
            Console.WriteLine($"false XOR false = { service.XOR(false, false) } ");
        }
    }
}
