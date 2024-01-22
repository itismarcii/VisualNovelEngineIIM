using System;
using CharacterSystem;
using ItemSystem;
using UnityEngine;

namespace DialogueSystem.TalkTo
{
    [Serializable]
    public struct GiftContainer
    {
        [field: SerializeField] public string Title { get; private set; }
        
        // Change depending on Dialogue logic
        [field: SerializeField] public string PositiveDialogue { get; private set; }
        [field: SerializeField] public string NeutralDialogue { get; private set; }
        [field: SerializeField] public string NegativeDialogue { get; private set; }

        public string GetDialogueFromGift(in Character character, in GiftItemInfoScriptable gift)
        {
            foreach (var dislikeGift in character.CharacterInfo.DislikeGifts)
            {
                if (dislikeGift == gift) return NegativeDialogue;
            }
            
            
            foreach (var dislikeGift in character.CharacterInfo.LikeGifts)
            {
                if (dislikeGift == gift) return PositiveDialogue;
            }
            
            return NeutralDialogue;
        }
    }
}
