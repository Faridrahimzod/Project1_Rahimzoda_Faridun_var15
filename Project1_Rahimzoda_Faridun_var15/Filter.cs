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
            Console.WriteLine("Выберите поле по которому пройдёт фильтрация:\n1.id\n2.label\n3.description\n4.unique");
            string choiceForFilter = Console.ReadLine();

            VaultData v = new VaultData();
            if (choiceForFilter == "1")
            {
                Console.WriteLine("Введите значение id для фильтрации");
                string id = Console.ReadLine();
                foreach (string el in vaultData.Elements.Keys)
                {
                    foreach (Element element in vaultData.Elements[el])
                    {
                        if (element.GetField("id") == id)
                        {
                            Console.WriteLine(element.ToString);
                            v.Elements[el].Add(element);
                        }

                    }
                }
                return v;
            }
            else if (choiceForFilter == "2")
            {
                Console.WriteLine("Введите значение label для фильтрации");
                string label = Console.ReadLine();
                foreach (string el in vaultData.Elements.Keys)
                {
                    foreach (Element element in vaultData.Elements[el])
                    {
                        if (element.GetField("label") == label)
                        {
                            v.Elements[el].Add(element);
                        }

                    }
                }
                return v;
            }
            
            else if (choiceForFilter == "3")
            {
                Console.WriteLine("Введите значение description для фильтрации");
                string description = Console.ReadLine();
                foreach (string el in vaultData.Elements.Keys)
                {
                    foreach (Element element in vaultData.Elements[el])
                    {
                        if (element.GetField("description") == description)
                        {
                            v.Elements[el].Add(element);
                        }

                    }
                }
                return v;
            }
            else if (choiceForFilter == "4")
            {
                Console.WriteLine("Введите значение unique для фильтрации");
                string unique = Console.ReadLine();
                foreach (string el in vaultData.Elements.Keys)
                {
                    foreach (Element element in vaultData.Elements[el])
                    {
                        if (element.GetField("unique") == unique)
                        {
                            v.Elements[el].Add(element);
                        }

                    }
                }
                return v;
            }
            else
            {
                throw new Exception();
            }
            
        }
    }
}
