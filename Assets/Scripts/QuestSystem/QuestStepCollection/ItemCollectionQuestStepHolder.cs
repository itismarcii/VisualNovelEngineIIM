using UnityEngine;

namespace QuestSystem.QuestStepCollection
{
    [CreateAssetMenu(menuName = "Project/Quest/Step/Collective", fileName = "Collective")]
    public class ItemCollectionQuestStepHolder : QuestStepHolder
    {
        [SerializeField] private ItemCollectionQuestStep QuestStep;

        public override QuestStep GetQuestStep() => QuestStep.CreateNew();
    }
}
