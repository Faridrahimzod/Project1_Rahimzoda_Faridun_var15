using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Rahimzoda_Faridun_var15
{
    internal static class Finding
    {
        public static VaultData FindingById(VaultData vaultData)
        {
            Console.WriteLine("Введите id нужного хранилища");
            string idForSearch = Console.ReadLine();
            VaultData v = new VaultData();
            foreach (string el in vaultData.Elements.Keys)
            {
                foreach (Element element in vaultData.Elements[el])
                {
                    string? fieldValue = element.GetField("id");

                    if (fieldValue == idForSearch)
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
