using System;
using CharacterSystem;
using UnityEngine;

namespace DialogueSystem.TalkTo
{
    [Serializable]
    public struct TalkToContainer
    {
        [field: Header("Info")]
        public CharacterInfoScriptable Character { get; private set; }
        [field: SerializeField] public ChatContainer Chat { get; private set; }
        [field: SerializeField] public GiftContainer Gift { get; private set; }


        public TalkToContainer(in Character character)
        {
            Character = character.CharacterInfo;

            Chat = TalkToHandler.ChatContainers[character.TalkToIndexContainer.ChatIndex];
            Gift = TalkToHandler.GiftContainers[character.TalkToIndexContainer.GiftIndex];
        }
    }
}
