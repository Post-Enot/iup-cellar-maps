using System;
using System.Collections.Generic;
using UnityEngine;

namespace IUP_Toolkits.CellarMaps
{
    [Serializable]
    public sealed class ViewLayers : IListViewShellBindable<LayerViewData>
    {
        public ViewLayers(ICellarMapLayers layers)
        {
            _layers = layers;
            _viewData = new(layers.Count);
            for (int i = 0; i < layers.Count; i += 1)
            {
                var viewData = new LayerViewData(_layers[i], i);
                _viewData.Add(viewData);
            }
            _activeLayerViewData = _viewData[0];
        }

        public LayerViewData ActiveLayerViewData
        {
            get => _activeLayerViewData;
            set
            {
                if (_activeLayerViewData != value)
                {
                    _activeLayerViewData = value;
                    ActiveLayerChanged?.Invoke(value);
                }
            }
        }
        public int LayersCount => _layers.Count;
        public ICellarMapLayer ActiveLayer => _activeLayerViewData.Layer;
        public IReadOnlyList<LayerViewData> ViewData => _viewData;

        public event Action<LayerViewData> ActiveLayerChanged;

        [SerializeReference] private ICellarMapLayers _layers;
        [SerializeReference] private List<LayerViewData> _viewData;
        [SerializeReference] private LayerViewData _activeLayerViewData;

        public void Add()
        {
            _layers.AddLayer();
            int lastElementIndex = _layers.Count - 1;
            var viewData = new LayerViewData(_layers[lastElementIndex], lastElementIndex);
            _viewData.Add(viewData);
        }

        public void Remove(int layerIndex)
        {
            _layers.RemoveLayer(layerIndex);
            _viewData.RemoveAt(layerIndex);
            ResetIndexes();
        }

        public void MoveItemFromTo(int from, int to)
        {
            _layers.MoveLayerFromTo(from, to);
            LayerViewData item = _viewData[from];
            _viewData.RemoveAt(from);
            _viewData.Insert(to, item);
            ResetIndexes();
        }

        public List<LayerViewData> GetBindableList()
        {
            return new List<LayerViewData>(_viewData);
        }

        public void UpdateSelectedItem(LayerViewData selectedItemViewData)
        {
            ActiveLayerViewData = selectedItemViewData;
        }

        private void ResetIndexes()
        {
            for (int i = 0; i < _viewData.Count; i += 1)
            {
                _viewData[i].LayerIndex = i;
            }
        }
    }
}
