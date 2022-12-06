using System;
using System.IO;
using UnityEditor;
using UnityEditor.AssetImporters;

namespace IUP.Toolkits.CellarMaps.Editor
{
    [ScriptedImporter(_version, _fileExtension)]
    public class CellarMapImporter : ScriptedImporter
    {
        internal const int _version = 1;
        internal const string _fileExtension = "cellarmap";
        private const string _defaultAssetLayout = "{\"data_format_version\":0,\"cellar_map_view_data\":{\"cell_types_view_data\":[{\"cell_type_name\":\"land\",\"color\":{\"r\":0.09411765,\"g\":0.09411765,\"b\":0.09411765,\"a\":1}},{\"cell_type_name\":\"main-hero\",\"color\":{\"r\":0.3137255,\"g\":0,\"b\":0,\"a\":1}}],\"layers_view_data\":[{\"layer_name\":\"surface\",\"color\":{\"r\":0.09411765,\"g\":0.09411765,\"b\":0.09411765,\"a\":1}},{\"layer_name\":\"entities\",\"color\":{\"r\":0.3137255,\"g\":0,\"b\":0,\"a\":1}}]},\"cellar_map\":{\"width\":10,\"height\":10,\"cell_types\":[{\"type_name\":\"land\"},{\"type_name\":\"main-hero\"}],\"layers\":[{\"layer_name\":\"surface\",\"cells\":[]},{\"layer_name\":\"entities\",\"cells\":[]}]}}";

        public override void OnImportAsset(AssetImportContext ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException(nameof(ctx));
            }
            string cellarMapJson;
            try
            {
                cellarMapJson = File.ReadAllText(ctx.assetPath);
            }
            catch (Exception exception)
            {
                ctx.LogImportError($"Could not read file '{ctx.assetPath}' ({exception})");
                return;
            }

            var asset = CellarMapAsset.CreateAsset(ctx.assetPath);
            ctx.AddObjectToAsset("<root>", asset);
            ctx.SetMainObject(asset);

            // Refresh editors.
            //InputActionEditorWindow.RefreshAllOnAssetReimport();
        }

        [MenuItem("Assets/Create/Cellar Maps/Cellar Map")]
        public static void CreateAsset()
        {
            ProjectWindowUtil.CreateAssetWithContent($"Cellar Map.{_fileExtension}",
                _defaultAssetLayout);
        }
    }
}
