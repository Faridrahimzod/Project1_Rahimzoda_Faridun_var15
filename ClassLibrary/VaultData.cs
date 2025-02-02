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
            this.Elements = null;
        }
        public VaultData(Dictionary<string, List<Dictionary<string, object>>> elements)
        {

            Dictionary<string, List<Element>> a = new Dictionary<string, List<Element>>();
            foreach (var listelement in elements.Keys)
            {
                List<Element> list = new List<Element>();
                foreach (var element in elements [listelement])
                {
                    list.Add(new Element(element));   
                }
                a[listelement] = list;
            }
            this.Elements = a;
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
                case "elements": 
                    
                default: throw new KeyNotFoundException();
            }
        }

    }
}
