using System;
using SaveSystem;

namespace QuestSystem
{
    [Savable, Serializable]
    public class Quest
    {
        [Serializable]
        public enum State
        {
            REQUIREMENTS_NOT_MET,
            CAN_START,
            IN_PROGRESS,
            CAN_FINISH,
            FINISHED
        }

        #region Save Data

        [field: SavableField] public int ID { get; private set; }
        [field: SavableField] public State QuestState { get; private set; } = State.REQUIREMENTS_NOT_MET;
        [field: SavableField] public int CurrentQuestStepIndex { get; private set; } = 0;

        #endregion
        
        public QuestInfoScriptable QuestInfo { get; private set; }
        public QuestStep[] QuestSteps { get; private set; }

        public Action<Quest> OnQuestFinished;

        public Quest(in QuestInfoScriptable questInfo)
        {
            ID = questInfo.ID;
            QuestInfo = questInfo;
            
            QuestSteps = new QuestStep[questInfo.QuestSteps.Length];

            for (var index = 0; index < questInfo.QuestSteps.Length; index++)
            {
                QuestSteps[index] = questInfo.QuestSteps[index].GetQuestStep().CreateNew();
            }

            SavableAttribute.RegisterObject(ID ,this);
            SaveHandler.OnLoad += OnLoad;
        }

        ~Quest()
        {
            SaveHandler.OnLoad -= OnLoad;
        }

        public bool StartQuest()
        {
           if(!QuestHandler.CheckRequirements(this)) return false;
           
            QuestState = State.IN_PROGRESS;
            QuestSteps[CurrentQuestStepIndex].OnFinish += NextQuestStep;
            QuestSteps[CurrentQuestStepIndex].Start();
            
            return true;
        }

        public void NextQuestStep()
        {
            if (QuestState is State.CAN_FINISH || !QuestSteps[CurrentQuestStepIndex].CheckQuestStepFinished()) return;
            
            if (++CurrentQuestStepIndex >= QuestSteps.Length)
            {
                QuestState = State.CAN_FINISH;
            }
            else
            {
                var questStep = QuestSteps[CurrentQuestStepIndex];
                questStep.OnFinish += NextQuestStep;
                questStep.Start();
            }
        }

        public bool FinishQuest()
        {
            if (QuestState != State.CAN_FINISH) return false;
            
            QuestState = State.FINISHED;
            OnQuestFinished?.Invoke(this);
            return true;
        }

        public void OnLoad()
        {
            if(QuestState is not State.IN_PROGRESS) return;
            
            QuestSteps[CurrentQuestStepIndex].OnFinish += NextQuestStep;
            QuestSteps[CurrentQuestStepIndex].Start();
            NextQuestStep();
        }
    }
}
