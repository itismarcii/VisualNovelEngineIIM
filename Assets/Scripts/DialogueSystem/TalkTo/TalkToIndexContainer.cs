using System;

namespace DialogueSystem.TalkTo
{
    [Serializable]
    public struct TalkToIndexContainer
    {
        public int ChatIndex;
        public int GiftIndex;

        public TalkToIndexContainer(in TalkToContainerScriptable container)
        {
            ChatIndex = container.Chat.ID;
            GiftIndex = container.Gift.ID;
        }
    }
}
