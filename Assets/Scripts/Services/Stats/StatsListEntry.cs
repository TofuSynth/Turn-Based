using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tofu.TurnBased.Services;
using UnityEngine;


namespace Tofu.TurnBased.Stats
{
    public class StatsListEntry : MonoBehaviour
    {
        private StatsService m_stats;
        [SerializeField] private TMP_Text m_name;
        [SerializeField] private TMP_Text m_hp;
        [SerializeField] private TMP_Text m_mp;
        [SerializeField] private PartyMembertokens m_partyMember;
        public PartyMembertokens PartyMemberToken
        {
            get { return m_partyMember; }
        }

        private void Start()
        {
            m_stats = ServiceLocator.GetService<StatsService>();
        }

        public void PopulateHPMP(int currentHP, int totalHP, int currentMP, int totalMP, PartyMembertokens partyMember)
        {
            m_name.text = partyMember.name;
            m_hp.text = (currentHP + "/" + totalHP);
            m_mp.text = (currentMP + "/" + totalMP);
            m_partyMember = partyMember;
        }
    }
}