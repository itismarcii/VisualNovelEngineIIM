using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Project/Quest/Info", fileName = "QuestInfoScr")]
    public class QuestInfoScriptable : ScriptableObject
    {
        public int ID { get; private set; }
        
        [field: Header("Info")]
        [field: SerializeField] public string QuestName { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        
        [field: Header("Logic")]
        [field: SerializeField] public QuestInfoScriptable[] QuestRequirement { get; private set; }
        [field: SerializeField] public QuestStepHolder[] QuestSteps { get; private set; }
        
        [field: Header("Reward")]
        [field: SerializeField] public QuestReward QuestReward { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            ID = GetHasNumber(this.name);
            UnityEditor.EditorUtility.SetDirty(this);
        }

        private static int GetHasNumber(string input)
        {
            var hash = input.GetHashCode();
            return (hash < 0 ? -hash : hash);
        }
#endif
    }
}
