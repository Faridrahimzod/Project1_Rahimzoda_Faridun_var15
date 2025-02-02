using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Rahimzoda_Faridun_var15
{
    internal static class InputOption
    {
        public static IJsonObject Input()
        {
            Console.WriteLine("Выберите способ ввода данных:");
            Console.WriteLine("1.Консоль");
            Console.WriteLine("2.Файл");
            string? choiceForInput = Console.ReadLine();
            string? code;
            if (choiceForInput == "1")
            {
                code = Console.ReadLine();
                return JsonParser.ReadJson(code);
            }
            else if (choiceForInput == "2")
            {
                Console.WriteLine("Введите путь к файлу:");
                code = Console.ReadLine();
                string res = "";
                using (StreamReader fileInput = new StreamReader(code))
                {
                    Console.SetIn(fileInput);
                    string line;
                    while ((line = Console.ReadLine()) != null)
                    {
                        res += line;
                        res += "\n";
                    }
                }
                Console.SetIn(Console.In);
                return JsonParser.ReadJson(res);
            }
            else
            {
                throw new Exception();
            }
            
        }
    }
}
