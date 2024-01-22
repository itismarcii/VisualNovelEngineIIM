using System;
using TimeSystem;
using UnityEngine;

namespace LocationSystem
{
    [CreateAssetMenu(menuName = "Project/Location/Room", fileName = "RoomLocation")]
    public class RoomLocationInfoScriptable : LocationInfoScriptable
    {
        [Serializable]
        public struct DayCycle
        {
            public Sprite Image;
            public TimeCycle TimeCycle;
        }
        
        
        [field: Header("Data")]
        [field: SerializeField] public DayCycle[] DayCycles { get; private set; }
    }
}
