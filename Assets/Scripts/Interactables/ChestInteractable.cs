using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Interactables;
using Tofu.TurnBased.Inventory;
using UnityEngine;
using Tofu.TurnBased.Services;

namespace Tofu.TurnBased.Chests
{
    public class ChestInteractable : Interactable
    {
        [SerializeField] private ChestToken m_chestToken;
        [SerializeField] private UsableItemToken m_Item;
        [SerializeField] private int m_itemAmount;
        [SerializeField] GameObject m_chestTop;
        [SerializeField] private float m_chestOpenAngle;
        private InventoryService m_inventoryService;

        protected override void Start()
        {
            base.Start();
            m_inventoryService = ServiceLocator.GetService<InventoryService>();
            if (m_chestToken.isOpened)
            {
                m_chestTop.transform.Rotate(m_chestOpenAngle, 0, 0, Space.Self);
            }
            
        }
        
        protected override void Interaction()
        {
            if (!m_chestToken.isOpened)
            {
                m_chestToken.isOpened = true;
                m_chestTop.transform.Rotate(m_chestOpenAngle, 0, 0, Space.Self);
                m_inventoryService.AddItemToInventory(m_Item, m_itemAmount);
                m_inventoryService.TestInventory();
            }
        }
    }
}