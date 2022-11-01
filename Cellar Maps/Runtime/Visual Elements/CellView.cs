using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace CellarMaps.UI
{
    public sealed class CellView : VisualElement
    {
        public CellView()
        {
            AddToClassList("cm-cell-view");
            ColorField = new ColorField();
            NameTextField = new TextField();
        }

        /// <summary>
        /// Визуальные данные о клетке.
        /// </summary>
        public CellTypeViewData ViewData { get; private set; }
        public ColorField ColorField { get; private set; }
        public TextField NameTextField { get; private set; }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<CellView, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
