using ClassLibrary;
using Project1_Rahimzoda_Faridun_var15;
using System;


class Program
{
    static void Main()
    {
        string choice;
        bool doing = true;
        VaultData vaultData = null;
        do
        {
            choice = MenuOption.Menu();
            switch (choice)
            {
                case "1":
                    try
                    {
                        vaultData = (VaultData)InputOption.Input();
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "2":
                    try
                    {
                        if (vaultData is null)
                        {
                            Console.WriteLine("Отсутствуют данные для фильтрации, введите сначала данные");
                            continue;
                        }
                        else
                        {
                            vaultData = Filter.FieldFiltering(vaultData);
                            Console.WriteLine(JsonParser.WriteJson(vaultData));

                        }
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "3":
                    try
                    {
                        if (vaultData is null)
                        {
                            Console.WriteLine("Отсутствуют данные для фильтрации, введите сначала данные");
                            continue;
                        }
                        else
                        {
                            vaultData = Sort.Sorting(vaultData);
                            Console.WriteLine(JsonParser.WriteJson(vaultData));
                        }
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "4":
                    try
                    {
                        if (vaultData is null)
                        {
                            Console.WriteLine("Отсутствуют данные для фильтрации, введите сначала данные");
                            continue;
                        }
                        else
                        {
                            vaultData = Finding.FindingById(vaultData);
                            Console.WriteLine(JsonParser.WriteJson(vaultData));
                        }
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "5":
                    try
                    {
                        Console.WriteLine("Выберите действие:\n1.Чтение данных из excel файла\n2.Конвертация данных в excel файл");
                        string choiceForExcel = Console.ReadLine();
                        if (choiceForExcel != "1")
                        {
                            vaultData = ExcelConverter.LoadFromExcel();
                        }
                        else if (choiceForExcel == "2")
                        {
                            ExcelConverter.SaveToExcel(vaultData);
                        }
                        else { Console.WriteLine("Неправильный ввод"); }

                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "6":
                    try
                    {
                        OutputOptions.Output(vaultData);
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
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
