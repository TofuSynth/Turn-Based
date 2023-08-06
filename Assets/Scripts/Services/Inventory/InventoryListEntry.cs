using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tofu.TurnBased.Inventory
{
    public class InventoryListEntry : MonoBehaviour
    {
        [SerializeField] private Button m_itemButton;
        [SerializeField] private TMP_Text m_itemName;
        [SerializeField] private TMP_Text m_itemAmount;
        [SerializeField] private UsableItemToken m_usableItemToken;
        [SerializeField] private GameObject m_itemHighlight;
        public UsableItemToken UsableItemToken
        {
            get { return m_usableItemToken; }
        }

        public void PopulateEntry(string itemName, int itemAmount, UsableItemToken token)
        {
            m_itemName.text = itemName;
            m_itemAmount.text = itemAmount.ToString();
            m_usableItemToken = token;
        }
    }
}