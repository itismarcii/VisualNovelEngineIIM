using System;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(menuName = "Project/Item/Base", fileName = "BaseItem")]
    public class ItemInfoScriptable : ScriptableObject
    {
        public int ID { get; private set; }
        
        [field: Header("Info")]
        [field: SerializeField] public string ItemName { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }

        public Action<ItemInfoScriptable, uint> OnPickUp;
        
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
