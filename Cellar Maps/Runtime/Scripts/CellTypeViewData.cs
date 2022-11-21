using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    [Serializable]
    public sealed class CellTypeViewData
    {
        public CellTypeViewData(CellType type)
        {
            _type = type;
            _color = new(30f / 255f, 30f / 255f, 30f / 255f, 1);
        }

        /// <summary>
        /// Тип клетки.
        /// </summary>
        public CellType Type => _type;
        /// <summary>
        /// Название, с помощью которого можно идентифицировать клетку для дальнейшего преобразования.
        /// </summary>
        public string TypeName
        {
            get => Type.TypeName;
            set
            {
                Type.TypeName = value;
                ViewDataUpdated?.Invoke();
            }
        }
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                ViewDataUpdated?.Invoke();
            }
        }

        public event Action ViewDataUpdated;

        [SerializeField] private Color _color;
        [SerializeReference] private CellType _type;
    }
}
