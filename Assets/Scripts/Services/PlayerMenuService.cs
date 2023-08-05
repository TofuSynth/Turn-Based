using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Equipment;
using Tofu.TurnBased.Inventory;
using Tofu.TurnBased.SceneManagement;
using Tofu.TurnBased.Services;
using Tofu.TurnBased.Skills;
using Tofu.TurnBased.Stats;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMenuService : ServiceBase<PlayerMenuService>
{
    private GameObject m_playerMenu;
    private GameStateService m_gameState;
    private InventoryService m_inventoryService;
    private ControlsService m_controlsService;
    private EquipmentService m_equipmentService;
    private StatsService m_statsService;
    private SkillService m_skillService;
    private SaveService m_saveService;
    private LoadService m_loadService;
    [SerializeField] private SceneToken m_mainMenu;
    [SerializeField] private StatsListEntry statsListEntryTemplate;
    [SerializeField] private Transform statsContainer;
    
    void Start()
    {
        m_gameState = ServiceLocator.GetService<GameStateService>();
        m_controlsService = ServiceLocator.GetService<ControlsService>();
        m_inventoryService = ServiceLocator.GetService<InventoryService>();
        m_statsService = ServiceLocator.GetService<StatsService>();
        m_equipmentService = ServiceLocator.GetService<EquipmentService>();
        m_skillService = ServiceLocator.GetService<SkillService>();
        m_saveService = ServiceLocator.GetService<SaveService>();
        m_loadService = ServiceLocator.GetService<LoadService>();
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

    public void OpenSkillUI()
    {
        m_skillService.MakeSkillUIVisible();
    }

    public void OpenSaveUI()
    {
        m_saveService.MakeSaveUIVisible();
    }

    public void OpenLoadUI()
    {
        m_loadService.MakeLoadUIVisible();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(m_mainMenu.TargetSceneName);
    }
    
    void MenuNavigation()
    {
        if (m_controlsService.isCancelDown)
        {
            CloseMenu();
        }
    }
}
