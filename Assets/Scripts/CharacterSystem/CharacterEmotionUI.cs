using System;
using UnityEngine;

namespace CharacterSystem
{
    [Serializable]
    public struct CharacterEmotionUI
    {
        [field: SerializeField] public Sprite Default { get; private set; }
        [field: SerializeField] public Sprite Angry { get; private set; }
        [field: SerializeField] public Sprite Mad { get; private set; }
        [field: SerializeField] public Sprite Sad { get; private set; }
        [field: SerializeField] public Sprite Happy { get; private set; }
        [field: SerializeField] public Sprite Confused { get; private set; }
        [field: SerializeField] public Sprite Afraid { get; private set; }
        [field: SerializeField] public Sprite Horny { get; private set; }
    }
}
