using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

namespace DialogueSystem
{
    public static class DialogueHandler
    {
        public static readonly Dictionary<int, Story> Dialogues = new Dictionary<int, Story>();

        private static Story CurrentStory = null;

        public static void GenerateDialogues()
        {
            Dialogues.Clear();

            var dialogueInfos = Resources.LoadAll<DialogueInfoScriptable>("Dialogue");

            foreach (var dialogueInfo in dialogueInfos)
            {
                Dialogues.Add(dialogueInfo.ID, new Story(dialogueInfo.Dialogue.text));
            }
        }

        public static void StartDialogue(in int index)
        {
            if(CurrentStory is not null) return;
            CurrentStory = Dialogues[index];
            CurrentStory.ResetState();
        }

        public static string ContinueDialogue() => CurrentStory.canContinue ? CurrentStory.Continue() : null;
        public static Choice[] GetCurrentChoices() => CurrentStory.currentChoices.ToArray();
        public static string[] GetTags() => CurrentStory.currentTags.ToArray();
        public static void SelectChoice(in Choice choice) => CurrentStory.ChooseChoiceIndex(choice.index);
        public static bool HasChoices() => CurrentStory is null || CurrentStory.currentChoices.Count > 0;
        public static void FinishDialogue() => CurrentStory = null;
    }
}
