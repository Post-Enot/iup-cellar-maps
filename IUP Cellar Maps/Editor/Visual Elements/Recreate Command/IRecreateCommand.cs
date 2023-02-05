using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Интерфейс представления команды пересоздания клеточной карты.
    /// </summary>
    public interface IRecreateCommand
    {
        /// <summary>
        /// Вызывается при вызове пользователем команды "пересоздать клеточную карту".
        /// </summary>
        public event Action<int, int> RecreateCommandInvoked;
    }
}
