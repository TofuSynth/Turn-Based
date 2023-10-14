using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;

public class SaveService : ServiceBase<SaveService>
{
    private ControlsService m_controlsService;
    private PlayerMenuService m_playerMenuService;
    void Start()
    {
        m_controlsService = ServiceLocator.GetService<ControlsService>();
        m_playerMenuService = ServiceLocator.GetService<PlayerMenuService>();
        HideSaveUI();
    }

    void HideSaveUI()
    {
        this.gameObject.SetActive(false);
    }

    public void MakeSaveUIVisible()
    {
        this.gameObject.SetActive(true);
    }
    
    private void Update()
    {
        SaveMenuNavigation();
    }

    void SaveMenuNavigation()
    {
        if (m_controlsService.isCancelDown)
        {
            m_playerMenuService.MakeMenuUIVisible();
            HideSaveUI();
        }
    }
}
