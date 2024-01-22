using UnityEngine;

namespace LocationSystem
{
    [CreateAssetMenu(menuName = "Project/Location/Character", fileName = "CharacterLocation")]
    public class CharacterLocationInfoScriptable : LocationInfoScriptable
    {
        [field: SerializeField] public RoomLocationInfoScriptable RoomLocation { get; private set; }
    }
}
