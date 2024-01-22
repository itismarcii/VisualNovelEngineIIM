using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LocationSystem
{
    public abstract class Location : MonoBehaviour
    {
        public int ID { get; protected set; }

        public static readonly Dictionary<int, Location> Locations = new Dictionary<int, Location>();
        [field: SerializeField] public Image Image { get; private set; }

        private void Awake()
        {
            Locations.Add(ID, this);
        }

#if UNITY_EDITOR
        protected static int GetHasNumber(string input)
        {
            var hash = input.GetHashCode();
            return (hash < 0 ? -hash : hash);
        }
#endif
    }
}
