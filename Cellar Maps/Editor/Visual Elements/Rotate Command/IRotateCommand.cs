using IUP.Toolkits.Matrices;
using System;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    public interface IRotateCommand
    {
        public event Action<MatrixRotation> RotateCommandInvoked;
    }
}
