using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurrencyConverterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] availableCurrency = CurrencyConverter.GetCurrencyTags();
            Console.WriteLine("Mata Uang yang tersedia : ");
            Console.WriteLine(string.Join(",", availableCurrency));
            Console.WriteLine("\n");

            
            Console.WriteLine("Covert mata uang dari : ");
            string fromCurrency = Console.ReadLine();
            Console.WriteLine("\n");

            Console.WriteLine("Convert mata uang ke : ");
            string toCurrency = Console.ReadLine();
            Console.WriteLine("\n"); 

            Console.WriteLine("Masukkan jumlah uang : ");
            int amount = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            float exchangeRate = CurrencyConverter.GetExchangeRate(fromCurrency, toCurrency, amount);
            Console.WriteLine( "Dari " + amount + " " + fromCurrency.ToUpper() + " ke " + toCurrency.ToUpper() + " adalah " + exchangeRate);
            Console.ReadLine();
        }
    }
}
