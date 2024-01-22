using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.TalkTo
{
    public static class TalkToHandler
    {
        public static readonly Dictionary<int, ChatContainer> ChatContainers = new Dictionary<int, ChatContainer>();
        public static readonly Dictionary<int, GiftContainer> GiftContainers = new Dictionary<int, GiftContainer>();

        public static void GenerateDialogues()
        {
            var chats = Resources.LoadAll<ChatContainerScriptable>("Dialogue/TalkTo/Chat");
            var gifts = Resources.LoadAll<GiftContainerScriptable>("Dialogue/TalkTo/Gift");
        }
        
    }
    
}
