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

        public void SetLabels(string itemName, int itemAmount)
        {
            m_itemName.text = itemName;
            m_itemAmount.text = itemAmount.ToString();
        }
    }
}