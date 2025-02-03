///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class Required : IJsonObject
    {
        public int Follower { get; private set; }
        public int Funds { get; private set; }

        // Основные конструкторы с параметрами и без
        public Required(int follower = 0, int funds = 0)
        {
            Follower = follower;
            Funds = funds;
        }
        public Required(Dictionary<string, int> properties)
        {
            foreach (var property in properties.Keys)
            {
                switch (property)
                {
                    case "follower": Follower = properties[property]; break;
                    case "funds": Funds = properties[property]; break;
                    default: break;
                }
            }
        }


        /// <summary>
        /// Метод интерфейса IJSONObject
        /// </summary>
        /// <returns>Список всех полей объекта Required</returns>
        public IEnumerable<string> GetAllFields()
        {
            List<string> fields = new List<string> { };
            if (Follower > 0)
            {
                fields.Add("follower");
            }
            if (Funds > 0)
            {
                fields.Add("funds");
            }
            return fields;
        }

        /// <summary>
        /// Метод интерфейса IJSONObject
        /// </summary>
        /// <param name="fieldName">Имя поля</param>
        /// <returns>Значение указанного поля, если такого поля нет, возвращает null</returns>
        public string? GetField(string fieldName)
        {
            switch (fieldName)
            {
                case "follower": return Follower.ToString();
                case "funds": return Funds.ToString();
                default: return null;
            }
        }

        /// <summary>
        /// Метод интерфейса IJSONObject
        /// </summary>
        /// <param name="fieldName">Поле, которму будет присваиватся значение</param>
        /// <param name="value">Значение</param>
        /// <exception cref="NotImplementedException">При отсутствии поля</exception>
        public void SetField(string fieldName, string value)
        {
            try
            {
                switch (fieldName)
                {
                    case "follower": Follower = Int32.Parse(value); break;
                    case "funds": Funds = Int32.Parse(value); break;
                    default: throw new KeyNotFoundException();
                }
            } catch { throw new Exception(); }
        }

        public override string ToString()
        {
            string res = "";
            res += Follower > 0 ? $"follower : {Follower} " : "";
            res += Funds > 0 ? $"funds : {Funds} " : "";
            return res;
        }
    }
}
