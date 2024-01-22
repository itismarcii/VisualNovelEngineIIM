using System.Collections.Generic;
using ItemSystem;

namespace PlayerSystem
{
    public static class PlayerInventory
    {
        public static uint Money { get; private set; }
        public static uint Experience { get; private set; }

        private static readonly Dictionary<ItemInfoScriptable, uint> Inventory = new Dictionary<ItemInfoScriptable, uint>();

        public static void AddItem(in ItemInfoScriptable item, in uint amount = 1)
        {
            if(amount <= 0) return;
            
            if (Inventory.ContainsKey(item))
            {
                Inventory[item] += amount;
            }
            else
            {
                Inventory.Add(item, amount);
            }

            item.OnPickUp(item,Inventory[item]);
        }

        public static bool RemoveItem(in ItemInfoScriptable item, in uint amount = 1)
        {
            if (amount <= 0 || !Inventory.ContainsKey(item) || Inventory[item] < amount) return false;
            
            if (Inventory[item] == amount)
            {
                Inventory.Remove(item);
            }
            else
            {
                Inventory[item] -= amount;
            }

            return true;
        }

        public static uint GetItemAmount(in ItemInfoScriptable item) =>
            !Inventory.ContainsKey(item) ? (uint) 0 : Inventory[item];

        public static void AddMoney(uint money) => Money += money;
        public static void RemoveMoney(uint money) => Money = money > Money ? 0 : Money - money; 
        
        
    }
}
