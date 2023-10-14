using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;

namespace Tofu.TurnBased.Skills
{
    public class SkillService : ServiceBase<SkillService>
    {
        private ControlsService m_controlsService;
        private PlayerMenuService m_playerMenuService;
        void Start()
        {
            m_controlsService = ServiceLocator.GetService<ControlsService>();
            m_playerMenuService = ServiceLocator.GetService<PlayerMenuService>();
            HideSkillUI();
        }

        void HideSkillUI()
        {
            this.gameObject.SetActive(false);
        }

        public void MakeSkillUIVisible()
        {
            this.gameObject.SetActive(true);
        }
        
        private void Update()
        {
            SkillMenuNavigation();
        }

        void SkillMenuNavigation()
        {
            if (m_controlsService.isCancelDown)
            {
                m_playerMenuService.MakeMenuUIVisible();
                HideSkillUI();
            }
        }
        
    }
}