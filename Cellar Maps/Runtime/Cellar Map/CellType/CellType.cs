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
        /// <param name="typeName">Название типа клетки. Попытка присвоить значение null вызовет 
        /// ArgumentNullException.</param>
        public CellType(string typeName)
        {
            SetTypeName(typeName);
        }

        public string TypeName { get; private set; }

        /// <summary>
        /// Инициализирует свойство названия типа клетки.
        /// </summary>
        /// <param name="typeName">Название типа клетки. Попытка присвоить значение null вызовет 
        /// ArgumentNullException.</param>
        public void SetTypeName(string typeName)
        {
            TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        }
    }
}
