using System;
using CharacterSystem;
using LocationSystem;
using UnityEngine;

namespace QuestSystem
{
    public abstract class QuestStep
    {
        
        [field: SerializeField] public CharacterInfoScriptable Character { get; private set; }
        [field: SerializeField] public CharacterLocationInfoScriptable CharacterLocation { get; private set; }
        [field: SerializeField] public CharacterEmotion Emotion { get; private set; } = CharacterEmotion.NONE;
        
        protected bool IsActive = false;
        public Action OnFinish;
        
        public abstract bool CheckQuestStepFinished();
        public abstract QuestStep CreateNew();
        
        public virtual void Start()
        {
            var character = CharacterHandler.Characters[Character.ID];

            character.SetLocation(Location.Locations[CharacterLocation.ID] as CharacterLocation, Emotion);
        }
        
        protected void Finish()
        {

            CharacterHandler.Characters[Character.ID].SetToDefaultLocation();
            OnFinish?.Invoke();
            OnFinish = null;
            IsActive = false;
        }
    }
}
