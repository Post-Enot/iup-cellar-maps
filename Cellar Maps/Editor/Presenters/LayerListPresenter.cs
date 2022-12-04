using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    public sealed class LayerListPresenter
    {
        public LayerListPresenter(
            ICellarMapInteractor model,
            UI.ILayerList view,
            UI.IActiveLayerIndicator activeLayerIndicator,
            CellarMapPresenter cellarMapPresenter,
            string defaultLayerName,
            Color defaultLayerColor,
            int activeLayer)
        {
            _model = model;
            _view = view;
            _activeLayerIndicator = activeLayerIndicator;
            _cellarMapPresenter = cellarMapPresenter;
            _defaultLayerName = defaultLayerName;
            _defaultLayerColor = defaultLayerColor;
            BindViewWithModel();
            _view.ViewDataChanged += ChangeViewData;
            _view.Added += AddLayer;
            _view.Removed += RemoveLayer;
            _view.Selected += SelectLayer;
            SelectLayer(activeLayer);
        }

        public int SelectedLayerIndex { get; private set; }
        public ILayer SelectedLayer => _model[SelectedLayerIndex];

        private readonly ICellarMapInteractor _model;
        private readonly UI.ILayerList _view;
        private readonly UI.IActiveLayerIndicator _activeLayerIndicator;
        private readonly CellarMapPresenter _cellarMapPresenter;
        private readonly string _defaultLayerName;
        private readonly Color _defaultLayerColor;

        private void ChangeViewData(
            int layerIndex,
            string newLayerName,
            Color layerColor)
        {
            _model.RenameLayer(layerIndex, newLayerName);
            _model.SetLayerColor(layerIndex, layerColor);
            ILayerViewData viewData = _model.LayersViewData[layerIndex];
            _activeLayerIndicator.SetViewData(viewData);
            BindViewWithModel();
        }

        private void AddLayer(int layerIndex)
        {
            _model.AddLayer(_defaultLayerName, _defaultLayerColor);
            BindViewWithModel();
        }

        private void RemoveLayer(int layerIndex)
        {
            if (_model.LayersCount > 1)
            {
                _model.RemoveLayer(layerIndex);
                if (SelectedLayerIndex == _model.LayersCount)
                {
                    layerIndex = _model.LayersCount - 1;
                    SelectLayer(layerIndex);
                }
            }
            BindViewWithModel();
        }

        private void SelectLayer(int layerIndex)
        {
            SelectedLayerIndex = layerIndex;
            _cellarMapPresenter.SetActiveLayerIndex(layerIndex);
            ILayerViewData viewData = _model.LayersViewData[layerIndex];
            _activeLayerIndicator.SetViewData(viewData);
            _cellarMapPresenter.UpdateCellarMapView();
        }

        private void BindViewWithModel()
        {
            _view.BindWith(_model.LayersViewData);
        }
    }
}
