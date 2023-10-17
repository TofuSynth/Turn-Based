using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tofu.TurnBased.Services;
using UnityEngine;

public class PopupService : ServiceBase<PopupService>
{
    private GameStateService m_gameStateService;
    [SerializeField] private GameObject m_itemUsedPopUp;
    [SerializeField] private TMP_Text m_itemUsedPopupText;
    [SerializeField] private GameObject m_itemAddedPopUp;
    [SerializeField] private TMP_Text m_itemAddedPopupText;
    private bool m_isAddedPopupOpen = false;
    private bool m_isUsedPopupOpen = false;

    private void Start()
    {
        m_gameStateService = ServiceLocator.GetService<GameStateService>();
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
        if (m_isUsedPopupOpen && Input.anyKeyDown)
        {
            m_gameStateService.MenuState();
            m_itemUsedPopUp.SetActive(false);
            m_isUsedPopupOpen = false;
        }
        else if (m_isAddedPopupOpen && Input.anyKeyDown)
        {
            m_gameStateService.NormalState();
            m_itemAddedPopUp.SetActive(false);
            m_isAddedPopupOpen = false;
        }
    }
    
    public void ItemAddedPopup(UsableItemToken item, int itemAmount)
    {
        m_gameStateService.DialogueState();
        m_itemAddedPopupText.text = "You found " + item.name + " x" + itemAmount;
        m_itemAddedPopUp.SetActive(true);
        m_isAddedPopupOpen = true;
    }
    public void ItemUsedPopup(UsableItemToken item)
    {
        // Temporary popup until items actually get used properly
        m_itemUsedPopupText.text = item.name + " used";
        m_itemUsedPopUp.SetActive(true);
        m_isUsedPopupOpen = true;
    }
}
