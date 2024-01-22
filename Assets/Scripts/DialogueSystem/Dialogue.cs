using System;
using CharacterSystem;
using UnityEngine;

namespace DialogueSystem
{
    [Serializable]
    public struct Dialogue
    {
        [field: SerializeField] public CharacterInfoScriptable Character { get; private set; }
        [field: SerializeField] public int AnimationIndex { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
    }
}
