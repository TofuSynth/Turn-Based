using System.Collections;using System.Collections.Generic;
using UnityEngine;
using Tofu.TurnBased.Services;

namespace Tofu.TurnBased.Inventory
{
    public class InventoryService : ServiceBase<InventoryService>
    {
        private Dictionary<UsableItemToken, int> m_ownedUsableItems = new Dictionary<UsableItemToken, int>();

        public void AddItemToInventory(UsableItemToken addItem, int itemAmount)
        {
            if (!m_ownedUsableItems.ContainsKey(addItem))
            {
                m_ownedUsableItems.Add(addItem, itemAmount);
            }
            else
            {
                m_ownedUsableItems[addItem] += itemAmount;
            }
        }

        public void RemoveItemFromInventory(UsableItemToken removeItem)
        {
            m_ownedUsableItems[removeItem] -= 1;
            if (m_ownedUsableItems[removeItem] <= 0)
            {
                m_ownedUsableItems.Remove(removeItem);
            }
        }

        public void TestInventory()
        {
            foreach (var x in m_ownedUsableItems)
            {
                print(x);
            }
        }
    }
}