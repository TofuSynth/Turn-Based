using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace Tofu.TurnBased.Equipment
{
    public class EquipmentService : ServiceBase<EquipmentService>
    {
        private ControlsService m_controlsService;
        private PlayerMenuService m_playerMenuService;
        private void Start()
        {
            m_controlsService = ServiceLocator.GetService<ControlsService>();
            m_playerMenuService = ServiceLocator.GetService<PlayerMenuService>();
            HideEquipmentUI();
        }

        private void HideEquipmentUI()
        {
            this.gameObject.SetActive(false);
        }

        public void MakeEquipmentUIVisible()
        {
            this.gameObject.SetActive(true);
        }

        private void Update()
        {
            EquipmentMenuNavigation();
        }

        void EquipmentMenuNavigation()
        {
            if (m_controlsService.isCancelDown)
            {
                m_playerMenuService.MakeMenuUIVisible();
                HideEquipmentUI();
            }
        }
    }
}