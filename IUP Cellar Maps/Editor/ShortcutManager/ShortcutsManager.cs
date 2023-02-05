using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor
{
    public sealed class ShortcutManager
    {
        public ShortcutManager(VisualElement rootVisualElement)
        {
            _rootVisualElement = rootVisualElement;
            _rootVisualElement.RegisterCallback<KeyDownEvent>(HandleKeyDownEvent);
            _rootVisualElement.RegisterCallback<KeyUpEvent>(HandleKeyUpEvent);
        }

        private readonly VisualElement _rootVisualElement;
        private readonly List<KeyCode> _pressedKeys = new();
        private readonly HashSet<Shortcut> _shortcuts = new();

        public void RegisterShortcut(Shortcut shortcut)
        {
            _shortcuts.Add(shortcut);
        }

        public void UnregisterShortcut(Shortcut shortcut)
        {
            _shortcuts.Remove(shortcut);
        }

        private void HandleKeyDownEvent(KeyDownEvent context)
        {
            _pressedKeys.Add(context.keyCode);
            foreach (Shortcut shortcut in _shortcuts)
            {
                TryInvokeShortcut(shortcut);
            }
        }

        private void HandleKeyUpEvent(KeyUpEvent context)
        {
            _ = _pressedKeys.Remove(context.keyCode);
        }

        private void TryInvokeShortcut(Shortcut shortcut)
        {
            if (shortcut.Count != _pressedKeys.Count)
            {
                return;
            }
            for (int i = 0; i < shortcut.Count; i += 1)
            {
                if (shortcut[i].CompareTo(_pressedKeys[i]) != 0)
                {
                    return;
                }
            }
            shortcut.InvokeShortcutCallback();
        }
    }
}
