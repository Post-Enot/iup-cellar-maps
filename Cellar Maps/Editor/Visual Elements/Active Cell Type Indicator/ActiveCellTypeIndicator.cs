using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Визуальный элемент индикатора активного типа клетки клеточной карты.
    /// </summary>
    public sealed class ActiveCellTypeIndicator : VisualElement, IActiveCellTypeIndicator
    {
        public ActiveCellTypeIndicator()
        {
            AddToClassList("cm-active-cell-type-indicator");
            _label = new Label()
            {
                text = "Active Cell Type"
            };
            _label.AddToClassList("cm-active-cell-type-indicator__label");
            _cellTypeIndicator = new CellTypeIndicator();
            _cellTypeIndicator.AddToClassList("cm-active-cell-type-indicator__indicator");
            Add(_label);
            Add(_cellTypeIndicator);
        }

        private readonly Label _label;
        private readonly CellTypeIndicator _cellTypeIndicator;

        public void SetViewData(ICellTypeViewData viewData)
        {
            _cellTypeIndicator.SetViewData(viewData);
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<ActiveCellTypeIndicator, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
