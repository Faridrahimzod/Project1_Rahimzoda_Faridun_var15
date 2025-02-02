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
        public static string WriteJsonObject(object obj)
        {
            if (obj == null) return "null";

            Type type = obj.GetType();

            if (obj is VaultData vaultData)
            {
                return WriteJsonObject(vaultData.ToSerializableObject());
            }

            if (type == typeof(string))
            {
                return $"\"{obj}\"";
            }
            else if (type == typeof(int) || type == typeof(double) || type == typeof(float) || type == typeof(bool))
            {
                return obj.ToString().ToLower();
            }
            else if (obj is IEnumerable<object> list)
            {
                return "[" + string.Join(",", list.Select(WriteJsonObject)) + "]";
            }
            else if (obj is Dictionary<string, object> dict) // Обрабатываем правильно словари
            {
                return "{" + string.Join(",", dict.Select(kv => $"\"{kv.Key}\":{WriteJsonObject(kv.Value)}")) + "}";
            }
            else if (obj is Dictionary<string, List<Element>> elementsDict) // Исправляем сериализацию элементов
            {
                return "{" + string.Join(",", elementsDict.Select(kv =>
                    $"\"{kv.Key}\":[{string.Join(",", kv.Value.Select(WriteJsonObject))}]")) + "}";
            }
            else
            {
                var properties = type.GetProperties().Where(prop => prop.GetIndexParameters().Length == 0);
                var jsonPairs = properties.Select(prop =>
                {
                    var value = prop.GetValue(obj);
                    return $"\"{prop.Name}\":{WriteJsonObject(value)}";
                });

                return "{" + string.Join(",", jsonPairs) + "}";
            }
        }





        public static object ParseJsonValue(string json, ref int index)
        {
            SkipWhitespace(json, ref index);

            if (index >= json.Length)
                throw new FormatException("Недопустимая JSON строка");

            char currentChar = json[index];

            if (currentChar == '{')
                return ParseJsonObject(json, ref index);
            else if (currentChar == '[')
                return ParseJsonArray(json, ref index);
            else if (currentChar == '"')
                return ParseJsonString(json, ref index);
            else if (char.IsDigit(currentChar) || currentChar == '-')
                return ParseJsonNumber(json, ref index);
            else if (currentChar == 't' || currentChar == 'f')
                return ParseJsonBoolean(json, ref index);
            else if (currentChar == 'n')
                return ParseJsonNull(json, ref index);

            throw new FormatException($"Неправильный символ: {currentChar}");
        }

        private static Dictionary<string, object> ParseJsonObject(string json, ref int index)
        {
            var result = new Dictionary<string, object>();
            index++;

            while (index < json.Length)
            {
                SkipWhitespace(json, ref index);

                if (json[index] == '}')
                {
                    index++;
                    return result;
                }

                string key = ParseJsonString(json, ref index);
                SkipWhitespace(json, ref index);

                if (json[index] != ':')
                    throw new FormatException("Отсутствует символ ':'");

                index++;
                object value = ParseJsonValue(json, ref index);
                result[key] = value;

                SkipWhitespace(json, ref index);

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

        private static List<object> ParseJsonArray(string json, ref int index)
        {
            var result = new List<object>();
            index++;

            while (index < json.Length)
            {
                SkipWhitespace(json, ref index);

                if (json[index] == ']')
                {
                    index++;
                    return result;
                }

                object value = ParseJsonValue(json, ref index);
                result.Add(value);

                SkipWhitespace(json, ref index);

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

        private static string ParseJsonString(string json, ref int index)
        {
            index++;
            var result = new StringBuilder();

            while (index < json.Length)
            {
                char currentChar = json[index];

                if (currentChar == '\\') // Экранирование символов
                {
                    index++;
                    if (index >= json.Length) throw new FormatException("Ожидался символ после '\\'");

                    char escapeChar = json[index];
                    if (escapeChar == '"') result.Append('"');
                    else if (escapeChar == '\\') result.Append('\\');
                    else if (escapeChar == '/') result.Append('/');
                    else if (escapeChar == 'b') result.Append('\b');
                    else if (escapeChar == 'f') result.Append('\f');
                    else if (escapeChar == 'n') result.Append('\n');
                    else if (escapeChar == 'r') result.Append('\r');
                    else if (escapeChar == 't') result.Append('\t');
                    else throw new FormatException($"Недопустимый escape-последовательность: \\{escapeChar}");
                }
                else if (currentChar == '"')
                {
                    index++;
                    return result.ToString();
                }
                else
                {
                    result.Append(currentChar);
                }

                index++;
            }

            throw new FormatException("Неправильный формат строки");
        }

        private static object ParseJsonNumber(string json, ref int index)
        {
            var startIndex = index;

            while (index < json.Length && (char.IsDigit(json[index]) || json[index] == '-' || json[index] == '.'))
            {
                index++;
            }

            string numberString = json.Substring(startIndex, index - startIndex);

            if (int.TryParse(numberString, out int intValue))
                return intValue;

            if (double.TryParse(numberString, out double doubleValue))
                return doubleValue;

            throw new FormatException($"Недопустимый формат числа: {numberString}");
        }

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

        private static object ParseJsonNull(string json, ref int index)
        {
            if (json.Substring(index, 4) == "null")
            {
                index += 4;
                return null;
            }

            throw new FormatException("Недопустимое значение null");
        }

        private static void SkipWhitespace(string json, ref int index)
        {
            while (index < json.Length && char.IsWhiteSpace(json[index]))
            {
                index++;
            }
        }
    }
}
