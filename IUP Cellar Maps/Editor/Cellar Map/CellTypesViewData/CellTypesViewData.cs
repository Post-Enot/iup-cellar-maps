using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Визуальные данные типов клеток клеточной карты.
    /// </summary>
    public sealed class CellTypesViewData : ICellTypesViewData
    {
        public int Count => _viewData.Count;

        private readonly List<CellTypeViewData> _viewData = new();
        private readonly Dictionary<string, CellTypeViewData> _viewDataByCellTypeName = new();

        public ICellTypeViewData this[int index] => _viewData[index];
        public ICellTypeViewData this[string cellTypeName] => _viewDataByCellTypeName[cellTypeName];

        public bool Contains(string cellTypeName)
        {
            return _viewDataByCellTypeName.ContainsKey(cellTypeName);
        }

        public void MoveItemFromTo(int from, int to)
        {
            _viewData.MoveItemFromTo(from, to);
        }

        public void SetCellTypeColor(string cellTypeName, Color cellTypeColor)
        {
            _viewDataByCellTypeName[cellTypeName].Color = cellTypeColor;
        }

        /// <summary>
        /// Создаёт новые визуальные данные типа клетки.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки: должно быть уникальным.</param>
        /// <param name="cellTypeColor">Цвет типа клетки.</param>
        /// <returns>Возвращает визуальные данные типа клетки.</returns>
        public void Add(string cellTypeName, Color cellTypeColor)
        {
            if (_viewDataByCellTypeName.ContainsKey(cellTypeName))
            {
                throw new ArgumentException(
                    "Палитра уже содержит визуальные данные для типа клетки с переданным названием типа.",
                    nameof(cellTypeName));
            }
            var viewData = new CellTypeViewData(cellTypeName, cellTypeColor);
            _viewData.Add(viewData);
            _viewDataByCellTypeName.Add(cellTypeName, viewData);
        }

        /// <summary>
        /// Удаляет визуальные данные типа клетки по названию типа.
        /// </summary>
        /// <param name="cellTypeName">Название удаляемого типа клетки.</param>
        public void Remove(string cellTypeName)
        {
            if (!_viewDataByCellTypeName.ContainsKey(cellTypeName))
            {
                throw new ArgumentException(
                    "Отсутствуют визуальные данные для типа клетки с переданным названием типа.",
                    nameof(cellTypeName));
            }
            CellTypeViewData viewData = _viewDataByCellTypeName[cellTypeName];
            _ = _viewData.Remove(viewData);
            _ = _viewDataByCellTypeName.Remove(cellTypeName);
        }

        /// <summary>
        /// Изменяет название типа клетки, если это возможно.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        /// <param name="newCellTypeName">Новое название типа клетки.</param>
        /// <returns>Возвращает true, если название типа клетки было успешно изменено; иначе false.</returns>
        public bool RenameCellTypeViewData(string cellTypeName, string newCellTypeName)
        {
            if (_viewDataByCellTypeName.ContainsKey(newCellTypeName))
            {
                return false;
            }
            CellTypeViewData viewData = _viewDataByCellTypeName[cellTypeName];
            viewData.SetTypeName(newCellTypeName);
            _ = _viewDataByCellTypeName.Remove(cellTypeName);
            _viewDataByCellTypeName.Add(newCellTypeName, viewData);
            return true;
        }

        public IEnumerator<ICellTypeViewData> GetEnumerator()
        {
            return _viewData.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _viewData.GetEnumerator();
        }
    }
}
