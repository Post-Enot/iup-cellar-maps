using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    public class Shortcut : IReadOnlyList<KeyCode>
    {
        public Shortcut(Action shortcutCallback, params KeyCode[] shortcutKeys)
        {
            _shortcutCallback = shortcutCallback;
            _shortcutKeys = new List<KeyCode>(shortcutKeys);
        }

        public int Count => _shortcutKeys.Count;

        private readonly List<KeyCode> _shortcutKeys;
        private readonly Action _shortcutCallback;

        public KeyCode this[int keyCodeIndex] => _shortcutKeys[keyCodeIndex];

        public void InvokeShortcutCallback()
        {
            _shortcutCallback();
        }

        public IEnumerator<KeyCode> GetEnumerator()
        {
            return _shortcutKeys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _shortcutKeys.GetEnumerator();
        }
    }
}
