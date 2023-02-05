using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Визуальные данные типа клетки клеточной карты.
    /// </summary>
    public class CellTypeViewData : ICellTypeViewData
    {
        public CellTypeViewData(string cellTypeName, Color color)
        {
            CellTypeName = cellTypeName;
            Color = color;
        }

        public string CellTypeName { get; private set; }
        public Color Color { get; set; }

        /// <summary>
        /// Инициализирует свойство названия типа клетки.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки. Попытка присвоить значение null вызовет 
        /// ArgumentNullException.</param>
        public void SetTypeName(string cellTypeName)
        {
            if (cellTypeName == null)
            {
                new ArgumentNullException(nameof(cellTypeName));
            }
            CellTypeName = cellTypeName;
        }
    }
}
