using ClassLibrary;
using Project1_Rahimzoda_Faridun_var15;
using System;


class Program
{
    static void Main()
    {
        string choice;
        bool doing = true;
        do
        {
            choice = MenuOption.Menu();
            VaultData vaultData = null;
            switch (choice)
            {
                case "1": vaultData = (VaultData)InputOption.Input(); break;
                case "2": 
                    if (vaultData is null)
                    {
                        Console.WriteLine("Отсутствуют данные для фильтрации, введите сначала данные");
                        continue;
                    }
                    else
                    {
                        vaultData = Filter.FieldFiltering(vaultData);
                    }
                    break;
                case "3":
                    if (vaultData is null)
                    {
                        Console.WriteLine("Отсутствуют данные для фильтрации, введите сначала данные");
                        continue;
                    }
                    else
                    {
                        vaultData = Sort.Sorting(vaultData);
                    }
                    break;
                case "4":
                    if (vaultData is null)
                    {
                        Console.WriteLine("Отсутствуют данные для фильтрации, введите сначала данные");
                        continue;
                    }
                    else
                    {
                        vaultData = Finding.FindingById(vaultData);
                    }
                    break;
                case "5": 
                    //Здесь будет Доп. Задача
                    break;
                case "6":
                    OutputOptions.Output(vaultData);
                    break;
                case "7":
                    doing = false;
                    break;
                default:
                    Console.WriteLine("Выбрано неверное действие, повторите попытку");
                    break;
            }
        } while (doing);
    }
}
