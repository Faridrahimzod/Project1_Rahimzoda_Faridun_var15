///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1_Rahimzoda_Faridun_var15
{
    /// <summary>
    /// Класс для сортировки
    /// </summary>
    internal static class Sort
    {
        /// <summary>
        /// Метод, помогающий для сортировки объекта для хранения данных
        /// </summary>
        /// <param name="vaultData">Объект хранения данных, который следует отсортировать</param>
        /// <returns>Отсортированный объект для хранения данных</returns>
        /// <exception cref="Exception"></exception>
        public static VaultData Sorting(VaultData vaultData)
        {
            Console.WriteLine("Выберите поле для сортировки:\n1.id\n2.label\n3.description\n4.unique");
            string choiceForSorting = Console.ReadLine(); // Получаем поле, через которое будет происходить сортировка
            Console.WriteLine("Выберите направление сортировки:\n1.По возрастанию\n2.По убыванию");
            string directoryForSort = Console.ReadLine(); // Получаем направление сортировки

            VaultData v = new VaultData(); // Создаем Объект для хранения отсортированных данных

            // Явно создаем новый словарь, так как private set не позволяет изменять существующий
            var sortedElements = new Dictionary<string, List<Element>>();

            Func<Element, object> keySelector = choiceForSorting switch // Делегат для хранения поля через который будет проходится сортировка
            {
                "1" => e => e.Id,
                "2" => e => e.Label,
                "3" => e => e.Description,
                "4" => e => e.Unique,
                _ => throw new Exception("Неверный выбор сортировки")
            };

            bool ascending = directoryForSort == "1";
            foreach (string el in vaultData.Elements.Keys) // Проходим через словарь для сортировки...
            {
                sortedElements[el] = ascending
                    ? vaultData.Elements[el].OrderBy(keySelector).ToList()//...по возрастанию
                    : vaultData.Elements[el].OrderByDescending(keySelector).ToList();//...по убыванию
            }

            // Используем рефлексию, чтобы установить значение приватного set
            typeof(VaultData).GetProperty("Elements")!
                .SetValue(v, sortedElements);

            return v; // Возврат
        }
    }

}
