using System;
using DialogueSystem.TalkTo;
using LocationSystem;
using QuestSystem;
using SaveSystem;
using TimeSystem;
using UnityEngine;

namespace CharacterSystem
{
    [Savable, Serializable]
    public class Character
    {
        
        #region Save Data

        [field: SavableField] public int ID { get; private set; }
        [field: SavableField] public CharacterEmotion Emotion { get; private set; } = CharacterEmotion.DEFAULT;
        [field: SavableField] public int CharacterScale { get; private set; }
        [field: SavableField] public int ModeScale { get; private set; }
        [field: SavableField] public int LikeScale { get; private set; }
        [field: SavableField] public int QuestIndex { get; private set; } = 0;
        [field: SavableField] private int _LocationID;
        [field: SavableField] public LocationWeekIndexContainer LocationWeekSchedule { get; private set; }
        [field: SavableField] public TalkToIndexContainer TalkToIndexContainer { get; private set; }

        #endregion
        
        public CharacterInfoScriptable CharacterInfo { get; private set; }
        private Sprite _CharacterSprite;
        private CharacterLocation _CurrentLocation;
        public CharacterLocation CurrentLocation
        {
            get => _CurrentLocation;
            set
            {
                _CurrentLocation = value;
                _LocationID = _CurrentLocation.ID;
            }
        }
        
        #region Events

        // Character quest step talk to event call
        public Action<Character, QuestStep> OnTalkToQuestSystem;
        public Action<CharacterEmotion> OnEmotionChange;

        #endregion
        
        public Character(in CharacterInfoScriptable characterInfo)
        {
            ID = characterInfo.ID;
            CharacterInfo = characterInfo;
            _CharacterSprite = characterInfo.EmotionUI.Default;

            LocationWeekSchedule = new LocationWeekIndexContainer(characterInfo.DefaultLocation.LocationWeekObject);
            TalkToIndexContainer = new TalkToIndexContainer(characterInfo.DefaultTalkTo);
            
            TimeManager.OnNexDay += ModeBalance;

            SavableAttribute.RegisterObject(ID, this);
            SaveHandler.OnLoad += OnLoad;
        }
        
        ~Character()
        {
            SaveHandler.OnLoad -= OnLoad;
        }
        
        public void InfluenceMode(in int amount)
        {
            ModeScale = Math.Clamp(ModeScale + amount, -100, 100);
            SetEmotion();
        }
        
        private void ModeBalance(Day day)
        {
            switch (Emotion)
            {
                case CharacterEmotion.ANGRY:
                    InfluenceMode(CharacterInfo.ModeBalanceValue.x);
                    return;
                case CharacterEmotion.HAPPY:
                    InfluenceMode(-CharacterInfo.ModeBalanceValue.y);
                    return;
                default:
                    return;
            }
        }
        
        public void SetEmotion()
        {
            var previousEmotion = Emotion;
            Emotion = ModeScale switch
            {
                <= -50 => CharacterEmotion.ANGRY,
                > -50 and <= -1 => CharacterEmotion.MAD,
                > -1 and < 75 => CharacterEmotion.DEFAULT,
                >= 75 => CharacterEmotion.HAPPY
            };

            if (Emotion != previousEmotion) OnEmotionChange?.Invoke(Emotion);
        }

        protected void SetEmotion(in CharacterEmotion emotion)
        {
            if(Emotion == emotion) return;

            Emotion = emotion;
            OnEmotionChange?.Invoke(Emotion);
        }

        protected Quest GetCurrentQuest()
        {
            return QuestHandler.Quests[CharacterInfo.QuestQueue[QuestIndex].ID];
        }

        private void OnQuestFinished() => QuestIndex++;

        private Sprite SelectCharacterSprite(in CharacterEmotion emotion)
        {
            return emotion switch
            {
                CharacterEmotion.DEFAULT => CharacterInfo.EmotionUI.Default,
                CharacterEmotion.ANGRY => CharacterInfo.EmotionUI.Angry,
                CharacterEmotion.MAD => CharacterInfo.EmotionUI.Mad,
                CharacterEmotion.SAD => CharacterInfo.EmotionUI.Sad,
                CharacterEmotion.HAPPY => CharacterInfo.EmotionUI.Happy,
                CharacterEmotion.CONFUSED => CharacterInfo.EmotionUI.Confused,
                CharacterEmotion.AFRAID => CharacterInfo.EmotionUI.Afraid,
                CharacterEmotion.NONE => _CharacterSprite,
                _ => throw new ArgumentOutOfRangeException(nameof(emotion), emotion, null)
            };
        }

        public void SetToDefaultLocation()
        {
            CurrentLocation.Deactivate();
            SetEmotion();
            CurrentLocation = GetDefaultLocation(this);
        }
        

        public void SetLocation(in CharacterLocation characterLocation, in CharacterEmotion emotion = CharacterEmotion.NONE)
        {
            CurrentLocation.Deactivate();
            CurrentLocation = characterLocation;
            CurrentLocation.Activate(SelectCharacterSprite(emotion));
        }

        public void OnLoad()
        {
            SetEmotion();
            SetLocation(Location.Locations[_LocationID] as CharacterLocation, Emotion);
        }

        #region Static Methods

        private static CharacterLocation GetDefaultLocation(in Character character) =>
            Location.Locations[GetLocationID(character)] as CharacterLocation;
        
        private static int GetLocationID(in Character character)
        {
            return TimeManager.Day switch
            {
                Day.MONDAY => GetLocationIDForDay(character.LocationWeekSchedule.Monday),
                Day.TUESDAY => GetLocationIDForDay(character.LocationWeekSchedule.Tuesday),
                Day.WEDNESSDAY => GetLocationIDForDay(character.LocationWeekSchedule.Wednesday),
                Day.THURSDAY => GetLocationIDForDay(character.LocationWeekSchedule.Thursday),
                Day.FRIDAY => GetLocationIDForDay(character.LocationWeekSchedule.Friday),
                Day.SATURDAY => GetLocationIDForDay(character.LocationWeekSchedule.Saturday),
                Day.SUNDAY => GetLocationIDForDay(character.LocationWeekSchedule.Sunday),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static int GetLocationIDForDay(LocationWeekIndexContainer.TimeCycleContainer day)
        {
            return TimeManager.Cycle switch
            {
                TimeCycle.MIDNIGHT => day.MidnightID,
                TimeCycle.EARLY_MORNING => day.EarlyMorningID,
                TimeCycle.MORNING => day.MorningID,
                TimeCycle.LATE_MORNING => day.LateMorningID,
                TimeCycle.NOON => day.NoonID,
                TimeCycle.AFTERNOON => day.AfternoonID,
                TimeCycle.EARLY_EVENING => day.EarlyEveningID,
                TimeCycle.EVENING => day.EveningID,
                TimeCycle.LATE_EVENING => day.LateEveningID,
                TimeCycle.NIGHT => day.NightID,
                TimeCycle.LATE_NIGHT => day.LateNightID,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #endregion
    }
}
