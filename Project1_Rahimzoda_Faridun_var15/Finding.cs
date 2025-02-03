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
    /// Класс для поиска хранилища по id
    /// </summary>
    internal static class Finding
    {
        /// <summary>
        /// Метод, который помогает найти хранилище по id
        /// </summary>
        /// <param name="vaultData">Объект хранения данных</param>
        /// <returns>Хранилище подходящее по id</returns>
        public static VaultData FindingById(VaultData vaultData)
        {
            Console.WriteLine("Введите id нужного хранилища");
            string idForSearch = Console.ReadLine(); // Получаем id для поиска
            VaultData v = new VaultData(); // Создаём объект для хранения результата
            foreach (string el in vaultData.Elements.Keys) // Проходимся по значениям словаря
            {
                foreach (Element element in vaultData.Elements[el]) // Проходимся по элементам значений
                {
                    string? fieldValue = element.GetField("id"); // Получаем с каждого элемента значение поля id

                    if (fieldValue == idForSearch) // Если значение id равно вводимому значению...
                    {
                        if (!v.Elements.ContainsKey(el))
                        {
                            v.Elements[el] = new List<Element>(); // Инициализация списка, если его нет
                        }
                        v.Elements[el].Add(element);//...добавляем в объект
                    }
                }
            }
            return v; // Возврат
        }
    }
}
