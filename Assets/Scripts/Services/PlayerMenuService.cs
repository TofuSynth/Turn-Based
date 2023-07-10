using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Inventory;
using Tofu.TurnBased.Services;
using UnityEngine;

public class PlayerMenuService : ServiceBase<PlayerMenuService>
{
    private GameObject m_playerMenu;
    private GameStateService m_gameState;
    private InventoryService m_inventoryService;
    private ControlsService m_controlsService;
    
    void Start()
    {
        m_gameState = ServiceLocator.GetService<GameStateService>();
        m_controlsService = ServiceLocator.GetService<ControlsService>();
        m_inventoryService = ServiceLocator.GetService<InventoryService>();
        HideMenuUI();
        
    }

    private void Update()
    {
        MenuNavigation();
    }

    public void MakeMenuUIVisible()
    {
        if (m_gameState.GetState() != GameStateService.GameState.Menu)
        {
            m_gameState.MenuState();
        }
        this.gameObject.SetActive(true);
    }

    void CloseMenu()
    {
        m_gameState.NormalState();
        HideMenuUI();
    }

    void HideMenuUI()
    {
        this.gameObject.SetActive(false);
    }
    
    public void OpenInventoryUI()
    {
        m_inventoryService.FillInventoryUI();
        m_inventoryService.MakeInventoryUIVisible();
        HideMenuUI();
    }

    void MenuNavigation()
    {
        if (m_controlsService.isCancelDown)
        {
            CloseMenu();
        }
    }
}
