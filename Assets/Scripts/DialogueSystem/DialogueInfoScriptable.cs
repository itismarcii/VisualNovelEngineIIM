using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "Project/Dialogue/Dialogue", fileName = "Dialogue")]
    public class DialogueInfoScriptable : ScriptableObject
    {
        public int ID { get; private set; }
        
        [field: SerializeField] public TextAsset Dialogue { get; private set; }
        
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
