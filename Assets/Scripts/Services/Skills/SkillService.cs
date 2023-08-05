using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tofu.TurnBased.Skills
{
    public class SkillService : ServiceBase<SkillService>
    {
        void Start()
        {
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
    }
}