using CellarMaps.UI;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CellarMaps.EditorScripts
{
    [CustomEditor(typeof(CellarMapAsset))]
    public sealed class CellarMapAssetEditor : Editor
    {
        private CellarMapAsset _cellarMapAsset;
        private VisualElement _rootElement;
        private VisualTreeAsset _visualTree;

        private IntegerField _uiWidthField;
        private IntegerField _uiHeightField;
        private Button _uiGenerateFieldButton;
        private UI.CellarMap _uiCellarMap;
        private UI.Palette _uiPalette;
        private SelectedCellTypeIndicator _uiSelectedCellTypeIndicator;

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = _rootElement;
            root.Clear();
            _visualTree.CloneTree(root);
            _uiWidthField = root.Q<IntegerField>("width-int-field");
            _uiHeightField = root.Q<IntegerField>("height-int-field");
            _uiCellarMap = root.Q<UI.CellarMap>("cellar-map");
            _uiPalette = root.Q<UI.Palette>("palette");
            _uiGenerateFieldButton = root.Q<Button>("generate-cellar-map-button");
            _uiGenerateFieldButton.clicked += HandleGenerateFieldButtonClick;
            _uiSelectedCellTypeIndicator = root.Q<SelectedCellTypeIndicator>("selected-cell-type-indicator");
            SynchAssetDataWithEditor();
            InitPaletteViewWithPresenter();
            return root;
        }

        private void OnEnable()
        {
            _cellarMapAsset = target as CellarMapAsset;
            _rootElement = new VisualElement();
            _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/com.iup.cellar-maps/Runtime/CellarMapsEditorWindow.uxml");
            StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Packages/com.iup.cellar-maps/Runtime/CellarMapAssetEditorStyleSheet.uss");
            _rootElement.styleSheets.Add(uss);
        }

        private void OnDisable()
        {
            _cellarMapAsset.Map.CellsChanged -= HandleMapCellsChanged;
            _cellarMapAsset.Map.Recreated -= HandleMapRecreation;
        }

        private void InitPaletteViewWithPresenter()
        {
            void AddItems(IEnumerable<int> indexes)
            {
                foreach (int index in indexes)
                {
                    EditorUtility.SetDirty(_cellarMapAsset);
                    _cellarMapAsset.ViewPalette.CreateNewCellType();
                }
                _uiPalette.BindWith(_cellarMapAsset.ViewPalette);
            }
            void RemoveItems(IEnumerable<int> indexes)
            {
                EditorUtility.SetDirty(_cellarMapAsset);
                foreach (int index in indexes)
                {
                    _cellarMapAsset.ViewPalette.Remove(index);
                }
                _uiPalette.BindWith(_cellarMapAsset.ViewPalette);
            }
            void SwapItemsInOrder(int aIndex, int bIndex)
            {
                EditorUtility.SetDirty(_cellarMapAsset);
                _cellarMapAsset.ViewPalette.SwapItemsInOrder(aIndex, bIndex);
                _uiPalette.BindWith(_cellarMapAsset.ViewPalette);
            }
            _uiPalette.itemsAdded += AddItems;
            _uiPalette.itemsRemoved += RemoveItems;
            _uiPalette.itemIndexChanged += SwapItemsInOrder;
            _uiPalette.onSelectionChange += HandlePaletteEventOnSelectionChange;
            _cellarMapAsset.ViewPalette.SelectedCellTypeViewDataChanged +=
                HandleViewPaletteSelectedCellTypeViewDataChanged;
            _uiSelectedCellTypeIndicator.ResetButtonClicked += HandleSelectedCellTypeIndicatorResetButtonClick;
        }

        private void HandleSelectedCellTypeIndicatorResetButtonClick()
        {
            _cellarMapAsset.ViewPalette.SelectedCellTypeViewData = null;
        }

        private void HandleViewPaletteSelectedCellTypeViewDataChanged(CellTypeViewData viewData)
        {
            _uiSelectedCellTypeIndicator.IndicatedCellTypeViewData = viewData;
        }

        /// <summary>
        /// Данное событие срабатывает в том числе, когда из списка удаляется выбранный элемент: 
        /// в этом случае selecredObjects будет содержать всего один элемент со значением null.
        /// </summary>
        private void HandlePaletteEventOnSelectionChange(IEnumerable<object> selectedObjects)
        {
            IEnumerator<object> enumerator = selectedObjects.GetEnumerator();
            enumerator.MoveNext();
            _cellarMapAsset.ViewPalette.SelectedCellTypeViewData = enumerator.Current as CellTypeViewData;
        }

        private void SynchAssetDataWithEditor()
        {
            _uiPalette.BindWith(_cellarMapAsset.ViewPalette);
            SynchField();
        }

        private void SynchField()
        {
            _uiCellarMap.CreateMap(_cellarMapAsset.Map.Width, _cellarMapAsset.Map.Height);
            for (int y = 0; y < _cellarMapAsset.Map.Height; y += 1)
            {
                for (int x = 0; x < _cellarMapAsset.Map.Width; x += 1)
                {
                    if (_cellarMapAsset.Map[x, y] != null)
                    {
                        _uiCellarMap[y, x].ViewData = _cellarMapAsset.ViewPalette.ViewData[_cellarMapAsset.Map[x, y]];
                    }
                    else
                    {
                        _uiCellarMap[y, x].ViewData = null;
                    }
                }
            }
            _uiCellarMap.InteractWithCell += HandleInteractWithCell;
            _cellarMapAsset.Map.CellsChanged += HandleMapCellsChanged;
            _cellarMapAsset.Map.Recreated += HandleMapRecreation;
            _uiWidthField.value = _cellarMapAsset.Map.Width;
            _uiHeightField.value = _cellarMapAsset.Map.Height;
        }

        private void HandleMapRecreation()
        {
            _uiCellarMap.CreateMap(_cellarMapAsset.Map.Width, _cellarMapAsset.Map.Height);
        }

        private void HandleMapCellsChanged(Vector2Int[] changedCellsCoordinate)
        {
            EditorUtility.SetDirty(_cellarMapAsset);
            Undo.RecordObject(_cellarMapAsset, "map cells changed");
            foreach (Vector2Int coordinate in changedCellsCoordinate)
            {
                if (_cellarMapAsset.Map[coordinate] != null)
                {
                    _uiCellarMap[coordinate].ViewData = _cellarMapAsset.ViewPalette.ViewData[_cellarMapAsset.Map[coordinate]];
                }
                else
                {
                    _uiCellarMap[coordinate].ViewData = null;
                }
            }
        }

        private void HandleInteractWithCell(Vector2Int coordinate)
        {
            CellTypeViewData viewData = _uiCellarMap[coordinate].ViewData;
            if (viewData == null || viewData != _uiSelectedCellTypeIndicator.IndicatedCellTypeViewData)
            {
                if (_uiSelectedCellTypeIndicator.IndicatedCellTypeViewData == null)
                {
                    _cellarMapAsset.Map[coordinate] = null;
                }
                else
                {
                    _cellarMapAsset.Map[coordinate] = _uiSelectedCellTypeIndicator.IndicatedCellTypeViewData.Type;
                }
            }
            else
            {
                _cellarMapAsset.Map[coordinate] = null;
            }
        }

        private void HandleGenerateFieldButtonClick()
        {
            EditorUtility.SetDirty(_cellarMapAsset);
            _cellarMapAsset.Map.Recreate(_uiWidthField.value, _uiHeightField.value);
        }
    }
}
