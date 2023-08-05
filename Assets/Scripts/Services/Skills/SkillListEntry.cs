using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tofu.TurnBased.Skills
{
    public class SkillListEntry : MonoBehaviour
    {
        [SerializeField] private Button m_skillButton;
        [SerializeField] private TMP_Text m_skillName;
        [SerializeField] private TMP_Text m_mpCost;
        /*[SerializeField] private SkillToken m_skillToken;
        public SkillToken SkillToken
        {
            get { return m_skillToken; }
        }
        
        
        public void PopulateEntry(string skillName, int mpCost, SkillToken token)
        {
            m_skillName.text = skillName;
            m_mpCost.text = mpCost.ToString();
            m_skillToken = token;
        }
        */
    }
}