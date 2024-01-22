using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class ChoiceButtonGroup : MonoBehaviour
    {
        private readonly List<ChoiceButton> DialogueButtons = new List<ChoiceButton>();

        public void Subscribe(in ChoiceButton button) => DialogueButtons.Add(button);
        public void Unsubscribe(in ChoiceButton button) => DialogueButtons.Remove(button);

        private ChoiceButton ActiveButton;
        
        public void OnSelect(in ChoiceButton button)
        {
            if (ActiveButton == button)
            {
                ActiveButton.Action();
                ActiveButton = null;
                DialogueButtons.Clear();
                return;
            }
            
            ActiveButton = button;
            ActiveButton.SelectColorChange();
            ActiveButton.Selection();
            ResetButtons();
        }
        
        public void OnHover(in ChoiceButton button)
        {
            if(ActiveButton == button) return;

            ResetButtons();
            button.HoverColorChange();
        }

        public void OnDeselect(in ChoiceButton button)
        {
            if(ActiveButton == button) return;
            ResetButtons();
        }

        private void ResetButtons()
        {
            foreach (var button in DialogueButtons)
            {
                if(button == ActiveButton) continue;
                button.DeselectColorChange();
            }        
        }
        
    }
}
