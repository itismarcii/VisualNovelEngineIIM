using UnityEngine;

namespace QuestSystem
{
    public abstract class QuestStepHolder : ScriptableObject
    {
        public abstract QuestStep GetQuestStep();
    }
}
