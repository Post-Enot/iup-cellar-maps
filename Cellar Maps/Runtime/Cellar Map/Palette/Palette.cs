using System;
using System.Collections;
using System.Collections.Generic;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Палитра, содержащая набор уникальных типов клеток клеточной карты.
    /// </summary>
    public sealed class Palette : IPalette
    {
        private readonly Dictionary<string, CellType> _cellTypeByTypeName = new();

        public int Count => _cellTypeByTypeName.Count;

        public ICellType this[string cellTypeName] => _cellTypeByTypeName[cellTypeName];

        /// <summary>
        /// Создаёт новый тип клетки и добавляет его в палитру.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки. Должно быть уникальным, иначе вызовет 
        /// исключение ArgumentException.</param>
        /// <returns>Возвращает ссылку на созданный тип клетки.</returns>
        public void Add(string cellTypeName)
        {
            if (_cellTypeByTypeName.ContainsKey(cellTypeName))
            {
                throw new ArgumentException(
                    "Палитра уже содержит тип клетки с переданным названием типа.", nameof(cellTypeName));
            }
            var type = new CellType(cellTypeName);
            _cellTypeByTypeName.Add(cellTypeName, type);
        }

        /// <summary>
        /// Удаляет передаванный тип клетки из палитры.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        public void Remove(string cellTypeName)
        {
            bool isRemoveSuccessful = _cellTypeByTypeName.Remove(cellTypeName);
            if (!isRemoveSuccessful)
            {
                throw new ArgumentException(
                    "Отсутствует тип клетки с переданным названием типа в палитре.", nameof(cellTypeName));
            }
        }

        public bool Contains(string cellTypeName)
        {
            return _cellTypeByTypeName.ContainsKey(cellTypeName);
        }

        public bool RenameCellType(string cellTypeName, string newTypeName)
        {
            if (_cellTypeByTypeName.ContainsKey(newTypeName))
            {
                return false;
            }
            CellType cellType = _cellTypeByTypeName[cellTypeName];
            cellType.SetTypeName(newTypeName);
            _ = _cellTypeByTypeName.Remove(cellTypeName);
            _cellTypeByTypeName.Add(newTypeName, cellType);
            return true;
        }

        public IEnumerator<ICellType> GetEnumerator()
        {
            return _cellTypeByTypeName.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cellTypeByTypeName.Values.GetEnumerator();
        }
    }
}
