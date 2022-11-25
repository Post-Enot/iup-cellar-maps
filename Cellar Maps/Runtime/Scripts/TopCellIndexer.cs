using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    public class TopCellIndexer
    {
        public TopCellIndexer(CellarMapLayers layers)
        {
            _layers = layers;
        }

        private readonly CellarMapLayers _layers;

        public (CellType, bool) this[int topLayerIndex, Vector2Int coordinate]
            => this[topLayerIndex, coordinate.x, coordinate.y];
        public (CellType, bool) this[int topLayerIndex, int x, int y]
        {
            get
            {
                for (int layerIndex = topLayerIndex; layerIndex >= 0; layerIndex -= 1)
                {
                    if (_layers[layerIndex][x, y] != null)
                    {
                        return (_layers[layerIndex][x, y], layerIndex == topLayerIndex);
                    }
                }
                return (null, false);
            }
        }
    }
}
