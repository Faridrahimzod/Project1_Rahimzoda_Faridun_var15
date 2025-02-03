///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using System.Xml;

namespace ClassLibrary
{
    public class VaultData : IJsonObject
    {
        public Dictionary <string, List<Element>>? Elements { get; private set; }

        // Создаём конструкторы
        public VaultData() 
        {
            Elements = new Dictionary<string, List<Element>>();
        }
        public VaultData(Dictionary<string, object> parsedData)
        {
            Elements = new Dictionary<string, List<Element>>();

            if (parsedData.TryGetValue("elements", out object elementsObj) && elementsObj is List<object> elementsList)
            {
                List<Element> elements = new List<Element>();
                foreach (var elementObj in elementsList)
                {
                    if (elementObj is Dictionary<string, object> elementDict)
                    {
                        elements.Add(new Element(elementDict));
                    }
                }
                Elements["elements"] = elements;
            }
        }

        /// <summary>
        /// Метод интерфейса IJSONObject
        /// </summary>
        /// <returns>Список всех полей объекта VaultData</returns>
        public IEnumerable<string> GetAllFields()
        {
            return new List<string> { "elements" };
        }

        /// <summary>
        /// Метод интерфейса IJSONObject
        /// </summary>
        /// <param name="fieldName">Имя поля</param>
        /// <returns>Значение указанного поля, если такого поля нет, возвращает null</returns>
        public string? GetField(string fieldName)
        {
            return fieldName switch
            {
                "elements" => Elements is null ? null : string.Join(", ", Elements),
                _ => null
            };
        }

        /// <summary>
        /// Метод интерфейса IJSONObject
        /// </summary>
        /// <param name="fieldName">Поле, которму будет присваиватся значение</param>
        /// <param name="value">Значение</param>
        /// <exception cref="NotImplementedException">При отсутствии поля</exception>
        public void SetField(string fieldName, string value)
        {
            switch (fieldName)
            {
                case "elements": break;
                    
                default: throw new KeyNotFoundException();
            }
        }
        /// <summary>
        /// Метод для передачи ключей и значений из объекта
        /// </summary>
        /// <returns>Словарь, где ключи - поля, значения - значения полей</returns>
        public Dictionary<string, object> ToSerializableObject() 
        {
            return Elements.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
        }

    }
}
