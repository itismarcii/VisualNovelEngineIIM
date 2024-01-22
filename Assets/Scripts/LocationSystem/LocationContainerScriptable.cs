using System;
using UnityEngine;

namespace LocationSystem
{
    [CreateAssetMenu(menuName = "Project/Location/Container", fileName = "DefaultLocationContainer")]
    public class LocationContainerScriptable : ScriptableObject
    {
        public int ID { get; private set; }

        [field: SerializeField] public LocationWeekObjectContainer LocationWeekObject { get; private set; }
        
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
