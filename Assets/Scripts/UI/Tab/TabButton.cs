using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Tab
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        
        [Serializable]
        public struct Graphics
        {
            public float Size;
            public Image Image;
            public TMP_Text Text;
        }
        
        [Serializable]
        public struct TabColors
        {
            public float Size;
            public Color Element0;
            public Color Element1;
        }
        
        private int _IndexGroup;

        [SerializeField] private TabGroup _TabGroup;
        [SerializeField] private Graphics Graphic;
        [SerializeField] private TabColors ActiveColor;
        [SerializeField] private TabColors HoverColor;
        [SerializeField] private TabColors InactiveColor;

        public int IndexGroup => _IndexGroup;
        
        public UnityEvent OnTabSelect;

        public void OnPointerEnter(PointerEventData eventData) => _TabGroup.OnTabEnter(this);
        
        public void OnPointerClick(PointerEventData eventData) => _TabGroup.OnTabSelected(this);
        
        public void OnPointerExit(PointerEventData eventData) => _TabGroup.OnTabExit(this);
        public void SetIndexGroup(in int index) => _IndexGroup = index;
        
        private void Awake()
        {
            Graphic.Image.color = InactiveColor.Element0;
            _TabGroup.Subscribe(this);
        }

        public void Select()
        {
            TabSelectImageChange();
            OnTabSelect?.Invoke();
        } 
        
        private void TabSelectImageChange()
        {
            Graphic.Image.color = ActiveColor.Element0;
        }

        public void TabDeselectImageChange()
        {
            Graphic.Image.color = InactiveColor.Element0;

        }

        public void TabHoverImageChange()
        {
            Graphic.Image.color = HoverColor.Element0;
        }
    }
}
