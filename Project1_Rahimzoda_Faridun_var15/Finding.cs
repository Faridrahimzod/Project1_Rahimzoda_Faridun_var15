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
            foreach (string key in vaultData.Elements.Keys)
            {
                foreach(Element element in vaultData.Elements[key])
                {
                    if (element.Id == idForSearch)
                    {
                        Console.WriteLine(element.ToString());
                        v.Elements[key].Add(element);
                        break;
                    }
                    
                }
            }
            return v;
        }
    }
}
