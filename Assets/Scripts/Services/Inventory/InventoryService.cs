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
        private PopupService m_popupService;
        [SerializeField] private InventoryListEntry inventoryListEntryTemplate;
        [SerializeField] private Transform m_listContainer;
        [SerializeField] private StatsListEntry statsListEntryTemplate;
        [SerializeField] private Transform m_statsContainer;
        [SerializeField] private Button m_useItemButton;
        private bool m_useItemButtonPressed = false;
        private InventoryListEntry m_itemButtonActive = null;
        [SerializeField] private GameObject m_useItemHighlight;
        [SerializeField] private Button m_throwAwayItemButton;
        [SerializeField] private GameObject m_throwAwayItemHighlight;
        private bool m_throwAwayItemButtonPressed = false;
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
            m_popupService = ServiceLocator.GetService<PopupService>();
            HideInventoryUI();
        }

        private void Update()
        {
            // Not actually expensive, if used the menu closes
            InventoryNavigation();
        }

        public void MakeInventoryUIVisible()
        {
            this.gameObject.SetActive(true);
            UnselectButtons();
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
            m_popupService.ItemAddedPopup(addItem, itemAmount);
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
            for (int i = 0; i < m_listContainer.childCount; i++)
            {
                Destroy(m_listContainer.GetChild(i).gameObject);
            } 
            foreach (var item in m_ownedUsableItems) 
            {
                InventoryListEntry entry = Instantiate(inventoryListEntryTemplate, m_listContainer);
                entry.PopulateEntry(item.Key.name, item.Value, item.Key);
                entry.gameObject.SetActive(true);
            }
        }

        public void FillCharacterStats()
        {
            m_statsService.PopulateCharacterStats(statsListEntryTemplate , m_statsContainer);
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
            m_throwAwayItemHighlight.SetActive(false);
            m_useItemButtonPressed = true;
            m_useItemHighlight.SetActive(true);
            SetItemButtonsToActive();
        }

        public void ThrowAwayButtonPressed()
        {
            m_useItemButtonPressed = false;
            m_useItemHighlight.SetActive(false);
            m_throwAwayItemButtonPressed = true;
            m_throwAwayItemHighlight.SetActive(true);
            SetItemButtonsToActive();
        }
        public void SetItemButtonsToActive()
        {
            Button[] itemEntryButtons = m_listContainer.GetComponentsInChildren<Button>();
            foreach (Button button in itemEntryButtons)
            {
                button.interactable = true;
            }
        }
        public void SetItemButtonsToInactive()
        {
            Button[] itemEntryButtons = m_listContainer.GetComponentsInChildren<Button>();
            foreach (Button button in itemEntryButtons)
            {
                button.interactable = false;
            }
        }
        public void ItemButtonPressed(InventoryListEntry listEntry)
        {
            if (m_itemButtonActive != null)
            {
                m_itemButtonActive.Unhighlight();
            }
            m_itemButtonActive = listEntry;
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
            Button[] characterEntryButtons = m_statsContainer.GetComponentsInChildren<Button>();
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
            Button[] characterEntryButtons = m_statsContainer.GetComponentsInChildren<Button>();
            foreach (Button button in characterEntryButtons)
            {
                button.interactable = false;
            }
        }

        void UseItem()
        {
            m_popupService.ItemUsedPopup(m_itemCurrentlySelected);
            RemoveItemFromInventory(m_itemCurrentlySelected, 1);
            // Temporary popup until items actually get used properly
            FillInventoryUI();
            FillCharacterStats();
            UnselectButtons();
        }

        void ThrowAwayItem(UsableItemToken item)
        {
            RemoveItemFromInventory(item, 1); 
            FillInventoryUI();
            UnselectButtons();
        }

        void UnselectButtons()
        {
            if (m_itemButtonActive != null)
            {
                m_itemButtonActive.Unhighlight();
            }
            m_itemButtonActive = null;
            SetCharacterButtonsToInactive();
            SetItemButtonsToInactive();
            m_itemCurrentlySelected = null;
            m_useItemButtonPressed = false;
            m_throwAwayItemButtonPressed = false;
            m_throwAwayItemHighlight.SetActive(false);
            m_useItemHighlight.SetActive(false);
        }
        
    }
    
}