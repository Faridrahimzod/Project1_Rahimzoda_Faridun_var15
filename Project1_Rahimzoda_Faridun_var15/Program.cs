///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using ClassLibrary;
using Project1_Rahimzoda_Faridun_var15;
using System;

/// <summary>
/// Основная программа
/// </summary>
class Program
{
    static void Main()
    {
        string choice; // Для выбора польлзователя
        bool doing = true; // Для цикла
        VaultData vaultData = null; // Объект для хранения данных
        do
        {
            choice = MenuOption.Menu(); // Статический класс для показа меню
            switch (choice)
            {
                case "1": // Ввод данных
                    try
                    {
                        vaultData = (VaultData)InputOption.Input(); // Введение данных пользователем через метод
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "2": // Фильтрация
                    try
                    {
                        if (vaultData is null) // Проверка на присутствие данных
                        {
                            Console.WriteLine("Отсутствуют данные для фильтрации, введите сначала данные");
                            continue;
                        }
                        else
                        {
                            vaultData = Filter.FieldFiltering(vaultData); // Выполнение фильтрации через метод
                            Console.WriteLine(JsonParser.WriteJson(vaultData)); // Вывод результата на консоль

                        }
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "3": // Сортировка
                    try
                    {
                        if (vaultData is null) // Проверка на присутствие данных
                        {
                            Console.WriteLine("Отсутствуют данные для фильтрации, введите сначала данные");
                            continue;
                        }
                        else
                        {
                            vaultData = Sort.Sorting(vaultData); // Выполнение сортировки через метод
                            Console.WriteLine(JsonParser.WriteJson(vaultData)); // Вывод результата на консоль
                        }
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "4": // Поиск хранилища через id
                    try
                    {
                        if (vaultData is null) // Проверка на присутствие данных
                        {
                            Console.WriteLine("Отсутствуют данные для фильтрации, введите сначала данные");
                            continue;
                        }
                        else
                        {
                            vaultData = Finding.FindingById(vaultData); // Возврат информации про хранилище по id
                            Console.WriteLine(JsonParser.WriteJson(vaultData)); // Вывод результата на консоль
                        }
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "5": // Работа с Excel
                    try
                    {
                        Console.WriteLine("Выберите действие:\n1.Чтение данных из excel файла\n2.Конвертация данных в excel файл");
                        string choiceForExcel = Console.ReadLine(); // Получаем действие
                        if (choiceForExcel == "1")
                        {
                            vaultData = ExcelConverter.LoadFromExcel(); // Загружаем значение из файла Excel
                        }
                        else if (choiceForExcel == "2")
                        {
                            ExcelConverter.SaveToExcel(vaultData); // Загружаем значение в файл Excel
                        }
                        else { Console.WriteLine("Неправильный ввод"); } // Или неправильный ввод

                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "6": // Вывод результата
                    try
                    {
                        OutputOptions.Output(vaultData); // Выводим результат согласно методу
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e}"); }
                    break;
                case "7": // Выход
                    doing = false;
                    break;
                default: // Ошибка
                    Console.WriteLine("Выбрано неверное действие, повторите попытку");
                    break;
            }
        } while (doing);
    }
}
