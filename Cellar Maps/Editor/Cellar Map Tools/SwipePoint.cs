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

        private IReadOnlyLayer SelectedLayer => Model[LayerList.SelectedLayerIndex];
        private IReadOnlyCellType SelectedCellType => Palette.SelectedCellType;

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
                if (SelectedLayer[cellCoordinate].Type
                    != SelectedCellType)
                {
                    Model.SetCellType(
                        LayerList.SelectedLayerIndex,
                        cellCoordinate,
                        SelectedCellType.Name);
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
                if (SelectedLayer[cellCoordinate].Type != null)
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
