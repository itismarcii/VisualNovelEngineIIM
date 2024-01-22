using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Tab
{
    public class TabGroup : MonoBehaviour
    {
        private List<TabButton> _TabButtons = new List<TabButton>();
        [SerializeField] private GameObject[] _TabGameObjects;
        private Sprite _Idle;
        private Sprite _Hover;
        private Sprite _Active;
        private TabButton _ActiveTab;
        
        public void Subscribe(in TabButton tap) => _TabButtons.Add(tap);
        public void Unsubscribe(in TabButton tap) => _TabButtons.Remove(tap);
        
        private void Awake()
        {
            var tabs = GetComponentsInChildren<TabButton>();
            
            for (var i = 0; i < tabs.Length; i++)
            {
                tabs[i].SetIndexGroup(i);
            }
        }
        
        public void OnTabEnter(in TabButton tap)
        {
            if(tap == _ActiveTab) return;

            ResetTabs();
            tap.TabHoverImageChange();
        }

        public void OnTabExit(in TabButton tap)
        {
            if(tap == _ActiveTab) return;
            ResetTabs();
        }

        public void OnTabSelected(in TabButton tap)
        {
            
            _ActiveTab = tap;
            _ActiveTab.Select();
            
            ResetTabs(); 

            var index = tap.IndexGroup;
            
            for (var i = 0; i < _TabGameObjects.Length; i++)
            {
                _TabGameObjects[i].SetActive(i == index);
            }
        }
        
        public void ResetTabs()
        {
            foreach (var tap in _TabButtons)
            {
                if(tap == _ActiveTab) continue;
                tap.TabDeselectImageChange();
            }
        }

    }
}
