using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.UI
{
    public sealed class CellarMapRow : VisualElement
    {
        public CellarMapRow()
        {
            AddToClassList("cm-cellar-map-row");
        }

        public IReadOnlyList<CellarMapCell> FieldCells => _fieldCells;

        private CellarMapCell[] _fieldCells;

        public void FillWithCells(int yCoordinate, int cellCount, Action<Vector2Int> interactionCallback)
        {
            if (_fieldCells != null)
            {
                DeleteCells();
            }
            _fieldCells = new CellarMapCell[cellCount];
            for (int i = 0; i < cellCount; i += 1)
            {
                _fieldCells[i] = new CellarMapCell(i, yCoordinate, interactionCallback);
                Add(_fieldCells[i]);
            }
        }

        public void DeleteCells()
        {
            foreach (CellarMapCell fieldCell in _fieldCells)
            {
                Remove(fieldCell);
            }
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<CellarMapRow, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
