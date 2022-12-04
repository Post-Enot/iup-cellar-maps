using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Визуальный элемент индикатора типа клетки клеточной карты.
    /// </summary>
    public sealed class CellTypeIndicator : Label, ICellTypeIndicator
    {
        public CellTypeIndicator()
        {
            AddToClassList("cm-cell-type-indicator");
        }

        public void SetViewData(ICellTypeViewData viewData)
        {
            text = viewData.CellTypeName;
            style.backgroundColor = viewData.Color;
            style.color = style.backgroundColor.value.MakeContrast(resolvedStyle.color);
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<CellTypeIndicator, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
