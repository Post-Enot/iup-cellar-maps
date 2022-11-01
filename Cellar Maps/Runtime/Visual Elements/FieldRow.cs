using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace CellarMaps.UI
{
	public sealed class FieldRow : VisualElement
	{
		public FieldRow()
		{
			AddToClassList("cm-field-row");
		}

		public IReadOnlyList<FieldCell> FieldCells => _fieldCells;

		private FieldCell[] _fieldCells;

		public void FillWithCells(int yCoordinate, int cellCount, Action<Vector2Int> interactionCallback)
		{
			if (_fieldCells != null)
			{
				DeleteCells();
            }
			_fieldCells = new FieldCell[cellCount];
            for (int i = 0; i < cellCount; i += 1)
			{
				_fieldCells[i] = new FieldCell(i, yCoordinate, interactionCallback);
				Add(_fieldCells[i]);
            }
		}

		public void DeleteCells()
		{
			foreach (FieldCell fieldCell in _fieldCells)
			{
                Remove(fieldCell);
            }
		}

		#region UXML
		[Preserve]
		public new sealed class UxmlFactory : UxmlFactory<FieldRow, UxmlTraits> { }

		[Preserve]
		public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
		#endregion
	}
}
