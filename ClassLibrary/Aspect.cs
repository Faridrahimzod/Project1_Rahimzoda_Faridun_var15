///Рахимзода Фаридун Тоджиддин
///БПИ244-1
///Вариант 15

using System;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace ClassLibrary
{
    public class Aspect : IJsonObject
    {
        public int? Vault { get; private set; }
        public int? Location { get; private set; }
        public int? VaultCapital { get; private set; }
        // Основные конструкторы с параметрами
        public Aspect(int vault, int location, int vaultCapital)
        {
            this.Vault = vault;
            this.Location = location;
            this.VaultCapital = vaultCapital;
        }

        public Aspect(Dictionary<string, int> dict)
        {
            foreach (var kvp in dict)
            {
                switch (kvp.Key)
                {
                    case "vault": Vault = kvp.Value; break;
                    case "location": Location = kvp.Value; break;
                    case "vaultcapital": VaultCapital = kvp.Value; break;
                }
            }
        }
        // Конструктор без параметров для неявного вызова
        public Aspect()
        {
            Vault = 0;
            Location = 0;
            VaultCapital = 0;
        }

        /// <summary>
        /// Метод интерфейса IJSONObject
        /// </summary>
        /// <returns>Список всех полей объекта Aspect</returns>
        public IEnumerable<string>? GetAllFields()
        {
            return ["vault", "location", "vaultcapital"];
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
                "vault" => Vault.ToString(),
                "location" => Location.ToString(),
                "vaultCapital" => VaultCapital.ToString(),
                _ => null,

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
            try
            {
                switch (fieldName)
                {
                    case "vault": Vault = Int32.Parse(value); break;
                    case "location": Location = Int32.Parse(value); break;
                    case "vaultcapital": VaultCapital = Int32.Parse(value); break;
                    default: throw new KeyNotFoundException();
                }
            } catch { throw new Exception(); }
        }

        public override string? ToString()
        {
            return $"vault: {Vault}, location: {Location}, vaultcapital: {VaultCapital}";
        }
    }
}
