using UnityEngine;

namespace QuestSystem.QuestStepCollection
{
    [CreateAssetMenu(menuName = "Project/Quest/Step/TalkTo", fileName = "TalkTo")]
    public class TalkToQuestStepHolder : QuestStepHolder
    {
        [SerializeField] private TalkToQuestStep QuestStep;
        public override QuestStep GetQuestStep() => QuestStep.CreateNew();
    }
}
