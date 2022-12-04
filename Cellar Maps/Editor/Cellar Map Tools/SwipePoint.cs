using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    public sealed class SwipePoint : CellarMapTool
    {
        public SwipePoint(
            ICellarMapInteractor model,
            PalettePresenter palette,
            LayerListPresenter layerList,
            CellarMapPresenter presenter) : base(model, palette, layerList, presenter) { }

        private ILayer SelectedLayer => Model[LayerList.SelectedLayerIndex];
        private ICellType SelectedCellType => Palette.SelectedCellType;

        public override void Enable()
        {
            Presenter.Clicked += ChangeCell;
        }

        public override void Disable()
        {
            Presenter.Clicked -= ChangeCell;
        }

        private void ChangeCell(Vector2Int cellCoordinate)
        {
            if (SelectedCellType != null)
            {
                if (SelectedLayer[cellCoordinate].CellType
                    != SelectedCellType)
                {
                    Model.SetCellType(
                        LayerList.SelectedLayerIndex,
                        cellCoordinate,
                        SelectedCellType.TypeName);
                }
                else
                {
                    Model.SetCellType(
                        LayerList.SelectedLayerIndex,
                        cellCoordinate,
                        null);
                }
                Presenter.UpdateCellView(cellCoordinate);
            }
            else
            {
                if (SelectedLayer[cellCoordinate].CellType != null)
                {
                    Model.SetCellType(
                        LayerList.SelectedLayerIndex,
                        cellCoordinate,
                        null);
                    Presenter.UpdateCellView(cellCoordinate);
                }
            }
        }
    }
}
