using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.UI
{
    public sealed class CellarMap : VisualElement
    {
        public CellarMap()
        {
            AddToClassList("cm-cellar-map");
        }

        public event Action<Vector2Int> InteractWithCell;

        private CellarMapRow[] _rows;

        public CellarMapCell this[int x, int y] => _rows[y].FieldCells[x];
        public CellarMapCell this[Vector2Int coordinate] => _rows[coordinate.y].FieldCells[coordinate.x];

        public void CreateMap(int width, int height)
        {
            if (_rows != null)
            {
                ClearMap();
            }
            _rows = new CellarMapRow[height];
            for (int i = 0; i < _rows.Length; i += 1)
            {
                _rows[i] = new CellarMapRow();
                Add(_rows[i]);
                _rows[i].FillWithCells(i, width, HandleInteractWithCell);
            }
        }

        public void ClearMap()
        {
            if (_rows != null)
            {
                foreach (CellarMapRow row in _rows)
                {
                    row.DeleteCells();
                    Remove(row);
                }
            }
        }

        private void HandleInteractWithCell(Vector2Int coordinate)
        {
            InteractWithCell?.Invoke(coordinate);
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<CellarMap, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
