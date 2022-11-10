using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP_Toolkits.CellarMaps.UI
{
    public sealed class LayersList : ListViewShell<LayersListElement, LayerViewData>
    {
        public LayersList() : base()
        {
            InitListViewProperties();
            AddToClassList("cm-layers");
        }

        private void InitListViewProperties()
        {
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            showBorder = true;
            headerTitle = "Layers";
            showFoldoutHeader = true;
            showAddRemoveFooter = true;
            reorderable = true;
            reorderMode = ListViewReorderMode.Animated;
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<LayersList, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
