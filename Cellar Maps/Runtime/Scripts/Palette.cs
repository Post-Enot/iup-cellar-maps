using System;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Класс палитры, содержащий набор уникальных типов клеток.
    /// </summary>
    [Serializable]
    public sealed class Palette : ISerializationCallbackReceiver
    {
        /// <summary>
        /// Коллекция из всех типов клеток.
        /// </summary>
        public IReadOnlyCollection<CellType> CellTypes => _cellTypes;

        private HashSet<CellType> _cellTypes = new();

        [SerializeReference] private CellType[] _scellTypes;

        /// <summary>
        /// Вызывается при удалении типа клетки из палитры.
        /// </summary>
        public event Action<CellType> CellTypeRemovedFromPalette;

        /// <summary>
        /// Создаёт новый тип клетки и помещает его в палитру.
        /// </summary>
        /// <returns>Возвращает ссылку на созданный тип клетки.</returns>
        public CellType CreateNewCellType()
        {
            var type = new CellType();
            _cellTypes.Add(type);
            return type;
        }

        /// <summary>
        /// Проверяет, содержится ли передаваемый тип клетки в палитре.
        /// </summary>
        /// <param name="type">Ссылка на тип клетки.</param>
        /// <returns>Возвращает true, если переданный экземпляр содержится в палитре; иначе false.</returns>
        public bool Contains(CellType type)
        {
            return _cellTypes.Contains(type);
        }

        /// <summary>
        /// Удаляет передаваемый тип клетки из палитры.
        /// </summary>
        /// <param name="type">Ссылка на тип клетки.</param>
        /// <returns>Возвращает true, если переданный тип клетки содержался в палитре
        /// и удаление произошло успешно; иначе false.</returns>
        public bool Remove(CellType type)
        {
            bool result = _cellTypes.Remove(type);
            CellTypeRemovedFromPalette?.Invoke(type);
            return result;
        }

        public void OnBeforeSerialize()
        {
            _scellTypes = new CellType[_cellTypes.Count];
            _cellTypes.CopyTo(_scellTypes);
        }

        public void OnAfterDeserialize()
        {
            _cellTypes = new HashSet<CellType>();
            for (int i = 0; i < _scellTypes.Length; i += 1)
            {
                _cellTypes.Add(_scellTypes[i]);
            }
        }
    }
}
