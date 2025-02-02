using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1_Rahimzoda_Faridun_var15
{
    internal static class Sort
    {
        public static VaultData Sorting(VaultData vaultData)
        {
            Console.WriteLine("Выберите поле для сортировки:\n1.id\n2.label\n3.description\n4.unique");
            string choiceForSorting = Console.ReadLine();
            Console.WriteLine("Выберите направление сортировки:\n1.По возрастанию\n2.По убыванию");
            string directoryForSort = Console.ReadLine();

            VaultData v = new VaultData();

            // Явно создаем новый словарь, так как private set не позволяет изменять существующий
            var sortedElements = new Dictionary<string, List<Element>>();

            Func<Element, object> keySelector = choiceForSorting switch
            {
                "1" => e => e.Id,
                "2" => e => e.Label,
                "3" => e => e.Description,
                "4" => e => e.Unique,
                _ => throw new Exception("Неверный выбор сортировки")
            };

            bool ascending = directoryForSort == "1";
            foreach (string el in vaultData.Elements.Keys)
            {
                sortedElements[el] = ascending
                    ? vaultData.Elements[el].OrderBy(keySelector).ToList()
                    : vaultData.Elements[el].OrderByDescending(keySelector).ToList();
            }

            // Используем рефлексию, чтобы установить значение приватного set
            typeof(VaultData).GetProperty("Elements")!
                .SetValue(v, sortedElements);

            return v;
        }
    }

}
