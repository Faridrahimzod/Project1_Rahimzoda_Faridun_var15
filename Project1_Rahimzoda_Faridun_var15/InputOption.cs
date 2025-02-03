///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Rahimzoda_Faridun_var15
{
    /// <summary>
    /// Класс для преобразования входных данных в объект для хранения
    /// </summary>
    internal static class InputOption
    {
        /// <summary>
        /// Метод для получения данных
        /// </summary>
        /// <returns>Объект для хранения данных</returns>
        /// <exception cref="Exception">Неправильный выбор</exception>
        public static IJsonObject Input()
        {
            Console.WriteLine("Выберите способ ввода данных:");
            Console.WriteLine("1. Консоль");
            Console.WriteLine("2. Файл");
            string choiceForInput = Console.ReadLine(); // Переменная для хранения выбора пользователя

            if (choiceForInput == "1") // Ввод будет с консоли
            {
                Console.WriteLine("Введите JSON:");
                string jsonInput = Console.ReadLine();
                return JsonParser.ReadJson(jsonInput); // Возвращаем объект для хранения
            }
            else if (choiceForInput == "2") // Ввод будет с файла, путь которого введется в консоль
            {
                Console.WriteLine("Введите путь к файлу:");
                string filePath = Console.ReadLine(); // Путь к файлу
                string res; // Переменная для сохранения результата чтения файла

                // Чтение файла 
                using (StreamReader fileInput = new StreamReader(filePath))
                {
                    res = fileInput.ReadToEnd(); // Читаем всё содержимое файла
                }

                return JsonParser.ReadJson(res); // Возвращаем объект для хранения
            }
            else
            {
                throw new Exception("Неверный выбор."); // Или ошибку
            }
        }
    }
}
