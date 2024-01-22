using System;
using UnityEngine;

namespace LocationSystem
{
    [Serializable]
    public struct LocationWeekObjectContainer
    {
        [field: SerializeField] public LocationContainer Monday { get; private set; }
        [field: SerializeField] public LocationContainer Tuesday { get; private set; }
        [field: SerializeField] public LocationContainer Wednesday { get; private set; }
        [field: SerializeField] public LocationContainer Thursday { get; private set; }
        [field: SerializeField] public LocationContainer Friday { get; private set; }
        [field: SerializeField] public LocationContainer Saturday { get; private set; }
        [field: SerializeField] public LocationContainer Sunday { get; private set; }
    }
}
