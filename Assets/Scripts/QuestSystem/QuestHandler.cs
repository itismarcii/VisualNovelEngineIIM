using System;
using System.Collections.Generic;
using CharacterSystem;
using PlayerSystem;
using SceneEventSystem;
using UnityEngine;

namespace QuestSystem
{
    public static class QuestHandler
    {
        public static readonly Dictionary<int, Quest> Quests = new Dictionary<int, Quest>();
        public static readonly Dictionary<Quest, Character> OngoingQuests = new Dictionary<Quest, Character>();

        public static Action<Quest, Character> OnQuestStart;
        
        public static void GenerateQuests()
        {
            Quests.Clear();
            
            var questInfos = Resources.LoadAll<QuestInfoScriptable>("Quest");

            foreach (var questInfo in questInfos)
            {
                Quests.Add(questInfo.ID,  new Quest(questInfo));
            }
        }

        public static void GenerateQuests(in Quest[] quests)
        {
            Quests.Clear();

            foreach (var quest in quests)
            {
                Quests.Add(quest.QuestInfo.ID, quest);
            }
        }

        public static bool CheckRequirements(in Quest quest)
        {
            foreach (var questReq in quest.QuestInfo.QuestRequirement)
            {
                if (!Quests.TryGetValue(questReq.ID, out var questInfo)) continue;
                if (questInfo.QuestState != Quest.State.FINISHED) return false;
            }

            return true;
        }

        public static void StartQuest(in Quest quest, in Character character)
        {
            quest.OnQuestFinished += QuestFinish;
            OngoingQuests.Add(quest, character);
            OnQuestStart?.Invoke(quest, character);
            quest.StartQuest();
        }

        private static void QuestFinish(Quest quest)
        {
            var character = OngoingQuests[quest];
            OngoingQuests.Remove(quest);
            var reward = quest.QuestInfo.QuestReward;
            PlayerHandler.ApplyReward(reward);
            
            if(reward.LocationOverrides.Length > 0) 
                CharacterHandler.ApplyLocationOverride(quest.QuestInfo.QuestReward.LocationOverrides);

            if (reward.SceneEvents.Length > 0)
                SceneEventHandler.UnlockScene(reward.SceneEvents);
        }
    }
}
