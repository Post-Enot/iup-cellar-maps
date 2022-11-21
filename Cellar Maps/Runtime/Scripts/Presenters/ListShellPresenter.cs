using System.Collections.Generic;

namespace IUP.Toolkits.CellarMaps.UI
{
    public sealed class ListShellPresenter<T1, T2> : IPresenter
        where T1 : ListViewElement<T2>, new() where T2 : class
    {
        public ListShellPresenter(IListViewShellBindable<T2> model, ListViewShell<T1, T2> view)
        {
            Model = model;
            View = view;
            BindViewWithModel();
        }

        public IListViewShellBindable<T2> Model { get; }
        public ListViewShell<T1, T2> View { get; }

        public void OnEnable()
        {
            View.itemsAdded += AddItems;
            View.itemsRemoved += RemoveItems;
            View.itemIndexChanged += MoveItemFromTo;
            View.onSelectionChange += HandleLayersEventOnSelectionChange;
        }

        public void OnDisable()
        {
            View.itemsAdded -= AddItems;
            View.itemsRemoved -= RemoveItems;
            View.itemIndexChanged -= MoveItemFromTo;
            View.onSelectionChange -= HandleLayersEventOnSelectionChange;
        }

        private void AddItems(IEnumerable<int> indexes)
        {
            foreach (int _ in indexes)
            {
                Model.Add();
            }
            BindViewWithModel();
        }

        private void RemoveItems(IEnumerable<int> indexes)
        {
            foreach (int index in indexes)
            {
                Model.Remove(index);
            }
            BindViewWithModel();
        }

        private void MoveItemFromTo(int aItemIndex, int bItemIndex)
        {
            Model.MoveItemFromTo(aItemIndex, bItemIndex);
            BindViewWithModel();
        }

        private void HandleLayersEventOnSelectionChange(IEnumerable<object> selectedObjects)
        {
            IEnumerator<object> enumerator = selectedObjects.GetEnumerator();
            enumerator.MoveNext();
            if (enumerator.Current != null)
            {
                Model.UpdateSelectedItem(enumerator.Current as T2);
            }
        }

        private void BindViewWithModel()
        {
            var bindableList = Model.GetBindableList();
            View.BindWith(bindableList);
        }
    }
}
