using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Rahimzoda_Faridun_var15
{
    internal static class OutputOptions
    {
        public static void Output(VaultData vaultData)
        {
            Console.WriteLine("Введите способ вывода данных:\n1.Консоль\n2.Файл");
            string choiceForOutput = Console.ReadLine();
            if (choiceForOutput == "1")
            {
                Console.WriteLine(JsonParser.WriteJson(vaultData));
            }
            else if (choiceForOutput == "2")
            {
                Console.WriteLine("Введите путь к файлу, в котором будет храниться результат");
                string path = Console.ReadLine();
                using (StreamWriter log =  new StreamWriter(path))
                {
                    Console.SetOut(log);
                    Console.WriteLine(JsonParser.WriteJson(vaultData));
                }
                Console.SetOut(Console.Out);
            }
        }
    }
}
