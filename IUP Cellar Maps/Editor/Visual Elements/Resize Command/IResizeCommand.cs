using IUP.Toolkits.Matrices;
using System;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    public interface IResizeCommand
    {
        public event Action<int, int, WidthResizeRule, HeightResizeRule> ResizeCommandInvoked;
    }
}
