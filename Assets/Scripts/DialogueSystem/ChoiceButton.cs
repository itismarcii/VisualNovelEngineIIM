using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class ChoiceButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Serializable]
        protected struct GraphicControl
        {
            public Image Image;
            public Color Select;
            public Color Hover;
            public Color Deselect;
        }

        [SerializeField] private GraphicControl Graphic;
        [SerializeField] private ChoiceButtonGroup choiceGroup;
        [field: SerializeField] public TMP_Text Text { get; private set; }

        private Action OnAction, OnSelect;
        
        public void Destroy() => Destroy(gameObject);

        private void Start()
        {
            if (!choiceGroup) choiceGroup = GetComponentInParent<ChoiceButtonGroup>();
            choiceGroup.Subscribe(this);
            DeselectColorChange();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(!Graphic.Image) return;
            DeselectColorChange();
        }
#endif

        public void OnPointerEnter(PointerEventData eventData) => choiceGroup.OnHover(this);
        public void OnPointerClick(PointerEventData eventData) => choiceGroup.OnSelect(this);
        public void OnPointerExit(PointerEventData eventData) => choiceGroup.OnDeselect(this);
        public void SelectColorChange() => Graphic.Image.color = Graphic.Select;
        public void HoverColorChange() => Graphic.Image.color = Graphic.Hover;
        public void DeselectColorChange() => Graphic.Image.color = Graphic.Deselect;
        public void SetGroup(ChoiceButtonGroup groupArea) => choiceGroup = groupArea;
        public void SetAction(in Action action) => OnAction = action;
        public void SelectAction(Action action) => OnSelect = action;
        public void Action() => OnAction?.Invoke();
        public void Selection() => OnSelect?.Invoke();

        private void OnDestroy()
        {
            OnAction = null;
            OnSelect = null;
        }
    }
}
