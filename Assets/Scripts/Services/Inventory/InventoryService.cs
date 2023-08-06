using System;
using System.Collections;using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using TMPro;
using Tofu.TurnBased.Quests;
using UnityEngine;
using Tofu.TurnBased.Services;
using Tofu.TurnBased.Stats;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tofu.TurnBased.Inventory
{
    public class InventoryService : ServiceBase<InventoryService>
    {
        private GameObject m_InventoryUi;
        private ControlsService m_controlsService;
        private PlayerMenuService m_playerMenuService;
        private StatsService m_statsService;
        
        [SerializeField] private InventoryListEntry inventoryListEntryTemplate;
        [SerializeField] private Transform listContainer;
        [SerializeField] private StatsListEntry statsListEntryTemplate;
        [SerializeField] private Transform statsContainer;
        [SerializeField] private Button m_useItemButton;
        private bool m_useItemButtonPressed = false;
        [SerializeField] private GameObject m_useItemHighlight;
        [SerializeField] private Button m_throwAwayItemButton;
        [SerializeField] private GameObject m_throwAwayItemHighlight;
        private bool m_throwAwayItemButtonPressed = false;
        [SerializeField] private GameObject characterInfoGameObject;
        private UsableItemToken m_itemCurrentlySelected;
        

        private Dictionary<UsableItemToken, int> m_ownedUsableItems = new Dictionary<UsableItemToken, int>(); 
        public Dictionary<UsableItemToken, int> ownedUsableItems
        {
            get { return m_ownedUsableItems; }
        }

        public void Start()
        {
            m_controlsService = ServiceLocator.GetService<ControlsService>();
            m_playerMenuService = ServiceLocator.GetService<PlayerMenuService>();
            m_statsService = ServiceLocator.GetService<StatsService>();
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
            for (int i = 0; i < listContainer.childCount; i++)
            {
                Destroy(listContainer.GetChild(i).gameObject);
            } 
            foreach (var item in m_ownedUsableItems) 
            {
                InventoryListEntry entry = Instantiate(inventoryListEntryTemplate, listContainer);
                entry.PopulateEntry(item.Key.name, item.Value, item.Key);
                entry.gameObject.SetActive(true);
            }
        }

        public void FillCharacterStats()
        {
            m_statsService.PopulateCharacterStats(statsListEntryTemplate , statsContainer);
        }
        
        void InventoryNavigation()
        {
            if (m_controlsService.isCancelDown)
            {
                UnselectButtons();
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
            m_useItemHighlight.SetActive(true);
        }

        public void ThrowAwayButtonPressed()
        {
            m_useItemButtonPressed = false;
            m_throwAwayItemButtonPressed = true;
            m_throwAwayItemHighlight.SetActive(true);
        }

        public void ItemButtonPressed(InventoryListEntry listEntry)
        {
            m_itemCurrentlySelected = listEntry.UsableItemToken;
            if (m_throwAwayItemButtonPressed)
            {
                ThrowAwayItem(m_itemCurrentlySelected);
            }
            else if (m_useItemButtonPressed)
            {
                SetCharacterButtonsToActive();
                listEntry.Highlight();
            }
        }

        public void SetCharacterButtonsToActive()
        {
            Button[] characterEntryButtons = characterInfoGameObject.GetComponentsInChildren<Button>();
            foreach (Button button in characterEntryButtons)
            {
                button.interactable = true;
            }
        }

        public void CharacterButtonPressed()
        {
            if (m_useItemButtonPressed)
            {
                UseItem();
            }
        }

        public void SetCharacterButtonsToInactive()
        {
            Button[] characterEntryButtons = characterInfoGameObject.GetComponentsInChildren<Button>();
            foreach (Button button in characterEntryButtons)
            {
                button.interactable = false;
            }
        }

        void UseItem()
        {
            print("Item Used");
            UnselectButtons();
        }

        void ThrowAwayItem(UsableItemToken item)
        {
            RemoveItemFromInventory(item, 1); 
            UnselectButtons();
        }

        void UnselectButtons()
        {
            SetCharacterButtonsToInactive();
            m_itemCurrentlySelected = null;
            m_useItemButtonPressed = false;
            m_throwAwayItemButtonPressed = false;
            m_throwAwayItemHighlight.SetActive(false);
            m_useItemHighlight.SetActive(false);
        }
    }
    
}