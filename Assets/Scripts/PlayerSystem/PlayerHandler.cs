using System;
using QuestSystem;

namespace PlayerSystem
{
    public static class PlayerHandler
    {
        public static void ApplyReward(in QuestReward reward)
        {
            foreach (var item in reward.Items)
            {
                PlayerInventory.AddItem(item);
            }

            switch (reward.Money)
            {
                case > 0:
                    PlayerInventory.AddMoney((uint)reward.Money);
                    break;
                case < 0:
                    PlayerInventory.RemoveMoney((uint)-reward.Money);
                    break;
            }
            
            // Unlock Scene
        }
    }
}
