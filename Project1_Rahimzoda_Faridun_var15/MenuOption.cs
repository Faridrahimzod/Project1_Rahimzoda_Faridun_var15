///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using System;
using System.Collections.Generic;


namespace Project1_Rahimzoda_Faridun_var15
{
    /// <summary>
    /// Статический класс для показа меню
    /// </summary>
    internal static class MenuOption
    {
        /// <summary>
        /// Метод показа меню
        /// </summary>
        /// <returns>Строка - Выбор пользователя</returns>
        public static string? Menu()
        {
            Console.WriteLine("================= Menu =====================");
            Console.WriteLine("1.Ввести данные (консоль/файл)");
            Console.WriteLine("2.Отфильтровать данные");
            Console.WriteLine("3.Отсортировать данные");
            Console.WriteLine("4.Найти информацию про хранилище");
            Console.WriteLine("5.Конвертация загруженных данных в Excel-таблицу и обратно");
            Console.WriteLine("6.Вывести данные(консоль/файл)");
            Console.WriteLine("7.Выход");
            Console.SetIn(Console.In);
            string res = Console.ReadLine();
            return res;
        }
    }
}
