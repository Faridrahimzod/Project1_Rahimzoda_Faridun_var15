///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// Вспомогательный класс для Парсинга 
    /// </summary>
    public class ParsingType
    {
        /// <summary>
        /// Рекурсивный метод для записывания объекта хранения данных в виде строки
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string WriteJsonObject(object obj)
        {
            if (obj == null) return "null";

            Type type = obj.GetType();

            if (obj is VaultData vaultData)
            {
                return WriteJsonObject(vaultData.ToSerializableObject());// Возврат, если параметр - Объект для хранения данных
            }

            if (type == typeof(string))
            {
                return $"\"{obj}\""; // Возврат, если параметр - строка
            }
            else if (type == typeof(int) || type == typeof(double) || type == typeof(float) || type == typeof(bool))
            {
                return obj.ToString().ToLower(); // Возврат, если параметр - число
            }
            else if (obj is IEnumerable<object> list)
            {
                return "[" + string.Join(",", list.Select(WriteJsonObject)) + "]"; // Возврат, если параметр - список
            }
            else if (obj is Dictionary<string, object> dict)
            {
                return "{" + string.Join(",", dict.Select(kv => $"\"{kv.Key}\":{WriteJsonObject(kv.Value)}")) + "}";// Возврат, если параметр - словарь, где ключ - строка, значение - объект
            }
            else if (obj is Dictionary<string, List<Element>> elementsDict)
            {
                return "{" + string.Join(",", elementsDict.Select(kv =>
                    $"\"{kv.Key}\":[{string.Join(",", kv.Value.Select(WriteJsonObject))}]")) + "}";// Возврат, если параметр - словарь, где ключ - строка, значение - Список Элементов
            }
            else
            {
                var properties = type.GetProperties().Where(prop => prop.GetIndexParameters().Length == 0);
                var jsonPairs = properties.Select(prop =>
                {
                    var value = prop.GetValue(obj);
                    return $"\"{prop.Name}\":{WriteJsonObject(value)}";
                });

                return "{" + string.Join(",", jsonPairs) + "}";// Возврат, если параметр - словарь, где ключ - строка, значение - Объект интерфеса IJSONObject
            }
        }




        /// <summary>
        /// Рекурсивный метод для Парсинга строки в объект хранения данных
        /// </summary>
        /// <param name="json">Строка, которая будет запарсированна</param>
        /// <param name="index">Номер элемента в строке</param>
        /// <returns>Объект хранения данных</returns>
        /// <exception cref="FormatException"></exception>
        public static object ParseJsonValue(string json, ref int index)
        {
            SkipWhitespace(json, ref index);

            if (index >= json.Length)
                throw new FormatException("Недопустимая JSON строка");

            char currentChar = json[index];

            // Определение типа значения и вызов соответствующего парсера
            if (currentChar == '{')
                return ParseJsonObject(json, ref index);     // Объект
            else if (currentChar == '[')
                return ParseJsonArray(json, ref index);      // Массив
            else if (currentChar == '"')
                return ParseJsonString(json, ref index);     // Строка
            else if (char.IsDigit(currentChar) || currentChar == '-')
                return ParseJsonNumber(json, ref index);     // Число
            else if (currentChar == 't' || currentChar == 'f')
                return ParseJsonBoolean(json, ref index);    // Логическое значение
            else if (currentChar == 'n')
                return ParseJsonNull(json, ref index);       // Null

            throw new FormatException($"Неправильный символ: {currentChar}");
        }

        /// <summary>
        /// Парсинг JSON-объекта в Dictionary<string, object>
        /// </summary>
        /// <param name="json"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        private static Dictionary<string, object> ParseJsonObject(string json, ref int index)
        {
            var result = new Dictionary<string, object>();
            index++; // Пропускаем '{'

            while (index < json.Length)
            {
                SkipWhitespace(json, ref index);

                // Проверка конца объекта
                if (json[index] == '}')
                {
                    index++;
                    return result;
                }

                // Парсинг пары ключ-значение
                string key = ParseJsonString(json, ref index);
                SkipWhitespace(json, ref index);

                if (json[index] != ':')
                    throw new FormatException("Отсутствует символ ':'");

                index++; // Пропускаем ':'
                object value = ParseJsonValue(json, ref index); // Рекурсивный парсинг значения
                result[key] = value;

                SkipWhitespace(json, ref index);

                // Обработка разделителя элементов
                if (json[index] == ',')
                {
                    index++;
                }
                else if (json[index] != '}')
                {
                    throw new FormatException("Ожидался символ '}'");
                }
            }

            throw new FormatException("Неверный объект JSON");
        }

        /// <summary>
        /// Парсинг JSON-массива в List<object>
        /// </summary>
        /// <param name="json"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        private static List<object> ParseJsonArray(string json, ref int index)
        {
            
            var result = new List<object>();
            index++; // Пропускаем '['

            while (index < json.Length)
            {
                SkipWhitespace(json, ref index);

                // Проверка конца массива
                if (json[index] == ']')
                {
                    index++;
                    return result;
                }

                // Парсинг элемента массива
                object value = ParseJsonValue(json, ref index);
                result.Add(value);

                SkipWhitespace(json, ref index);

                // Обработка разделителя элементов
                if (json[index] == ',')
                {
                    index++;
                }
                else if (json[index] != ']')
                {
                    throw new FormatException("Ожидался символ ']'");
                }
            }

            throw new FormatException("Неверный массив JSON");
        }

        /// <summary>
        /// Парсинг JSON-строки в виде строки
        /// </summary>
        /// <param name="json"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        private static string ParseJsonString(string json, ref int index)
        {
            
            index++; // Пропускаем открывающую кавычку
            var result = new StringBuilder();

            while (index < json.Length)
            {
                char currentChar = json[index];

                // Обработка escape-символов
                if (currentChar == '\\')
                {
                    index++;
                    if (index >= json.Length) throw new FormatException("Ожидался символ после '\\'");

                    // Замена escape-последовательностей
                    switch (json[index])
                    {
                        case '"': result.Append('"'); break;
                        case '\\': result.Append('\\'); break;
                        case '/': result.Append('/'); break;
                        case 'b': result.Append('\b'); break;
                        case 'f': result.Append('\f'); break;
                        case 'n': result.Append('\n'); break;
                        case 'r': result.Append('\r'); break;
                        case 't': result.Append('\t'); break;
                        default: throw new FormatException($"Недопустимый escape-символ: \\{json[index]}");
                    }
                }
                else if (currentChar == '"') // Конец строки
                {
                    index++;
                    return result.ToString();
                }
                else // Обычный символ
                {
                    result.Append(currentChar);
                }

                index++;
            }

            throw new FormatException("Неправильный формат строки");
        }
        /// <summary>
        /// Парсинг числовых значений (целые и с плавающей точкой)
        /// </summary>
        /// <param name="json"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        private static object ParseJsonNumber(string json, ref int index)
        {
            
            var startIndex = index;

            // Сбор всех символов числа
            while (index < json.Length && (char.IsDigit(json[index]) || json[index] == '-' || json[index] == '.'))
            {
                index++;
            }

            string numberString = json.Substring(startIndex, index - startIndex);

            // Попытка парсинга как целого числа
            if (int.TryParse(numberString, out int intValue))
                return intValue;

            // Попытка парсинга как числа с плавающей точкой
            if (double.TryParse(numberString, out double doubleValue))
                return doubleValue;

            throw new FormatException($"Недопустимый формат числа: {numberString}");
        }

        /// <summary>
        /// Парсинг логических значений (true/false)
        /// </summary>
        /// <param name="json"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        private static bool ParseJsonBoolean(string json, ref int index)
        {
            
            if (json.Substring(index, 4) == "true")
            {
                index += 4;
                return true;
            }
            else if (json.Substring(index, 5) == "false")
            {
                index += 5;
                return false;
            }

            throw new FormatException("Недопустимое значение");
        }

        /// <summary>
        /// Парсинг null-значения
        /// </summary>
        /// <param name="json"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        private static object ParseJsonNull(string json, ref int index)
        {
            
            if (json.Substring(index, 4) == "null")
            {
                index += 4;
                return null;
            }

            throw new FormatException("Недопустимое значение null");
        }

        /// <summary>
        /// Пропуск пробельных символов (пробелы, табы, переносы строк)
        /// </summary>
        /// <param name="json"></param>
        /// <param name="index"></param>
        private static void SkipWhitespace(string json, ref int index)
        {
            
            while (index < json.Length && char.IsWhiteSpace(json[index]))
            {
                index++;
            }
        }
    }
}
