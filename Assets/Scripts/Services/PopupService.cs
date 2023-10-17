using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tofu.TurnBased.Services;
using UnityEngine;

public class PopupService : ServiceBase<PopupService>
{
    [SerializeField] private GameObject m_itemUsedPopUp;
    [SerializeField] private TMP_Text m_itemUsedPopupText;
    [SerializeField] private GameObject m_itemAddedPopUp;
    [SerializeField] private TMP_Text m_itemAddedPopupText;
    private bool m_isPopupOpen = false;

    private void Start()
    {
        m_itemUsedPopUp.SetActive(false);
        m_itemAddedPopUp.SetActive(false);
    }

    void Update()
    {
        CloseItemPopup();
    }
    
    void CloseItemPopup()
    {
        // Temporary popup until items actually get used properly
        if (m_isPopupOpen && Input.anyKeyDown)
        {
            m_itemUsedPopUp.SetActive(false);
            m_itemAddedPopUp.SetActive(false);
            m_isPopupOpen = false;
        }
    }
    
    public void ItemAddedPopup(UsableItemToken item, int itemAmount)
    {
        m_itemAddedPopupText.text = "You found " + item.name + " x" + itemAmount;
        m_itemAddedPopUp.SetActive(true);
        m_isPopupOpen = true;
    }
    public void ItemUsedPopup(UsableItemToken item)
    {
        // Temporary popup until items actually get used properly
        m_itemUsedPopupText.text = item.name + " used";
        m_itemUsedPopUp.SetActive(true);
        m_isPopupOpen = true;
    }
}
