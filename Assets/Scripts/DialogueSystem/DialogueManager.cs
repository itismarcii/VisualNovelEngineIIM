using System;
using System.Collections.Generic;
using CharacterSystem;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        [Serializable]
        public struct CharacterTagContainer
        {
            public string Name;
            public CharacterInfoScriptable CharacterInfo;
        }

        protected struct TagContainer
        {
            public string Name;
            public int ID;
        }

        public static DialogueManager Instance { get; private set; }

        public static Action OnStartDialogue, OnFinishDialogue;

        #region Inspector Access

        [Header("Prefabs")] 
        [SerializeField] private int MaxCharacterPlaces = 2;
        private static int s_MaxCharacterPlaces;
        
        [SerializeField] private Image CharacterPlace;
        private static Image s_CharacterPlace;
        
        [SerializeField] private ChoiceButton Button;
        private static ChoiceButton s_Button;

        [Header("Scene Objects")] 
        [SerializeField]
        private Canvas DialogueCanvas;

        private static Canvas s_DialogueCanvas;

        private static Canvas[] s_PreviousCanvasArray;

        [SerializeField] private ChoiceButtonGroup ChoiceArea;
        private static ChoiceButtonGroup s_ChoiceArea;

        [SerializeField] private DialogueWindow DialogueWindow;
        private static DialogueWindow s_DialogueWindow;

        [SerializeField] private Image CharacterTalking;
        private static Image s_CharacterTalking;

        [SerializeField] private RectTransform LeftCharacterPlace;
        private static RectTransform s_LeftCharacterPlace;
        
        [SerializeField] private RectTransform RightCharacterPlace;
        private static RectTransform s_RightCharacterPlace;

        #endregion

        #region Dialogue Tags

        private static readonly Dictionary<string, Dictionary<string, int>> TagDictionary = new();
        private static readonly Dictionary<int, Image> PlaceLayout = new();
        
        private const string SPEAKER = "speaker";
        private const string LAYOUT = "layout";
        private const string EMOTION = "emotion";

        #endregion

        private static int TalkingLayoutIndex;
        private static bool IsCanvasArrayActive = true;
        private static List<ChoiceButton> ChoiceButtons = new();

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                #region Assignment

                // Prefab
                s_Button = Button;

                // CANVAS
                s_DialogueCanvas = DialogueCanvas;
                s_DialogueCanvas.gameObject.SetActive(false);
                
                // Areas
                s_ChoiceArea = ChoiceArea;
                s_DialogueWindow = DialogueWindow;
                s_CharacterTalking = CharacterTalking;

                #endregion     
                
                // Clear choice field
                for (var i = 0; i < s_ChoiceArea.transform.childCount; i++)
                {
                    Destroy(s_ChoiceArea.transform.GetChild(i).gameObject);
                }

                GenerateCharacterPlaces();
                
            }
        }

        #region Setup

        public static void GenerateCharacterTags()
        {
            foreach (var character in CharacterHandler.Characters)
            {
                TagDictionary[SPEAKER].Add(character.Value.CharacterInfo.CharacterName.Trim(), character.Key);
            }
        }

        private static CharacterInfoScriptable GetCharacterInfo(in string name) =>
            CharacterHandler.Characters[TagDictionary[SPEAKER][name]].CharacterInfo;

        private void GenerateCharacterPlaces()
        {
            for (var i = 0; i < MaxCharacterPlaces * 2; i++)
            {
                PlaceLayout.Add(i, 
                    i % 2 == 0 
                        ? Instantiate(CharacterPlace, LeftCharacterPlace)
                        : Instantiate(CharacterPlace, RightCharacterPlace));

                PlaceLayout[i].gameObject.SetActive(false);
            }
        }

        #endregion

        #region Dialogue handling

        public static void StartDialogue(in DialogueInfoScriptable dialogueInfo, in Canvas[] previousActiveCanvas)
        {
            s_DialogueCanvas.gameObject.SetActive(true);
            s_PreviousCanvasArray = previousActiveCanvas;
            SetActivePreviousCanvasArray(false);
            DialogueHandler.StartDialogue(dialogueInfo.ID);
            ContinueDialogue();
            OnStartDialogue?.Invoke();
        }

        public static void ContinueDialogue()
        {
            if (DialogueHandler.HasChoices()) return;

            var dialogue = DialogueHandler.ContinueDialogue();

            if (dialogue == null)
            {
                HandleChoices(ArraySegment<Choice>.Empty);
                SetActivePreviousCanvasArray(true);
                DialogueHandler.FinishDialogue();
                OnFinishDialogue?.Invoke();
                return;
            }

            HandleTags(DialogueHandler.GetTags());
            s_DialogueWindow.BodyText.text = dialogue;
            HandleChoices(DialogueHandler.GetCurrentChoices());
        }

        private static void SetActivePreviousCanvasArray(in bool toggle)
        {
            if (IsCanvasArrayActive == toggle) return;

            foreach (var canvas in s_PreviousCanvasArray)
            {
                canvas.gameObject.SetActive(toggle);
            }

            IsCanvasArrayActive = toggle;
        }

        private static void HandleChoices(IReadOnlyCollection<Choice> choices)
        {
            foreach (var choiceButton in ChoiceButtons)
            {
                choiceButton.Destroy();
            }

            ChoiceButtons.Clear();

            if (choices.Count <= 0) return;

            foreach (var choice in choices)
            {
                var button = Instantiate(s_Button, s_ChoiceArea.transform);
                button.SetGroup(s_ChoiceArea);
                button.Text.text = choice.text;

                button.SelectAction(() =>
                {
                    s_DialogueWindow.Character.text = PlayerInfo.PlayerName;
                    s_DialogueWindow.BodyText.text = choice.text;
                    s_DialogueWindow.Preview();
                });

                button.SetAction(() =>
                {
                    s_DialogueWindow.Commit();
                    DialogueHandler.SelectChoice(choice);
                    ContinueDialogue();
                });

                ChoiceButtons.Add(button);
            }
        }

        #endregion

        #region Tag handling

        private static void HandleTags(IEnumerable<string> currentTags)
        {
            foreach (var storyTag in currentTags)
            {
                var splitTag = storyTag.Split(":");

                switch (splitTag.Length)
                {
                    case < 2:
                        continue;
                    case 2:
                        HandleTag(splitTag[0].Trim(), splitTag[1].Trim());
                        continue;
                    case 3:
                        HandleTag(splitTag[0].Trim(), splitTag[1].Trim(), splitTag[2].Trim());
                        continue;
                }
            }
        }

        private static void HandleTag(in string key, in string value0)
        {
            switch (key)
            {
                case SPEAKER:
                    var character = GetCharacterInfo(value0);
                    s_DialogueWindow.Character.text = character.CharacterName;
                    s_CharacterTalking.sprite = character.EmotionUI.Default;
                    break;
                case LAYOUT:
                    break;
            }
        }

        private static void HandleTag(in string key, in string value0, in string value1)
        {
            var character = GetCharacterInfo(value0);

            switch (key)
            {
                case SPEAKER:
                    var emotion = (CharacterEmotion) Enum.Parse(typeof(CharacterEmotion), value1.ToUpper());
                    var sprite = GetEmotionSprite(character, emotion);
                    s_DialogueWindow.Character.text = character.CharacterName;
                    s_CharacterTalking.sprite = sprite ? sprite : character.EmotionUI.Default;
                    break;
                case LAYOUT:
                    break;
            }
        }

        private static Sprite GetEmotionSprite(in CharacterInfoScriptable character, in CharacterEmotion emotion)
            => emotion switch
            {
                CharacterEmotion.NONE => null,
                CharacterEmotion.DEFAULT => character.EmotionUI.Default,
                CharacterEmotion.ANGRY => character.EmotionUI.Angry,
                CharacterEmotion.MAD => character.EmotionUI.Mad,
                CharacterEmotion.SAD => character.EmotionUI.Sad,
                CharacterEmotion.HAPPY => character.EmotionUI.Happy,
                CharacterEmotion.CONFUSED => character.EmotionUI.Confused,
                CharacterEmotion.AFRAID => character.EmotionUI.Afraid,
                _ => throw new ArgumentOutOfRangeException(nameof(emotion), emotion, null)
            };

        #endregion
        
    }
}
