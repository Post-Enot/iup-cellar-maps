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
        public int Count => _cellTypeByName.Count;

        private readonly Dictionary<string, CellType> _cellTypeByName = new();

        public IReadOnlyCellType this[string cellTypeName] => _cellTypeByName[cellTypeName];

        public bool Contains(string cellTypeName)
        {
            return _cellTypeByName.ContainsKey(cellTypeName);
        }

        public void Add(string cellTypeName)
        {
            if (_cellTypeByName.ContainsKey(cellTypeName))
            {
                throw NewCellTypeWithNameAlreadyExist(
                    nameof(cellTypeName),
                    cellTypeName);
            }
            var cellType = new CellType(cellTypeName);
            _cellTypeByName.Add(cellTypeName, cellType);
        }

        public void Remove(string cellTypeName)
        {
            bool isRemoveSuccessful = _cellTypeByName.Remove(cellTypeName);
            if (!isRemoveSuccessful)
            {
                throw NewCellTypeWithNameDoesNotExist(
                    nameof(cellTypeName),
                    cellTypeName);
            }
        }

        public void RenameCellType(string oldCellTypeName, string newCellTypeName)
        {
            CellType cellType = _cellTypeByName[oldCellTypeName];
            bool isRemoveSuccessfully = _cellTypeByName.Remove(oldCellTypeName);
            if (!isRemoveSuccessfully)
            {
                throw NewCellTypeWithNameDoesNotExist(
                    nameof(oldCellTypeName),
                    oldCellTypeName);
            }
            if (_cellTypeByName.ContainsKey(newCellTypeName))
            {
                throw NewCellTypeWithNameAlreadyExist(
                    nameof(newCellTypeName),
                    newCellTypeName);
            }
            cellType.SetName(newCellTypeName);
            _cellTypeByName.Add(newCellTypeName, cellType);
        }

        public IEnumerator<ICellType> GetEnumerator()
        {
            return _cellTypeByName.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cellTypeByName.Values.GetEnumerator();
        }

        private ArgumentException NewCellTypeWithNameAlreadyExist(
            string argumentName,
            string argumentValue)
        {
            return new ArgumentException(
                    $"Палитра уже содержит тип клетки с переданным названием типа ({argumentValue})",
                    argumentName);
        }

        private ArgumentException NewCellTypeWithNameDoesNotExist(
            string argumentName,
            string argumentValue)
        {
            return new ArgumentException(
                    $"Отсутствует тип клетки с переданным названием типа в палитре ({argumentValue}).",
                    argumentName);
        }
    }
}
