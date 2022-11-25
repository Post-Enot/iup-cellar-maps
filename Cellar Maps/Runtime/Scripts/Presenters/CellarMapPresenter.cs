using UnityEngine;

namespace IUP.Toolkits.CellarMaps.UI
{
    public sealed class CellarMapPresenter : IPresenter
    {
        public CellarMapPresenter(
            CellarMaps.CellarMap model,
            CellarMap view,
            ViewPalette viewPalette,
            ViewLayers viewLayers)
        {
            Model = model;
            View = view;
            ViewPalette = viewPalette;
            ViewLayers = viewLayers;
            SynchMapViewWithModel();
        }

        public CellarMaps.CellarMap Model { get; }
        public CellarMap View { get; }
        public ViewPalette ViewPalette { get; }
        public ViewLayers ViewLayers { get; }

        public void OnEnable()
        {
            View.InteractWithCell += HandleInteractWithCell;
            Model.CellsChanged += UpdateMapView;
            Model.Recreated += ReinitMapView;
            Model.Resized += SynchMapViewWithModel;
            ViewLayers.ActiveLayerChanged += ViewLayers_ActiveLayerChanged;
        }

        public void OnDisable()
        {
            View.InteractWithCell -= HandleInteractWithCell;
            Model.CellsChanged -= UpdateMapView;
            Model.Recreated -= ReinitMapView;
            Model.Resized -= SynchMapViewWithModel;
            ViewLayers.ActiveLayerChanged -= ViewLayers_ActiveLayerChanged;
        }

        private void ViewLayers_ActiveLayerChanged(LayerViewData _)
        {
            UpdateMapView();
        }

        private void SynchMapViewWithModel()
        {
            View.CreateMap(Model.Width, Model.Height);
            for (int y = 0; y < Model.Height; y += 1)
            {
                for (int x = 0; x < Model.Width; x += 1)
                {
                    (CellType topCellType, bool isOnActiveLayer) =
                        Model.TopCells[ViewLayers.ActiveLayerViewData.LayerIndex, x, y];
                    if (topCellType != null)
                    {
                        View[x, y].SetViewData(ViewPalette.ViewData[topCellType], isOnActiveLayer);
                    }
                    else
                    {
                        View[x, y].SetViewData(null, false);
                    }
                }
            }
        }

        private void ReinitMapView()
        {
            View.CreateMap(Model.Width, Model.Height);
        }

        private void UpdateMapView()
        {
            for (int y = 0; y < Model.Height; y += 1)
            {
                for (int x = 0; x < Model.Width; x += 1)
                {
                    UpdateMapViewByCoordinate(x, y);
                }
            }
        }

        private void HandleInteractWithCell(Vector2Int coordinate)
        {
            if (ViewPalette.SelectedCellTypeViewData == null)
            {
                if (ViewLayers.ActiveLayer[coordinate] != null)
                {
                    ViewLayers.ActiveLayer[coordinate] = null;
                }
            }
            else
            {
                if (ViewLayers.ActiveLayer[coordinate] == null)
                {
                    ViewLayers.ActiveLayer[coordinate] = ViewPalette.SelectedCellTypeViewData.Type;
                }
                else
                {
                    ViewLayers.ActiveLayer[coordinate] = null;
                }
            }
        }

        private void UpdateMapViewByCoordinate(int x, int y)
        {
            (CellType topCellType, bool isOnActiveLayer) 
            = Model.TopCells[ViewLayers.ActiveLayerViewData.LayerIndex, x, y];
            if (topCellType != null)
            {
                View[x, y].SetViewData(ViewPalette.ViewData[topCellType], isOnActiveLayer);
            }
            else
            {
                View[x, y].SetViewData(null, false);
            }
        }
    }
}
