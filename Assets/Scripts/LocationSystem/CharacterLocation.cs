using UnityEngine;
using UnityEngine.UI;

namespace LocationSystem
{
    [RequireComponent(typeof(Image))]
    public class CharacterLocation : Location
    {
        [field: SerializeField] public CharacterLocationInfoScriptable CharacterLocationInfo { get; private set; }
        
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            ID = GetHasNumber(this.name + transform.parent);
            if(CharacterLocationInfo != null) CharacterLocationInfo.ApplyLocationID(ID);
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

        public void Activate(in Sprite characterSprite)
        {
            Image.sprite = characterSprite;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
