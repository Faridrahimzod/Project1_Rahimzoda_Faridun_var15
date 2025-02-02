using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Rahimzoda_Faridun_var15
{
    internal static class Filter
    {
        public static VaultData FieldFiltering(VaultData vaultData)
        {
            Console.WriteLine("Выберите поле, по которому пройдет фильтрация:\n1. id\n2. label\n3. description\n4. unique");
            string choiceForFilter = Console.ReadLine();

            // Создаем новый объект VaultData
            VaultData v = new VaultData(new Dictionary<string, object>());
            

            Console.WriteLine("Введите значение для фильтрации:");
            string filterValue = Console.ReadLine();

            foreach (string el in vaultData.Elements.Keys)
            {
                foreach (Element element in vaultData.Elements[el])
                {
                    string? fieldValue = element.GetField(choiceForFilter switch
                    {
                        "1" => "id",
                        "2" => "label",
                        "3" => "description",
                        "4" => "unique",
                        _ => throw new Exception("Некорректный выбор.")
                    });

                    if (fieldValue == filterValue)
                    {
                        if (!v.Elements.ContainsKey(el))
                        {
                            v.Elements[el] = new List<Element>(); // Инициализация списка, если его нет
                        }
                        v.Elements[el].Add(element);
                    }
                }
            }

            return v;
        }
    }

}
