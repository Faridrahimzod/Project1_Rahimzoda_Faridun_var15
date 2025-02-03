///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using ClassLibrary;

namespace Project1_Rahimzoda_Faridun_var15
{
    /// <summary>
    /// Класс для работы с Excel файлами
    /// </summary>
    internal static class ExcelConverter
    {
        /// <summary>
        /// Метод для загрузки данных в файл
        /// </summary>
        /// <param name="vaultData">Объект хранения данных, для хранения в файле</param>
        public static void SaveToExcel(VaultData vaultData)
        {
            try
            {
                Console.Write("Введите путь для сохранения Excel-файла: ");
                string filePath = Console.ReadLine()?.Trim();// Получаем путь файла

                if (string.IsNullOrWhiteSpace(filePath)) // Проверка на присутствие пути
                {
                    Console.WriteLine("Ошибка: путь не может быть пустым!");
                    return;
                }

                // Добавляем расширение для файла, если его нет
                if (!filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    filePath += ".xlsx";
                }

                // Проверяем и создаём директорию
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                

                using (var workbook = new XLWorkbook()) // Создаём воркбук excel
                {
                    var worksheet = workbook.Worksheets.Add("VaultData"); // Назовём лист

                    // Заголовки
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Label";
                    worksheet.Cell(1, 3).Value = "Aspects";
                    worksheet.Cell(1, 4).Value = "Slots";
                    worksheet.Cell(1, 5).Value = "Description";
                    worksheet.Cell(1, 6).Value = "Unique";

                    // Проверка данных
                    if (vaultData.Elements == null
                        || !vaultData.Elements.TryGetValue("elements", out var elements)
                        || elements == null
                        || elements.Count == 0)
                    {
                        Console.WriteLine("Нет данных для сохранения.");
                        workbook.SaveAs(filePath); // Создаём пустой файл
                        return;
                    }

                    // Заполнение данных
                    int row = 2;
                    foreach (var element in elements)
                    {
                        worksheet.Cell(row, 1).Value = element.GetField("id")?.ToString() ?? string.Empty;
                        worksheet.Cell(row, 2).Value = element.GetField("label")?.ToString() ?? string.Empty;
                        worksheet.Cell(row, 3).Value = element.GetField("aspects")?.ToString() ?? string.Empty;
                        worksheet.Cell(row, 4).Value = element.GetField("slots")?.ToString() ?? string.Empty;
                        worksheet.Cell(row, 5).Value = element.GetField("description")?.ToString() ?? string.Empty;
                        worksheet.Cell(row, 6).Value = element.GetField("unique")?.ToString() ?? string.Empty;

                        row++;
                    }

                    // Автоподбор ширины столбцов
                    worksheet.Columns().AdjustToContents();
                    // Сохраняем
                    workbook.SaveAs(filePath); 
                    Console.WriteLine($"Файл успешно сохранён: {filePath}");

                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}"); // Или ошибка
            }
        }

        /// <summary>
        /// Метод для загрузки данных из файла
        /// </summary>
        /// <returns>Объект хранения данных</returns>
        public static VaultData LoadFromExcel()
        {
            Console.WriteLine("Введите путь к файлу Excel:");
            string filePath = Console.ReadLine(); // Получаем путь

            if (string.IsNullOrWhiteSpace(filePath)) // Проверка на не пустую строку
            {
                Console.WriteLine("Некорректный путь к файлу.");
                return new VaultData();
            }

            if (!File.Exists(filePath)) // Проверка на существовании файла
            {
                Console.WriteLine("Файл не найден, создаётся новый.");
                using (var workbook = new XLWorkbook())// Если его нет, создаётся новый
                {
                    workbook.Worksheets.Add("VaultData"); // Назовём лист
                    workbook.SaveAs(filePath);
                }
                return new VaultData();
            }

            var vaultData = new VaultData(); // Объект для хранения данных для возврата
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet("VaultData");
                foreach (var row in worksheet.RowsUsed().Skip(1)) // Пропускаем заголовки
                {
                    int ind1 = 0;
                    var element = new Element(row.Cell(1).GetString(), row.Cell(2).GetString(), new Aspect(row.Cell(5).GetString().Split(',').ToDictionary(a => a, a => 1)), new List<Slot>(), row.Cell(3).GetString(), row.Cell(4).GetBoolean());
                        
                        
                    

                    if (!vaultData.Elements.ContainsKey("elements"))
                    {
                        vaultData.Elements["elements"] = new List<Element>();
                    }
                    vaultData.Elements["elements"].Add(element); // Добавление в объект
                }
            }

            Console.WriteLine("Файл успешно загружен!");
            return vaultData; // Возврат
        }
    }
}
