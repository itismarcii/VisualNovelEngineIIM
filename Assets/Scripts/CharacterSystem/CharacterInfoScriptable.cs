using DialogueSystem.TalkTo;
using ItemSystem;
using LocationSystem;
using QuestSystem;
using UnityEngine;

namespace CharacterSystem
{
    [CreateAssetMenu(menuName = "Project/Character", fileName = "new Character")]
    public class CharacterInfoScriptable : ScriptableObject
    {
        public int ID { get; private set; }
        
        [field: Header("Info")]
        [field: SerializeField] public string CharacterName { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public bool Female { get; private set; } = true;
        [field: SerializeField] public CharacterHobby Hobbies { get; private set; }
        [field: SerializeField] public CharacterHobby DislikeHobbies { get; private set; }
        [field: SerializeField] public GiftItemInfoScriptable[] LikeGifts { get; private set; }
        [field: SerializeField] public GiftItemInfoScriptable[] DislikeGifts { get; private set; }
        
        [field: Header("UI")] 
        [field: SerializeField] public Sprite IconSprite { get; private set; }
        [field: SerializeField] public CharacterEmotionUI EmotionUI { get; private set; }

        [field: Header("Logic")]
        [field: SerializeField, Tooltip("X: Positive correction, Y: Negative correction")] public Vector2Int ModeBalanceValue { get; private set; }
        [field: SerializeField] public QuestInfoScriptable[] QuestQueue { get; private set; }
        
        [field: Header("Dialogue")]
        [field: SerializeField] public TalkToContainerScriptable DefaultTalkTo { get; private set; }

        [field: Header("Location")]
        [field: SerializeField] public LocationContainerScriptable DefaultLocation { get; private set; }

        
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
