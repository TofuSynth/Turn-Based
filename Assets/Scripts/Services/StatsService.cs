using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;

public class StatsService : ServiceBase<StatsService>
{
    [SerializeField] private PartyMembertokens m_player;
    PartyMembertokens Player
    {
        get { return m_player; }
    }
    private Dictionary<PartyMembertokens, CurrentStats> m_partyMembers;
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
            m_partyMembers[partyMember].firstStatAssignment = false;
        }
        m_partyMembers[partyMember].currentHP =
            partyMember.BaseHP + (m_partyMembers[partyMember].level * partyMember.HPGrowth);
        m_partyMembers[partyMember].currentMP =
            partyMember.BaseMP + (m_partyMembers[partyMember].level * partyMember.MPGrowth);
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
    
}

public class CurrentStats
{
    public bool firstStatAssignment = true;
    public int level = 0;
    public int currentHP = 0;
    public int currentMP = 0;
    public int currentDamage = 0;
    public int currentMagic = 0;
    public int currentDefense = 0;
    public int currentMagicDefense = 0;
    public int currentSpeed = 0;
    public int currentLuck = 0;
}
