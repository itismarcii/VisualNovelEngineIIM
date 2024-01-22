using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueWindow : MonoBehaviour
    {
        [Serializable]
        protected struct GraphicControl
        {
            public Image Image;
            public Color Deflaut;
            public Color Preview;
        }
        
        [SerializeField] private GraphicControl CharacterGraphic;
        [SerializeField] private GraphicControl BodyGraphic;
        [field: SerializeField] public TMP_Text Character;
        [field: SerializeField] public TMP_Text BodyText;
        [field: SerializeField] public Button ContinueButton;

        public Action OnStartAnimationFinished, OnEndAnimationFinished;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (CharacterGraphic.Image)
            {
                CharacterGraphic.Image.color = CharacterGraphic.Deflaut;
            }

            if (BodyGraphic.Image)
            {
                BodyGraphic.Image.color = BodyGraphic.Deflaut;
            }
        }
#endif

        public void Commit()
        {
            CharacterGraphic.Image.color = CharacterGraphic.Deflaut;
            BodyGraphic.Image.color = BodyGraphic.Deflaut;
        }

        public void Preview()
        {
            CharacterGraphic.Image.color = CharacterGraphic.Preview;
            BodyGraphic.Image.color = BodyGraphic.Preview;
        }

        public void StartAnimationFinished() => OnStartAnimationFinished?.Invoke();
        public void EndAnimationFinished() => OnEndAnimationFinished?.Invoke();
    }
}
