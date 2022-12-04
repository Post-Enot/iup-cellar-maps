using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Презентер палитры.
    /// </summary>
    public sealed class PalettePresenter
    {
        public PalettePresenter(
            ICellarMapInteractor model,
            UI.IPalette view,
            UI.IActiveCellTypeIndicator activeCellTypeIndicator,
            string unselectedCellTypeName,
            Color unselectedCellTypeColor,
            CellarMapPresenter cellarMapPresenter,
            string defaultCellTypeName,
            Color defaultCellTypeColor)
        {
            _model = model;
            _view = view;
            _activeCellTypeIndicator = activeCellTypeIndicator;
            _cellarMapPresenter = cellarMapPresenter;
            _defaultCellTypeName = defaultCellTypeName;
            _defaultCellTypeColor = defaultCellTypeColor;
            _view.BindWith(_model.CellTypesViewData);
            _view.ViewDataChanged += ChangeViewData;
            _view.Added += AddCellType;
            _view.Removed += RemoveCellType;
            _view.Selected += SelectCellType;
            _view.Unselected += UnselectCellType;
            _unselectedCellTypeViewData = new CellTypeViewData(
                unselectedCellTypeName,
                unselectedCellTypeColor);
            _activeCellTypeIndicator.SetViewData(_unselectedCellTypeViewData);
        }

        private void UnselectCellType()
        {
            SelectedCellType = null;
            _activeCellTypeIndicator.SetViewData(_unselectedCellTypeViewData);
        }

        public ICellType SelectedCellType { get; private set; }

        private readonly ICellarMapInteractor _model;
        private readonly UI.IPalette _view;
        private readonly UI.IActiveCellTypeIndicator _activeCellTypeIndicator;
        private readonly CellarMapPresenter _cellarMapPresenter;
        private readonly string _defaultCellTypeName;
        private readonly Color _defaultCellTypeColor;
        private readonly ICellTypeViewData _unselectedCellTypeViewData;

        private void ChangeViewData(
            string cellTypeName,
            string newCellTypeName,
            Color cellTypeColor)
        {
            if (cellTypeName != newCellTypeName)
            {
                _model.RenameCellType(cellTypeName, newCellTypeName);
            }
            _model.SetCellTypeColor(newCellTypeName, cellTypeColor);
            ICellTypeViewData viewData = _model.CellTypesViewData[newCellTypeName];
            _activeCellTypeIndicator.SetViewData(viewData);
            _view.BindWith(_model.CellTypesViewData);
            _cellarMapPresenter.UpdateCellarMapView();
        }

        private void AddCellType(int index)
        {
            string cellTypeName = $"{_defaultCellTypeName}-0";
            int i = 0;
            while (_model.CellTypesViewData.Contains(cellTypeName))
            {
                i += 1;
                cellTypeName = $"{_defaultCellTypeName}-{i}";
            }
            _model.AddCellTypeToPalette(cellTypeName, _defaultCellTypeColor);
            _view.BindWith(_model.CellTypesViewData);
        }

        private void RemoveCellType(int index)
        {
            _model.RemoveCellTypeFromPalette(_model.CellTypesViewData[index].CellTypeName);
            _cellarMapPresenter.UpdateCellarMapView();
            _activeCellTypeIndicator.SetViewData(_unselectedCellTypeViewData);
            _view.BindWith(_model.CellTypesViewData);
        }

        private void SelectCellType(ICellTypeViewData viewData)
        {
            SelectedCellType = _model.Palette[viewData.CellTypeName];
            _activeCellTypeIndicator.SetViewData(viewData);
        }
    }
}
