using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public interface IJsonObject
    {
        /// <summary>
        /// Возвращает коллекцию строк, представляющую имена всех полей объекта JSON.
        /// </summary>
        IEnumerable<string>? GetAllFields();

        /// <summary>
        /// Возвращает значение поля с указанным именем в формате строки.
        /// Если поле отсутствует, возвращает null.
        /// </summary>
        string? GetField(string fieldName);

        /// <summary>
        /// Присваивает полю с указанным именем значение value.
        /// Если поле отсутствует, генерирует исключение KeyNotFoundException.
        /// </summary>
        void SetField(string fieldName, string value);
    }
}