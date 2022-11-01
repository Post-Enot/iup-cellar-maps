using System;
using UnityEngine;

namespace CellarMaps
{
    /// <summary>
    /// Класс, представляющий тип клетки, из которой состоят клеточные карты.
    /// </summary>
    [Serializable]
    public sealed class CellType
    {
        /// <summary>
        /// Название, с помощью которого можно идентифицировать клетку для дальнейшего преобразования.
        /// </summary>
        public string TypeName
        {
            get => _typeName;
            set => _typeName = value;
        }

        [SerializeField] private string _typeName = "noname";
    }
}
