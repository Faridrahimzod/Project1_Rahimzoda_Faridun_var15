using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (choiceForSorting == "1")
            {
                if (directoryForSort ==  "1")
                {
                    foreach (string el in vaultData.Elements.Keys)
                    {
                        List<Element> list = vaultData.Elements[el].OrderBy(e => e.Id).ToList();
                        Console.WriteLine(list.ToString());
                        v.Elements[el] = list;
                    }
                    return v;
                }
                else if (directoryForSort == "2")
                {
                    foreach (string el in vaultData.Elements.Keys)
                    {
                        List<Element> list = vaultData.Elements[el].OrderByDescending(e => e.Id).ToList();
                        Console.WriteLine(list.ToString());
                        v.Elements[el] = list;
                    }
                    return v;
                }
                else
                {
                    throw new Exception("Неправильный ввод направления сортировки");
                }
            }
            else if(choiceForSorting == "2")
            {
                if (directoryForSort == "1")
                {
                    foreach (string el in vaultData.Elements.Keys)
                    {
                        List<Element> list = vaultData.Elements[el].OrderBy(e => e.Label).ToList();
                        Console.WriteLine(list.ToString());
                        v.Elements[el] = list;
                    }
                    return v;
                }
                else if (directoryForSort == "2")
                {
                    foreach (string el in vaultData.Elements.Keys)
                    {
                        List<Element> list = vaultData.Elements[el].OrderByDescending(e => e.Label).ToList();
                        Console.WriteLine(list.ToString());
                        v.Elements[el] = list;
                    }
                    return v;
                }
                else
                {
                    throw new Exception("Неправильный ввод направления сортировки");
                }
            }
            else if (choiceForSorting == "3")
            {
                if (directoryForSort == "1")
                {
                    foreach (string el in vaultData.Elements.Keys)
                    {
                        List<Element> list = vaultData.Elements[el].OrderBy(e => e.Description).ToList();
                        Console.WriteLine(list.ToString());
                        v.Elements[el] = list;
                    }
                    return v;
                }
                else if (directoryForSort == "2")
                {
                    foreach (string el in vaultData.Elements.Keys)
                    {
                        List<Element> list = vaultData.Elements[el].OrderByDescending(e => e.Description).ToList();
                        Console.WriteLine(list.ToString());
                        v.Elements[el] = list;
                    }
                    return v;
                }
                else
                {
                    throw new Exception("Неправильный ввод направления сортировки");
                }
            }
            else if (choiceForSorting == "4")
            {
                if (directoryForSort == "1")
                {
                    foreach (string el in vaultData.Elements.Keys)
                    {
                        List<Element> list = vaultData.Elements[el].OrderBy(e => e.Unique).ToList();
                        Console.WriteLine(list.ToString());
                        v.Elements[el] = list;
                    }
                    return v;
                }
                else if (directoryForSort == "2")
                {
                    foreach (string el in vaultData.Elements.Keys)
                    {
                        List<Element> list = vaultData.Elements[el].OrderByDescending(e => e.Unique).ToList();
                        Console.WriteLine(list.ToString());
                        v.Elements[el] = list;
                    }
                    return v;
                }
                else
                {
                    throw new Exception("Неправильный ввод направления сортировки");
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
