///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Rahimzoda_Faridun_var15
{
    /// <summary>
    /// Класс для фильтрации данных
    /// </summary>
    internal static class Filter
    {
        /// <summary>
        /// Метод, помогающий отфильтровать данные в объекте
        /// </summary>
        /// <param name="vaultData">объект хранения данных</param>
        /// <returns>Отфильтрованный объект хранения данных</returns>
        /// <exception cref="Exception"></exception>
        public static VaultData FieldFiltering(VaultData vaultData)
        {
            Console.WriteLine("Выберите поле, по которому пройдет фильтрация:\n1. id\n2. label\n3. description\n4. unique");
            string choiceForFilter = Console.ReadLine(); // Выбор поля для фильтрации

            // Создаем новый объект VaultData для хранения результата
            VaultData v = new VaultData(new Dictionary<string, object>());
            

            Console.WriteLine("Введите значение для фильтрации:");
            string filterValue = Console.ReadLine(); // Получаем значение поля подходящее для фильтрации

            foreach (string el in vaultData.Elements.Keys) // Проходимся по ключам в объекте
            {
                foreach (Element element in vaultData.Elements[el]) // Проходимся по элементам значений
                {
                    string? fieldValue = element.GetField(choiceForFilter switch // Получаем поле для фильтрации
                    {
                        "1" => "id",
                        "2" => "label",
                        "3" => "description",
                        "4" => "unique",
                        _ => throw new Exception("Некорректный выбор.")
                    });

                    if (fieldValue == filterValue) // Если значения поля совпадают,
                    {
                        if (!v.Elements.ContainsKey(el))
                        {
                            v.Elements[el] = new List<Element>(); // Инициализация списка, если его нет
                        }
                        v.Elements[el].Add(element); // Добавляем список в объект для хранения данных
                    }
                }
            }

            return v; // Возврат
        }
    }

}
