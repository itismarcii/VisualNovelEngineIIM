using System;
using ItemSystem;

namespace QuestSystem.QuestStepCollection
{
    public interface ICollect
    {
        public ItemInfoScriptable GetItem();
        public void Collect();
    }
}
