using System;
using System.Collections.Generic;
using CharacterSystem;
using LocationSystem;
using UnityEngine;

namespace QuestSystem.QuestStepCollection
{
    [Serializable]
    public sealed class TalkToQuestStep : QuestStep
    {
        [Serializable]
        public class Container
        {
            [field: SerializeField] public CharacterInfoScriptable Character { get; private set; }
            [field: SerializeField] public CharacterLocationInfoScriptable Location { get; private set; }
            [field: SerializeField] public CharacterEmotion Emotion { get; private set; }
            [field: SerializeField] public string EventDialogue { get; private set; }
            [field: SerializeField] public string EventScene { get; private set; }

            public Container(in Container container)
            {
                Character = container.Character;
                EventDialogue = container.EventDialogue;
                EventScene = container.EventScene;
            }
        }

        [field: SerializeField] public Container[] Characters { get; private set; }
        public List<CharacterInfoScriptable> CharacterTalkedTo { get; private set; } = new List<CharacterInfoScriptable>();
        
        public TalkToQuestStep(in TalkToQuestStep questStep)
        {
            Characters = new Container[questStep.Characters.Length];

            for (var index = 0; index < Characters.Length; index++)
            {
                var container = new Container(questStep.Characters[index]);
                Characters[index] = container;
            }
        }

        public override bool CheckQuestStepFinished() => CharacterTalkedTo.Count == Characters.Length;

        public override QuestStep CreateNew() => new TalkToQuestStep(this);
        
        public override void Start()
        {
            if(IsActive) return;

            if(CheckQuestStepFinished())
            {
                Finish();
                return;
            }
            
            foreach (var container in Characters)
            {
                if (!CharacterHandler.Characters.TryGetValue(container.Character.ID, out var character)) continue;

                if(container.Location != null)
                {
                    character.SetLocation(container.Location.GetLocation() as CharacterLocation, container.Emotion);
                }
                
                character.OnTalkToQuestSystem += TalkedTo;
            }
            
            IsActive = true;
        }

        private void TalkedTo(Character character, QuestStep questStep)
        {
            if (questStep != this) return;

            CharacterTalkedTo.Add(character.CharacterInfo);
            character.OnTalkToQuestSystem -= TalkedTo;
            
            if(CheckQuestStepFinished()) Finish();
        }
    }
}
