using System;
using System.Collections.Generic;


namespace ClassLibrary
{
    public class Element : IJsonObject
    {
        public string? Id { get; private set; }
        public string? Label { get; private set; }
        public Aspect? Aspects { get; private set; }
        public List<Slot>? Slots { get; private set; }
        public string? Description { get; private set; }
        public bool Unique { get; private set; }

        // Основной конструктор с параметрами
        public Element(string id, string label, Aspect aspects, List<Slot> slots, string description, bool unique)
        {
            this.Id = id;
            this.Label = label;
            this.Aspects = aspects;
            this.Slots = slots;
            this.Description = description;
            this.Unique = unique;
        }

        public Element(Dictionary<string, object> pairs)
        {
            foreach (var kvp in pairs)
            {
                switch (kvp.Key)
                {
                    case "id":
                        if (kvp.Value is string id) Id = id;
                        break;
                    case "label":
                        if (kvp.Value is string label) Label = label;
                        break;
                    case "aspects":
                        if (kvp.Value is Dictionary<string, object> aspectsDict)
                            Aspects = new Aspect(aspectsDict.ToDictionary(k => k.Key, v => Convert.ToInt32(v.Value)));
                        break;
                    case "slots":
                        if (kvp.Value is List<object> slotsList)
                        {
                            Slots = slotsList.ConvertAll(slotObj =>
                            {
                                if (slotObj is Dictionary<string, object> slotDict)
                                    return new Slot(slotDict);
                                return new Slot();
                            });
                        }
                        break;
                    case "description":
                        if (kvp.Value is string desc) Description = desc;
                        break;
                    case "unique":
                        if (kvp.Value is bool unique) Unique = unique;
                        break;
                }
            }
        }
        // Для неявного вызова конструктора без параметров
        public Element() 
        {
            Id = null;
            Label = null;
            Aspects = null;
            Slots = null;
            Description = null;
            Unique = false;
        }
        /// <summary>
        /// Метод интерфейса IJSONObject
        /// </summary>
        /// <returns>Список всех полей объекта Element</returns>
        public IEnumerable<string> GetAllFields()
        {
            return new List<string> { "id", "label", "aspects", "slots", "description", "unique" };
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
                "aspects" => Aspects is null ? null : Aspects.ToString(),
                "slots" => Slots is null ? null : string.Join(", ", Slots),
                "description" => Description,
                "unique" => Unique.ToString(),
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
            switch(fieldName)
            { 
                case "id": Id = value; break;
                case "label": Label = value; break;
                case "description": Description = value; break;
                default: throw new KeyNotFoundException();
            }
        }

        public override string ToString()
        {
            return $"id : {Id}, label : {Label}, aspects : {Aspects.ToString()}, slots : {string.Join(", ", Slots)}, description : {Description}, unique : {Unique.ToString()}";
        }
    }

}
