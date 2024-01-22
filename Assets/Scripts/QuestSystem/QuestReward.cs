using System;
using ItemSystem;
using LocationSystem;
using SceneEventSystem;

namespace QuestSystem
{
    [Serializable]
    public struct QuestReward
    {
        public int Money;
        public int Experience;
        public ItemInfoScriptable[] Items;
        public LocationOverrideContainer[] LocationOverrides;
        public SceneEventInfoScriptable[] SceneEvents;
    }
}
