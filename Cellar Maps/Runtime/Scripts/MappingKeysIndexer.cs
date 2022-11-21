using IUP.Toolkits.Matrices;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Индексатор для доступа к названиям типов для дальнейшего сопоставления в классах-генераторах.
    /// </summary>
    public sealed class MappingKeysIndexer : IReadonlyMatrixIndexer<string>
    {
        public MappingKeysIndexer(Matrix<CellType> layer)
        {
            _layer = layer;
        }

        public int Width => _layer.Width;
        public int Height => _layer.Height;

        private readonly Matrix<CellType> _layer;

        public string this[Vector2Int coordinate] => this[coordinate.x, coordinate.y];
        public string this[int x, int y] => _layer[x, y]?.TypeName;
    }
}
