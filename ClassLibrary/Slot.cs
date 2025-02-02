using System;
using System.Collections.Generic;


namespace ClassLibrary
{
    public class Slot : IJsonObject
    {
        public string? Id { get; private set; }
        public string? Label { get; private set; }
        public string? Description { get; private set; }
        public Required? Required { get; private set; }
        public string? ActionId { get; private set; }

        // Основной конструктор с параметрами
        public Slot(string id, string label, string description, Required required, string actionId)
        {
            this.Id = id;
            this.Label = label;
            this.Description = description;
            this.Required = required;
            this.ActionId = actionId;
        }

        public Slot(Dictionary<string, object> dict)
        {
            foreach (var kvp in dict)
            {
                switch (kvp.Key)
                {
                    case "id":
                        Id = kvp.Value?.ToString();
                        break;
                    case "label":
                        Label = kvp.Value?.ToString();
                        break;
                    case "description":
                        Description = kvp.Value?.ToString();
                        break;
                    case "required":
                        if (kvp.Value is Dictionary<string, object> requiredDict)
                        {
                            // Конвертируем Dictionary<string, object> в Dictionary<string, int>
                            var convertedDict = new Dictionary<string, int>();
                            foreach (var entry in requiredDict)
                            {
                                if (entry.Value is int intValue)
                                {
                                    convertedDict[entry.Key] = intValue;
                                }
                                else
                                {
                                    throw new InvalidCastException($"Неверный тип значения для ключа '{entry.Key}' в поле 'required'. Ожидается int.");
                                }
                            }
                            Required = new Required(convertedDict);
                        }
                        break;
                    case "actionid":
                        ActionId = kvp.Value?.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
        // Конструктор без параметров для неявного вызова
        public Slot()
        {
            Id = null;
            Label = null;
            Description = null;
            Required = null;
            ActionId = null;
        }

        /// <summary>
        /// Метод интерфейса IJSONObject
        /// </summary>
        /// <returns>Список всех полей объекта Slots</returns>
        public IEnumerable<string> GetAllFields()
        {
            return ["id", "label", "description", "required", "actionid"];
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
                "id" => Id,
                "label" => Label,
                "description" => Description,
                "required" => Required is null ? null : Required.ToString(),
                "actionid" => ActionId,
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
                case "id": Id = value; break;
                case "label": Label = value; break;
                case "description": Description = value; break;
                case "actionid": ActionId = value; break;
                default: throw new KeyNotFoundException();
            }
        }
        public override string? ToString()
        {
            return $"id: {Id}, label: {Label}, description: {Description}, required : ";
        }

    }
}
