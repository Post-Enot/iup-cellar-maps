using System;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Тип клетки клеточной карты.
    /// </summary>
    public sealed class CellType : ICellType
    {
        /// <summary>
        /// Инициализирует свойство названия типа клетки.
        /// </summary>
        /// <param name="name">Название типа клетки. Попытка присвоить значение null вызовет 
        /// ArgumentNullException.</param>
        public CellType(string name)
        {
            SetName(name);
        }

        public string Name { get; private set; }

        public void SetName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
