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
        private CellarMapAsset _cellarMap;
        private VisualElement _rootElement;
        private VisualTreeAsset _visualTree;

        private IntegerField _widthField;
        private IntegerField _heightField;
        private Button _generateFieldButton;
        private Field _field;
        private UI.Palette _palette;

        private void OnEnable()
        {
            _cellarMap = target as CellarMapAsset;
            _rootElement = new VisualElement();
            _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/com.iup.cellar-maps/Runtime/CellarMapsEditorWindow.uxml");
            StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Packages/com.iup.cellar-maps/Runtime/CellarMapAssetEditorStyleSheet.uss");
            _rootElement.styleSheets.Add(uss);
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = _rootElement;
            root.Clear();
            _visualTree.CloneTree(root);
            _widthField = root.Q<IntegerField>("width-int-field");
            _heightField = root.Q<IntegerField>("height-int-field");
            _field = root.Q<Field>("field");
            _palette = root.Q<UI.Palette>("palette");
            _generateFieldButton = root.Q<Button>("generate-field-button");
            _generateFieldButton.clicked += GenerateFieldButton_clicked;
            SynchAssetDataWithEditor();
            InitPaletteViewWithPresenter();
            return root;
        }

        private void InitPaletteViewWithPresenter()
        {
            void AddItems(IEnumerable<int> indexes)
            {
                foreach (int index in indexes)
                {
                    _cellarMap.ViewPalette.CreateNewCellType();
                }
                _palette.BindWith(_cellarMap.ViewPalette);
            }
            void RemoveItems(IEnumerable<int> indexes)
            {
                foreach (int index in indexes)
                {
                    _cellarMap.ViewPalette.Remove(index);
                }
                _palette.BindWith(_cellarMap.ViewPalette);
            }
            void SwapItemsInOrder(int aIndex, int bIndex)
            {
                _cellarMap.ViewPalette.SwapItemsInOrder(aIndex, bIndex);
                _palette.BindWith(_cellarMap.ViewPalette);
            }
            _palette.itemsAdded += AddItems;
            _palette.itemsRemoved += RemoveItems;
            _palette.itemIndexChanged += SwapItemsInOrder;
            _palette.onItemsChosen += _palette_onItemsChosen;
        }

        private void _palette_onItemsChosen(IEnumerable<object> objects)
        {
            foreach (var obj in objects)
            {
                Debug.Log(obj);
            }
        }

        private void SynchAssetDataWithEditor()
        {
            _palette.BindWith(_cellarMap.ViewPalette);
            SynchField();
        }

        private void SynchField()
        {
            _field.CreateField(_cellarMap.Map.Width, _cellarMap.Map.Height);
            for (int y = 0; y < _cellarMap.Map.Height; y += 1)
            {
                for (int x = 0; x < _cellarMap.Map.Width; x += 1)
                {
                    if (_cellarMap.Map[x, y] != null)
                    {
                        _field[y, x].ViewData = _cellarMap.ViewPalette.ViewData[_cellarMap.Map[x, y]];
                    }
                }
            }
            _field.InteractWithCell += HandleInteractWithCell;
        }

        private void HandleInteractWithCell(Vector2Int coordinate)
        {
            CellTypeViewData viewData = _field[coordinate].ViewData;
            if (viewData == null)
            {
                _cellarMap.Map[coordinate] = _cellarMap.ViewPalette.ViewDataOrder[0].Type;
                _field[coordinate].ViewData = _cellarMap.ViewPalette.ViewData[_cellarMap.Map[coordinate]];
            }
            else
            {
                _cellarMap.Map[coordinate] = null;
                _field[coordinate].ViewData = null;
            }
        }

        private void GenerateFieldButton_clicked()
        {
            _field.CreateField(_widthField.value, _heightField.value);
        }
    }
}
