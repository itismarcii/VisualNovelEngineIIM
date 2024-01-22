using System;
using CharacterSystem;
using UnityEngine;

namespace LocationSystem
{
    [Serializable]
    public struct LocationOverrideContainer
    {
        [field: SerializeField] public CharacterInfoScriptable Character;
        [field: SerializeField] public LocationWeekObjectContainer Location;
    }
}
