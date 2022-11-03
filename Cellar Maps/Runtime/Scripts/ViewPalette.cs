using System;
using System.Collections.Generic;
using UnityEngine;

namespace CellarMaps
{
    [Serializable]
    public sealed class ViewPalette : ISerializationCallbackReceiver
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
                    SelectedCellTypeViewDataChanged?.Invoke(_selectedCellTypeViewData);
                }
                else
                {
                    _selectedCellTypeViewData = value;
                }
            }
        }
        public IReadOnlyDictionary<CellType, CellTypeViewData> ViewData => _viewData;
        public IReadOnlyList<CellTypeViewData> ViewDataOrder => _viewDataOrder;
        public Palette Palette => _palette;

        public event Action<CellTypeViewData> SelectedCellTypeViewDataChanged;

        private Dictionary<CellType, CellTypeViewData> _viewData = new();
        private CellTypeViewData _selectedCellTypeViewData;
        [SerializeReference] private Palette _palette;
        [SerializeReference] private CellType[] _skeys;
        [SerializeReference] private CellTypeViewData[] _svalues;
        [SerializeReference] private List<CellTypeViewData> _viewDataOrder;

        public void CreateNewCellType()
        {
            CellType type = _palette.CreateNewCellType();
            var viewData = new CellTypeViewData(type);
            _viewData.Add(type, viewData);
            _viewDataOrder.Add(viewData);
        }

        public void Remove(int index)
        {
            CellTypeViewData viewData = _viewDataOrder[index];
            _viewDataOrder.RemoveAt(index);
            _palette.Remove(viewData.Type);
        }

        public void SwapItemsInOrder(int aIndex, int bIndex)
        {
            (_viewDataOrder[bIndex], _viewDataOrder[aIndex]) = (_viewDataOrder[aIndex], _viewDataOrder[bIndex]);
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
