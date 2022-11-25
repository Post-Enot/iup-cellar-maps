using System;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    [Serializable]
    public sealed class ViewPalette : ISerializationCallbackReceiver, IListViewShellBindable<CellTypeViewData>
    {
        public ViewPalette(Palette palette)
        {
            _palette = palette;
            _viewDataOrder = new List<CellTypeViewData>(palette.CellTypes.Count);
            foreach (CellType type in palette.CellTypes)
            {
                var viewData = new CellTypeViewData(type);
                _viewData.Add(type, viewData);
                _viewDataOrder.Add(viewData);
            }
        }

        public CellTypeViewData SelectedCellTypeViewData
        {
            get => _selectedCellTypeViewData;
            set
            {
                if (_selectedCellTypeViewData != value)
                {
                    _selectedCellTypeViewData = value;
                    SelectedCellTypeViewDataChanged?.Invoke(value);
                }
            }
        }
        public IReadOnlyDictionary<CellType, CellTypeViewData> ViewData => _viewData;
        public IReadOnlyList<CellTypeViewData> ViewDataOrder => _viewDataOrder;
        public Palette Palette => _palette;

        public event Action<CellTypeViewData> SelectedCellTypeViewDataChanged;

        private Dictionary<CellType, CellTypeViewData> _viewData = new();
        [SerializeReference] private CellTypeViewData _selectedCellTypeViewData;
        [SerializeReference] private Palette _palette;
        [SerializeReference] private CellType[] _skeys;
        [SerializeReference] private CellTypeViewData[] _svalues;
        [SerializeReference] private List<CellTypeViewData> _viewDataOrder;

        public void Add()
        {
            CellType type = _palette.CreateNewCellType();
            var viewData = new CellTypeViewData(type);
            _viewData.Add(type, viewData);
            _viewDataOrder.Add(viewData);
        }

        public void Remove(int typeIndex)
        {
            CellTypeViewData viewData = _viewDataOrder[typeIndex];
            if (SelectedCellTypeViewData == viewData)
            {
                UpdateSelectedItem(null);
            }
            _viewDataOrder.RemoveAt(typeIndex);
            _palette.Remove(viewData.Type);
        }

        public void MoveItemFromTo(int from, int to)
        {
            CellTypeViewData item = _viewDataOrder[from];
            _viewDataOrder.RemoveAt(from);
            _viewDataOrder.Insert(to, item);
        }

        public List<CellTypeViewData> GetBindableList()
        {
            return new List<CellTypeViewData>(_viewDataOrder);
        }

        public void UpdateSelectedItem(CellTypeViewData selectedItemViewData)
        {
            SelectedCellTypeViewData = selectedItemViewData;
        }

        public void OnBeforeSerialize()
        {
            _skeys = new CellType[_viewData.Count];
            _viewData.Keys.CopyTo(_skeys, 0);
            _svalues = new CellTypeViewData[_viewData.Count];
            _viewData.Values.CopyTo(_svalues, 0);
        }

        public void OnAfterDeserialize()
        {
            _viewData = new();
            for (int i = 0; i < _skeys.Length; i += 1)
            {
                _viewData.Add(_skeys[i], _svalues[i]);
            }
        }
    }
}
