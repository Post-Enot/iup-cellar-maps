using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// UI-представление списка слоёв клеточной карты.
    /// </summary>
    public sealed class LayerList : ListView, ILayerList
    {
        public LayerList() : base()
        {
            AddToClassList("cm-layer-list");
            InitUI_Properties();
            makeItem = () =>
            {
                var element = new LayerListElement();
                element.ViewDataChanged += Element_ViewDataChanged;
                return element;
            };
            bindItem = (VisualElement item, int i) =>
            {
                var layerListElement = item as LayerListElement;
                layerListElement.BindWith(itemsSource[i] as ILayerViewData);
            };
            itemsAdded += HandleItemsAddedEvent;
            itemsRemoved += HandleItemsRemovedEvent;
            itemIndexChanged += HandleItemIndexChangedEvent;
            onSelectionChange += HandleOnSelectionChangeEvent;
        }

        public event Action<int, string, Color> ViewDataChanged;
        public event Action<int> Added;
        public event Action<int> Removed;
        public event Action<int, int> MovedFromTo;
        public event Action<int> Selected;
        public event Action Unselected;

        public void BindWith(IReadOnlyList<ILayerViewData> viewDataList)
        {
            itemsSource = viewDataList.ToArray();
        }

        private void Element_ViewDataChanged(
            ILayerViewData viewData,
            string newCellTypeName,
            Color cellTypeColor)
        {
            ViewDataChanged?.Invoke(itemsSource.IndexOf(viewData), newCellTypeName, cellTypeColor);
        }

        private void HandleOnSelectionChangeEvent(IEnumerable<object> elements)
        {
            IEnumerator<object> enumerator = elements.GetEnumerator();
            enumerator.MoveNext();
            if (enumerator.Current != null)
            {
                int layerIndex = itemsSource.IndexOf(enumerator.Current as ILayerViewData);
                Selected?.Invoke(layerIndex);
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
            headerTitle = "Layers";
            showFoldoutHeader = true;
            showAddRemoveFooter = true;
            reorderable = true;
            reorderMode = ListViewReorderMode.Animated;
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<LayerList, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
