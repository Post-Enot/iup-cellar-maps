using IUP.Toolkits.Matrices;
using System;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    public interface IMirrorCommand
    {
        public event Action<MatrixMirror> MirrorCommandInvoked;
    }
}
