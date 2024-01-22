using UnityEngine;

namespace LocationSystem
{
    public abstract class LocationInfoScriptable : ScriptableObject
    {
        public int ID { get; private set; }
        
        [field: SerializeField] public string LocationName { get; private set; }
        [field: SerializeField] public int LocationID { get; private set; }
        
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            LocationName = this.name;
            ID = GetHasNumber(this.name);
            UnityEditor.EditorUtility.SetDirty(this);
        }

        private static int GetHasNumber(string input)
        {
            var hash = input.GetHashCode();
            return (hash < 0 ? -hash : hash);
        }

        public void ApplyLocationID(in int id) => LocationID = id;
#endif

        public Location GetLocation() => Location.Locations[ID];
    }
}
    