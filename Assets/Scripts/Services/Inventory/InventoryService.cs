using System;
using System.Collections;using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using TMPro;
using Tofu.TurnBased.Quests;
using UnityEngine;
using Tofu.TurnBased.Services;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tofu.TurnBased.Inventory
{
    public class InventoryService : ServiceBase<InventoryService>
    {
        private GameObject m_InventoryUi;
        
        [SerializeField] private InventoryListEntry inventoryListEntrytemplate;
        [SerializeField] private Transform listcontainer;
        private ControlsService m_controlsService;
        private PlayerMenuService m_playerMenuService;
        [SerializeField] private Button m_useItemButton;
        private bool m_useItemButtonPressed = false;
        [SerializeField] private Button m_throwAwayItemButton;
        private bool m_throwAwayItemButtonPressed = false;
        

        private Dictionary<UsableItemToken, int> m_ownedUsableItems = new Dictionary<UsableItemToken, int>(); 
        public Dictionary<UsableItemToken, int> ownedUsableItems
        {
            get { return m_ownedUsableItems; }
        }

        public void Start()
        {
            m_controlsService = ServiceLocator.GetService<ControlsService>();
            m_playerMenuService = ServiceLocator.GetService<PlayerMenuService>();
            HideInventoryUI();
        }

        private void Update()
        {
            InventoryNavigation();
        }

        public void MakeInventoryUIVisible()
        {
            this.gameObject.SetActive(true);
        }

        void HideInventoryUI()
        {
            EventSystem.current.SetSelectedGameObject(null);
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

        public void FillInventoryUI()
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
        void InventoryNavigation()
        {
            if (m_controlsService.isCancelDown)
            {
                m_playerMenuService.MakeMenuUIVisible();
                m_throwAwayItemButtonPressed = false;
                m_useItemButtonPressed = false;
                HideInventoryUI();
            }
        }

        public void UseItemButton()
        {
            m_throwAwayItemButtonPressed = false;
            m_useItemButtonPressed = true;
        }

        public void ThrowAwayButtonPressed()
        {
            m_useItemButtonPressed = false;
            m_throwAwayItemButtonPressed = true;
        }

        public void ItemButtonPressed()
        {
            if (m_useItemButtonPressed)
            {
                UseItem();
                EventSystem.current.SetSelectedGameObject(m_useItemButton.gameObject);
            }
            else if (m_throwAwayItemButtonPressed)
            {
                ThrowAwayItem();
                EventSystem.current.SetSelectedGameObject(m_throwAwayItemButton.gameObject);
            }
        }

        void UseItem()
        {
            print("Item Used");
        }

        void ThrowAwayItem()
        {
            print("Item Thrown Away");
        }
    }
    
}