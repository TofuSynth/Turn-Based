using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Inventory;
using Tofu.TurnBased.Services;
using Tofu.TurnBased.Stats;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMenuService : ServiceBase<PlayerMenuService>
{
    private GameObject m_playerMenu;
    private GameStateService m_gameState;
    private InventoryService m_inventoryService;
    private ControlsService m_controlsService;
    private EquipmentService m_equipmentService;
    private StatsService m_statsService;
    [SerializeField] private StatsListEntry statsListEntryTemplate;
    [SerializeField] private Transform statsContainer;
    
    void Start()
    {
        m_gameState = ServiceLocator.GetService<GameStateService>();
        m_controlsService = ServiceLocator.GetService<ControlsService>();
        m_inventoryService = ServiceLocator.GetService<InventoryService>();
        m_statsService = ServiceLocator.GetService<StatsService>();
        m_equipmentService = ServiceLocator.GetService<EquipmentService>();
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
        Cursor.visible = true;
        this.gameObject.SetActive(true);
        FillCharacterStats();
    }

    void CloseMenu()
    {
        m_gameState.NormalState();
        Cursor.visible = false;
        HideMenuUI();
    }

    void HideMenuUI()
    {
        EventSystem.current.SetSelectedGameObject(null);
        this.gameObject.SetActive(false);
    }
    
    public void FillCharacterStats()
    {
        m_statsService.PopulateCharacterStats(statsListEntryTemplate , statsContainer);
    }
    
    public void OpenInventoryUI()
    {
        m_inventoryService.FillInventoryUI();
        m_inventoryService.MakeInventoryUIVisible();
        m_inventoryService.FillCharacterStats();
        HideMenuUI();
    }

    public void OpenEquipmentUI()
    {
        m_equipmentService.MakeEquipmentUIVisible();
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
