using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using ClassLibrary;

namespace Project1_Rahimzoda_Faridun_var15
{
    internal static class ExcelConverter
    {
        public static void SaveToExcel(VaultData vaultData)
        {
            try
            {
                Console.Write("Введите путь для сохранения Excel-файла: ");
                string filePath = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    Console.WriteLine("Ошибка: путь не может быть пустым!");
                    return;
                }

                bool fileExists = File.Exists(filePath);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("VaultData");


                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Label";
                    worksheet.Cell(1, 3).Value = "Description";
                    worksheet.Cell(1, 4).Value = "Unique";
                        
                    //worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;


                    // Начальная строка для записи данных
                    int row = 2;

                    // Проверка наличия данных
                    if (vaultData.Elements == null || !vaultData.Elements.ContainsKey("elements"))
                    {
                        Console.WriteLine("Нет данных для сохранения.");
                        return;
                    }

                    // Запись элементов
                    foreach (var element in vaultData.Elements["elements"])
                    {
                        worksheet.Cell(row, 1).Value = element.GetField("id");
                        worksheet.Cell(row, 2).Value = element.GetField("label");
                        worksheet.Cell(row, 3).Value = element.GetField("description");
                        worksheet.Cell(row, 4).Value = element.GetField("unique");
                        
                        row++;
                    }

                    workbook.SaveAs(filePath);
                    Console.WriteLine($"Данные сохранены в {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в Excel: {ex.Message}");
            }
        }

        public static VaultData LoadFromExcel()
        {
            Console.WriteLine("Введите путь к файлу Excel:");
            string filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Некорректный путь к файлу.");
                return new VaultData();
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден, создаётся новый.");
                using (var workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add("VaultData");
                    workbook.SaveAs(filePath);
                }
                return new VaultData();
            }

            var vaultData = new VaultData();
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
                    vaultData.Elements["elements"].Add(element);
                }
            }

            Console.WriteLine("Файл успешно загружен!");
            return vaultData;
        }
    }
}
