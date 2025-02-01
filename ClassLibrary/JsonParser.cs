using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;


namespace ClassLibrary
{
    public static class JsonParser
    {
        // Метод для записи JSON в поток вывода
        public static void WriteJson(IJsonObject obj, string path)
        {
            var jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");

            var fields = obj.GetAllFields();
            bool first = true;

            foreach (var field in fields)
            {
                if (!first)
                {
                    jsonBuilder.Append(",");
                }
                first = false;

                string value = obj.GetField(field);
                jsonBuilder.Append($"\"{field}\":\"{value}\"");
            }

            jsonBuilder.Append("}");
            Console.Out.Write(jsonBuilder.ToString());
        }

        // Метод для чтения JSON из потока ввода
        public static IJsonObject ReadJson(string jsonString)
        {
            // Чтение всей JSON-строки из входного потока (например, консоли)
            
            int index = 0; // Индекс для отслеживания текущей позиции в строке

            // Парсинг JSON-строки и получение результата
            var result = ParsingType.ParseJsonValue(jsonString, ref index);

            // Проверка, что результат является объектом (словарём)
            if (result is Dictionary<string, List<Dictionary<string, object>>> dictionary)
            {
                // Возвращаем объект, реализующий IJsonObject
                return new VaultData(dictionary);
            }

            // Если результат не является объектом, выбрасываем исключение
            throw new FormatException("Invalid JSON format: expected an object.");
        }
    }
}
