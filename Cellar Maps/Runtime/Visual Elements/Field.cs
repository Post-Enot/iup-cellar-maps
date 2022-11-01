using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace CellarMaps.UI
{
	public sealed class Field : VisualElement
	{
		public Field()
		{
			AddToClassList("cm-field");
		}

		public FieldCell this[int y, int x] => _fieldRows[y].FieldCells[x];
		public FieldCell this[Vector2Int coordinate] => _fieldRows[coordinate.y].FieldCells[coordinate.x];

		private FieldRow[] _fieldRows;

		public event Action<Vector2Int> InteractWithCell;

		public void CreateField(int width, int height)
		{
			if (_fieldRows != null)
			{
				RemoveField();
			}
			_fieldRows = new FieldRow[height];
			for (int i = 0; i < _fieldRows.Length; i += 1)
			{
				_fieldRows[i] = new FieldRow();
				Add(_fieldRows[i]);
				_fieldRows[i].FillWithCells(i, width, HandleInteractWithCell);
			}
		}

		public void RemoveField()
		{
			if (_fieldRows != null)
			{
				foreach (FieldRow row in _fieldRows)
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
		public new sealed class UxmlFactory : UxmlFactory<Field, UxmlTraits> { }

		[Preserve]
		public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
		#endregion
	}
}
