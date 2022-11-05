using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP_Toolkits.CellarMaps.UI
{
    public sealed class Palette : ListView
    {
        public Palette()
        {
            InitListViewProperties();
            AddToClassList("cm-palette");
        }

        private ViewPalette _viewPalette;

        public void BindWith(ViewPalette viewPalette)
        {
            _viewPalette = viewPalette;
            var itemsSource = new List<CellTypeViewData>(viewPalette.ViewDataOrder.Count);
            for (int i = 0; i < viewPalette.ViewDataOrder.Count; i += 1)
            {
                itemsSource.Add(viewPalette.ViewDataOrder[i]);
            }
            this.itemsSource = itemsSource;
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
            VisualElement MakeItem() => new PaletteElement();
            void BindItem(VisualElement item, int i)
            {
                var paletteElement = item as PaletteElement;
                paletteElement.BindWith(_viewPalette.ViewDataOrder[i]);
            }
            makeItem = MakeItem;
            bindItem = BindItem;
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<Palette, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
