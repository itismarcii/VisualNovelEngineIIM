using System;
using UnityEngine;

namespace DialogueSystem.TalkTo
{
    [Serializable]
    public struct ChatContainer
    {
        [field: SerializeField] public string Title { get; private set; }
    
        // Change depending on Dialogue logic
        [field: SerializeField] public string Dialogue { get; private set; }
    }
}
