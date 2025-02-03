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
    /// Класс Для вывода результата
    /// </summary>
    internal static class OutputOptions
    {
        /// <summary>
        /// Метод, выводящий объект для храненния данных в поток вывода
        /// </summary>
        /// <param name="vaultData">Объект для вывода</param>
        public static void Output(VaultData vaultData)
        {
            Console.WriteLine("Введите способ вывода данных:\n1.Консоль\n2.Файл");
            string choiceForOutput = Console.ReadLine(); // Получаем способ вывода
            if (choiceForOutput == "1") 
            {
                Console.WriteLine(JsonParser.WriteJson(vaultData)); // Выводим на консоль 
            }
            else if (choiceForOutput == "2")
            {
                Console.WriteLine("Введите путь к файлу, в котором будет храниться результат");
                string path = Console.ReadLine(); // Получаем путь
                using (StreamWriter log =  new StreamWriter(path))
                {
                    log.Write(JsonParser.WriteJson(vaultData)); // Выводим результат в файл
                }
                
            }
        }
    }
}
