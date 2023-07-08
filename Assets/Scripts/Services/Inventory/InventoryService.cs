using System;
using System.Collections;using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using TMPro;
using Tofu.TurnBased.Quests;
using UnityEngine;
using Tofu.TurnBased.Services;
using UnityEngine.UI;

namespace Tofu.TurnBased.Inventory
{
    public class InventoryService : ServiceBase<InventoryService>
    {
        private GameObject m_InventoryUi;
        
        [SerializeField] private InventoryListEntry inventoryListEntrytemplate;
        [SerializeField] private Transform listcontainer;

        private Dictionary<UsableItemToken, int> m_ownedUsableItems = new Dictionary<UsableItemToken, int>();
        public Dictionary<UsableItemToken, int> ownedUsableItems
        {
            get { return m_ownedUsableItems; }
        }

        public void Start()
        {
            HideInventoryUI();
        }

        public void MakeInventoryUIVisible()
        {
            this.gameObject.SetActive(true);
        }

        void HideInventoryUI()
        {
            this.gameObject.SetActive(false);
        }
        
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
            CheckIfItemCompletesQuestConditional();
        }

        public void CheckIfItemCompletesQuestConditional()
        {
            QuestManagementService m_questUpdate = ServiceLocator.GetService<QuestManagementService>();
            if (!m_questUpdate)
            {
                foreach (QuestToken quest in m_questUpdate.activeQuests.Keys)
                {
                    m_questUpdate.CheckIfRequiredItemIsObtained(quest);
                }
            }
        }

        public void RemoveItemFromInventory(UsableItemToken removeItem, int itemAmount)
        {
            m_ownedUsableItems[removeItem] -= itemAmount;
            if (m_ownedUsableItems[removeItem] <= 0)
            {
                m_ownedUsableItems.Remove(removeItem);
            }
        }

        public void TestInventory()
        { 
            for (int i = 0; i < listcontainer.childCount; i++)
            {
                Destroy(listcontainer.GetChild(i).gameObject);
            } 
            foreach (var item in m_ownedUsableItems) 
            {
                InventoryListEntry entry = Instantiate(inventoryListEntrytemplate, listcontainer);
                entry.SetLabels(item.Key.name, item.Value);
                entry.gameObject.SetActive(true); 
            }
            
        }
        
    }
    
}