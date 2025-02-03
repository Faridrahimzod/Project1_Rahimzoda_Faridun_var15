///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;


namespace ClassLibrary
{

    public static class JsonParser
    {
        // Метод для записи JSON в поток вывода
        public static string WriteJson(IJsonObject obj)
        {
            return ParsingType.WriteJsonObject(obj);
        }

        public static IJsonObject ReadJson(string jsonString)
        {
            int index = 0;
            var result = ParsingType.ParseJsonValue(jsonString, ref index);

            if (result is Dictionary<string, object> parsedData)
            {
                return new VaultData(parsedData);
            }

            throw new FormatException("Ожидается объект JSON.");
        }
    }
}
