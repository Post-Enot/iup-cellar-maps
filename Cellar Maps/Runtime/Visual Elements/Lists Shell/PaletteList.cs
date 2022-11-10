using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP_Toolkits.CellarMaps.UI
{
    public sealed class PaletteList : ListViewShell<PaletteListElement, CellTypeViewData>
    {
        public PaletteList() : base()
        {
            InitListViewProperties();
            AddToClassList("cm-palette");
        }

        private void InitListViewProperties()
        {
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            showBorder = true;
            headerTitle = "Palette";
            showFoldoutHeader = true;
            showAddRemoveFooter = true;
            reorderable = true;
            reorderMode = ListViewReorderMode.Animated;
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<PaletteList, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
