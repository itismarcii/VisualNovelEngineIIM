using CharacterSystem;
using UnityEditor;
using UnityEngine;

namespace SceneEventSystem
{
    [CreateAssetMenu(menuName = "Project/SceneEvent", fileName = "SceneEvent")]
    public class SceneEventInfoScriptable : ScriptableObject
    {
        public int ID { get; private set; }

        [field: SerializeField] public string SceneName { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public bool InGallery { get; private set; }
        [field: SerializeField] public CharacterInfoScriptable Character { get; private set; }
        [field: SerializeField] public SceneAsset Scene { get; private set; }
        
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
