using UnityEngine;

namespace DialogueSystem.TalkTo
{
    [CreateAssetMenu(menuName = "Project/Dialogue/TalkTo/Container", fileName = "DefaultDialogueContainer")]
    public class TalkToContainerScriptable : ScriptableObject
    {
        public int ID { get; private set; }
        
        [field: Header("Info")]
        [field: SerializeField] public ChatContainerScriptable Chat { get; private set; }
        [field: SerializeField] public GiftContainerScriptable Gift { get; private set; }
        
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
