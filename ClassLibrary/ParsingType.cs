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
        // Рекурсивный метод для парсинга JSON-значений
        public static object ParseJsonValue(string json, ref int index)
        {
            // Пропуск пробелов и других незначимых символов
            SkipWhitespace(json, ref index);

            // Проверка на конец строки
            if (index >= json.Length)
            {
                throw new FormatException("Недопустимая JSON строка");
            }

            // Определение типа значения по первому символу
            char currentChar = json[index];

            if (currentChar == '{') // Если это объект
            {
                return ParseJsonObject(json, ref index);
            }
            else if (currentChar == '[') // Если это массив
            {
                return ParseJsonArray(json, ref index);
            }
            else if (currentChar == '"') // Если это строка
            {
                return ParseJsonString(json, ref index);
            }
            else if (char.IsDigit(currentChar) || currentChar == '-') // Если это число
            {
                return ParseJsonNumber(json, ref index);
            }
            else if (currentChar == 't' || currentChar == 'f') // Если это булево значение
            {
                return ParseJsonBoolean(json, ref index);
            }
            else if (currentChar == 'n') // Если это null
            {
                return ParseJsonNull(json, ref index);
            }

            // Если символ не распознан, выбрасываем исключение
            throw new FormatException($"Неправильный символ: {currentChar}");
        }

        // Парсинг JSON-объекта
        private static Dictionary<string, object> ParseJsonObject(string json, ref int index)
        {
            var result = new Dictionary<string, object>();
            index++; // Пропуск '{'

            while (index < json.Length)
            {
                SkipWhitespace(json, ref index);

                // Если встретили '}', объект закончен
                if (json[index] == '}')
                {
                    index++; // Пропуск '}'
                    return result;
                }

                // Парсинг ключа (имени поля)
                string key = ParseJsonString(json, ref index);
                SkipWhitespace(json, ref index);

                // Проверка, что после ключа идёт ':'
                if (json[index] != ':')
                {
                    throw new FormatException("Отсутствует симмвол ':' ");
                }

                index++; // Пропуск ':'

                // Парсинг значения
                object value = ParseJsonValue(json, ref index);
                result[key] = value; // Добавление пары ключ-значение в словарь

                SkipWhitespace(json, ref index);

                // Если встретили ',', значит, есть ещё поля
                if (json[index] == ',')
                {
                    index++; // Пропуск ','
                }
                else if (json[index] != '}') // Если не ',' и не '}', это ошибка
                {
                    throw new FormatException("Отсутствует символ '}'");
                }
            }

            // Если JSON-строка закончилась до завершения объекта
            throw new FormatException("Неверный объект");
        }

        // Парсинг JSON-массива
        private static List<Dictionary<string, object>> ParseJsonArray(string json, ref int index)
        {
            var result = new List<Dictionary<string, Object>>();
            index++; // Пропуск '['

            while (index < json.Length)
            {
                SkipWhitespace(json, ref index);

                // Если встретили ']', массив закончен
                if (json[index] == ']')
                {
                    index++; // Пропуск ']'
                    return result;
                }

                // Парсинг элемента массива
                Dictionary<string, object> value = (Dictionary<string, object>)ParseJsonValue(json, ref index);
                result.Add(value); // Добавление элемента в массив

                SkipWhitespace(json, ref index);

                // Если встретили ',', значит, есть ещё элементы
                if (json[index] == ',')
                {
                    index++; // Пропуск ','
                }
                else if (json[index] != ']') // Если не ',' и не ']', это ошибка
                {
                    throw new FormatException("Отсутствует символ ']'");
                }
            }

            // Если JSON-строка закончилась до завершения массива
            throw new FormatException("Неверный список данных");
        }

        // Парсинг JSON-строки
        private static string ParseJsonString(string json, ref int index)
        {
            index++; // Пропуск '"'
            var result = new StringBuilder();

            while (index < json.Length)
            {
                char currentChar = json[index];

                // Если встретили '"', строка закончена
                if (currentChar == '"')
                {
                    index++; // Пропуск '"'
                    return result.ToString();
                }

                // Добавление символа в результат
                result.Append(currentChar);
                index++;
            }

            // Если JSON-строка закончилась до завершения строки
            throw new FormatException("Неправильный формат строки");
        }

        // Парсинг JSON-числа
        private static object ParseJsonNumber(string json, ref int index)
        {
            var startIndex = index;

            // Чтение всех символов, которые могут быть частью числа
            while (index < json.Length && (char.IsDigit(json[index]) || json[index] == '-' || json[index] == '.'))
            {
                index++;
            }

            // Извлечение подстроки, представляющей число
            string numberString = json.Substring(startIndex, index - startIndex);

            // Попытка преобразовать в целое число
            if (int.TryParse(numberString, out int intValue))
            {
                return intValue;
            }

            // Попытка преобразовать в число с плавающей точкой
            if (double.TryParse(numberString, out double doubleValue))
            {
                return doubleValue;
            }

            // Если преобразование не удалось, выбрасываем исключение
            throw new FormatException($"Недопустимый формат числа: {numberString}");
        }

        // Парсинг JSON-булева значения
        private static bool ParseJsonBoolean(string json, ref int index)
        {
            // Проверка на "true"
            if (json.Substring(index, 4) == "true")
            {
                index += 4;
                return true;
            }
            // Проверка на "false"
            else if (json.Substring(index, 5) == "false")
            {
                index += 5;
                return false;
            }

            // Если значение не распознано, выбрасываем исключение
            throw new FormatException("Недопустимое значение");
        }

        // Парсинг JSON-значения null
        private static object ParseJsonNull(string json, ref int index)
        {
            // Проверка на "null"
            if (json.Substring(index, 4) == "null")
            {
                index += 4;
                return null;
            }

            // Если значение не распознано, выбрасываем исключение
            throw new FormatException("Недопустимое значение");
        }

        // Пропуск пробелов и других незначимых символов
        private static void SkipWhitespace(string json, ref int index)
        {
            while (index < json.Length && char.IsWhiteSpace(json[index]))
            {
                index++;
            }
        }
    }
}
