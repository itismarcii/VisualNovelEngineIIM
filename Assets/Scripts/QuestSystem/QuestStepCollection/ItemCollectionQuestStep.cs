using System;
using ItemSystem;
using PlayerSystem;
using UnityEngine;

namespace QuestSystem.QuestStepCollection
{
    [Serializable]
    public sealed class ItemCollectionQuestStep : QuestStep
    {
        [Serializable]
        public class Container
        {
            [field: SerializeField] public ItemInfoScriptable CollectiveItem { get; private set; }
            [field: SerializeField] public int Amount { get; private set; }
            [HideInInspector] public bool IsFinished;

            public Container(in Container container)
            {
                CollectiveItem = container.CollectiveItem;
                Amount = container.Amount;
                IsFinished = false;
            }
        }

        [field: SerializeField] public Container[] Collective { get; private set; }

        public ItemCollectionQuestStep(in ItemCollectionQuestStep questStep)
        {
            Collective = new Container[questStep.Collective.Length];

            for (var index = 0; index < Collective.Length; index++)
            {
                var container = new Container(questStep.Collective[index]);
                Collective[index] = container;
            }
        }

        private void CheckAmount(ItemInfoScriptable item, uint amount)
        {
            if(!IsActive) return;
            
            var isFinished = true;
            
            foreach (var container in Collective)
            {
                if (container.CollectiveItem != item)
                {
                    isFinished = container.IsFinished != false && isFinished;
                    continue;
                }
                
                if (container.Amount < amount) return;
                
                container.IsFinished = true;
                item.OnPickUp -= CheckAmount;
            }
            
            if(isFinished) Finish();
        }

        public override bool CheckQuestStepFinished()
        {
            foreach (var container in Collective)
            {
                if (PlayerInventory.GetItemAmount(container.CollectiveItem) < container.Amount) return false;
            }

            return true;
        }

        public override QuestStep CreateNew() => new ItemCollectionQuestStep(this);

        public override void Start()
        {
            if (IsActive) return;

            if (!CheckQuestStepFinished())
            {
                SubscribeToPickUpEvent();
                IsActive = true;
            }
            else
            {
                IsActive = false;
                Finish();
            }
        }
        
        private void SubscribeToPickUpEvent()
        {
            foreach (var container in Collective)
            {
                container.CollectiveItem.OnPickUp += CheckAmount;
            }
        }
        
        private void UnsubscribeToPickUpEvent()
        {
            foreach (var container in Collective)
            {
                container.CollectiveItem.OnPickUp -= CheckAmount;
            }
        }
    }
}
