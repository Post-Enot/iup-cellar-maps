using System;
using UnityEngine;

namespace CellarMaps
{
    [Serializable]
    public sealed class CellarMapSerializationShell : ISerializationCallbackReceiver
    {
        public void OnAfterDeserialize() { }

        public void OnBeforeSerialize() { }
    }
}
