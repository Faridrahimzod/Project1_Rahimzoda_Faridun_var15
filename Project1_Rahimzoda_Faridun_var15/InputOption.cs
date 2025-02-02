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
            Console.WriteLine("1. Консоль");
            Console.WriteLine("2. Файл");
            string choiceForInput = Console.ReadLine();

            if (choiceForInput == "1")
            {
                Console.WriteLine("Введите JSON:");
                string jsonInput = Console.ReadLine();
                return JsonParser.ReadJson(jsonInput);
            }
            else if (choiceForInput == "2")
            {
                Console.WriteLine("Введите путь к файлу:");
                string filePath = Console.ReadLine();
                string res;

                // Чтение файла без изменения Console.In
                using (StreamReader fileInput = new StreamReader(filePath))
                {
                    res = fileInput.ReadToEnd(); // Читаем всё содержимое файла
                }

                return JsonParser.ReadJson(res);
            }
            else
            {
                throw new Exception("Неверный выбор.");
            }
        }
    }
}
