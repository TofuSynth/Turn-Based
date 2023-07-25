using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tofu.TurnBased.Services;
using UnityEngine;

namespace Tofu.TurnBased.Stats
{
    public class StatsService : ServiceBase<StatsService>
    {
        [SerializeField] private PartyMembertokens m_player;
        PartyMembertokens Player
        {
            get { return m_player; }
        }
        private Dictionary<PartyMembertokens, CurrentStats> m_partyMembers = new Dictionary<PartyMembertokens, CurrentStats>();
        public Dictionary<PartyMembertokens, CurrentStats> PartyMembers
        {
            get { return m_partyMembers; }
        }

        private void Start()
        {
            m_partyMembers.TryAdd(Player, new CurrentStats());
            AssignStats(Player);
        }

        public void AssignStats(PartyMembertokens partyMember)
        {
            if (m_partyMembers[partyMember].firstStatAssignment)
            {
                m_partyMembers[partyMember].level = partyMember.StartingLevel;
            }

            m_partyMembers[partyMember].totalHP =
                partyMember.BaseHP + (m_partyMembers[partyMember].level * partyMember.HPGrowth);
            m_partyMembers[partyMember].totalMP =
                partyMember.BaseMP + (m_partyMembers[partyMember].level * partyMember.MPGrowth);

            if (m_partyMembers[partyMember].firstStatAssignment)
            {
                m_partyMembers[partyMember].currentHP = m_partyMembers[partyMember].totalHP;
                m_partyMembers[partyMember].currentMP = m_partyMembers[partyMember].totalMP;
                m_partyMembers[partyMember].firstStatAssignment = false;
            }

            m_partyMembers[partyMember].currentDamage =
                partyMember.BaseDamage + (m_partyMembers[partyMember].level * partyMember.DamageGrowth);
            m_partyMembers[partyMember].currentMagic =
                partyMember.BaseMagic + (m_partyMembers[partyMember].level * partyMember.MagicGrowth);
            m_partyMembers[partyMember].currentDefense =
                partyMember.BaseDefense + (m_partyMembers[partyMember].level * partyMember.DefenseGrowth);
            m_partyMembers[partyMember].currentMagicDefense =
                partyMember.BaseMagicDefense + (m_partyMembers[partyMember].level * partyMember.MagicDefenseGrowth);
            m_partyMembers[partyMember].currentSpeed =
                partyMember.BaseSpeed + (m_partyMembers[partyMember].level + partyMember.SpeedGrowth);
            m_partyMembers[partyMember].currentLuck =
                partyMember.BaseLuck + (m_partyMembers[partyMember].level * partyMember.LuckGrowth);
        }

        public void PopulateCharacterStats(StatsListEntry template, Transform characterStats)
        {
            for (int i = 0; i < characterStats.childCount; i++)
            {
                Destroy(characterStats.GetChild(i).gameObject);
            } 
            foreach (var partyMember in m_partyMembers) 
            {
                StatsListEntry entry = Instantiate(template, characterStats);
                entry.PopulateHPMP(m_partyMembers[partyMember.Key].currentHP, m_partyMembers[partyMember.Key].totalHP,
                    m_partyMembers[partyMember.Key].currentMP, m_partyMembers[partyMember.Key].totalMP, partyMember.Key);
                entry.gameObject.SetActive(true); 
            }
        }
        
        public void DisplayPortrait(Sprite portrait, PartyMembertokens partyMember)
        {
            portrait = partyMember.CharacterPortrait;
        }

    }

    public class CurrentStats
    {
        public bool firstStatAssignment = true;
        public int level = 0;
        public int currentHP = 0;
        public int totalHP = 0;
        public int currentMP = 0;
        public int totalMP = 0;
        public int currentDamage = 0;
        public int currentMagic = 0;
        public int currentDefense = 0;
        public int currentMagicDefense = 0;
        public int currentSpeed = 0;
        public int currentLuck = 0;
    }
}