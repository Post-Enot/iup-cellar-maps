using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// UI-представление палитры клеточной карты.
    /// </summary>
    public sealed class Palette : ListView, IPalette
    {
        public Palette() : base()
        {
            AddToClassList("cm-palette");
            InitUI_Properties();
            makeItem = () =>
            {
                var element = new PaletteElement();
                element.ViewDataChanged += Element_ViewDataChanged;
                return element;
            };
            bindItem = (VisualElement item, int i) =>
            {
                var paletteElement = item as IPaletteElement;
                paletteElement.BindWith(itemsSource[i] as ICellTypeViewData);
            };
            itemsAdded += HandleItemsAddedEvent;
            itemsRemoved += HandleItemsRemovedEvent;
            itemIndexChanged += HandleItemIndexChangedEvent;
            selectionChanged += HandleOnSelectionChangeEvent;
        }

        public event Action<string, string, Color> ViewDataChanged;
        public event Action<int> Added;
        public event Action<int> Removed;
        public event Action<int, int> MovedFromTo;
        public event Action<ICellTypeViewData> Selected;
        public event Action Unselected;

        public void BindWith(IReadOnlyList<ICellTypeViewData> viewDataList)
        {
            itemsSource = viewDataList.ToArray();
        }

        private void Element_ViewDataChanged(
            string cellTypeName,
            string newCellTypeName,
            Color cellTypeColor)
        {
            ViewDataChanged?.Invoke(cellTypeName, newCellTypeName, cellTypeColor);
        }

        private void HandleOnSelectionChangeEvent(IEnumerable<object> elements)
        {
            IEnumerator<object> enumerator = elements.GetEnumerator();
            enumerator.MoveNext();
            if (enumerator.Current != null)
            {
                Selected?.Invoke(enumerator.Current as ICellTypeViewData);
            }
            else
            {
                Unselected?.Invoke();
            }
        }

        private void HandleItemsAddedEvent(IEnumerable<int> itemIndexes)
        {
            foreach (int itemIndex in itemIndexes)
            {
                Added?.Invoke(itemIndex);
            }
        }

        private void HandleItemsRemovedEvent(IEnumerable<int> itemIndexes)
        {
            foreach (int itemIndex in itemIndexes)
            {
                Removed?.Invoke(itemIndex);
            }
        }

        private void HandleItemIndexChangedEvent(int from, int to)
        {
            MovedFromTo?.Invoke(from, to);
        }

        private void InitUI_Properties()
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
        public new sealed class UxmlFactory : UxmlFactory<Palette, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
