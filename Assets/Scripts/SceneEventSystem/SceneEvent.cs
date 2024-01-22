using System;
using UnityEngine;

namespace SceneEventSystem
{
    [Serializable]
    public class SceneEvent
    {
        [field: SerializeField] public SceneEventInfoScriptable SceneEventInfo { get; private set; }
        public bool IsUnlocked { get; private set; } = false;

        public void Unlock() => IsUnlocked = true;

        public SceneEvent(in SceneEventInfoScriptable sceneEventInfo)
        {
            SceneEventInfo = sceneEventInfo;
        }
    }
}
