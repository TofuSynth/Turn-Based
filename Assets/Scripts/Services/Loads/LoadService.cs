using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;

public class LoadService : ServiceBase<LoadService>
{
    private ControlsService m_controlsService;
    private PlayerMenuService m_playerMenuService;
    void Start()
    {
        m_controlsService = ServiceLocator.GetService<ControlsService>();
        m_playerMenuService = ServiceLocator.GetService<PlayerMenuService>();
        HideLoadUI();
    }

    void HideLoadUI()
    {
        this.gameObject.SetActive(false);
    }

    public void MakeLoadUIVisible()
    {
        this.gameObject.SetActive(true);
    }
    
    private void Update()
    {
        LoadMenuNavigation();
    }

    void LoadMenuNavigation()
    {
        if (m_controlsService.isCancelDown)
        {
            m_playerMenuService.MakeMenuUIVisible();
            HideLoadUI();
        }
    }
}
