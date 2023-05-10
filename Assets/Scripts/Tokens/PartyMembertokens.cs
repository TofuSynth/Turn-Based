using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tokens/Party Member Token")]
public class PartyMembertokens : ScriptableObject
{
    [SerializeField] private string m_partyMemberName;
    public string PartyMemberName
    {
        get { return m_partyMemberName; }
    }
    [SerializeField] private int m_baseHP;
    public int BaseHP
    {
        get { return m_baseHP; }
    }
    [SerializeField] private int m_baseMP;
    public int BaseMP
    {
        get { return m_baseMP; }
    }
    [SerializeField] private int m_baseDamage;
    public int BaseDamage
    {
        get { return m_baseDamage; }
    }
    [SerializeField] private int m_baseMagic;
    public int BaseMagic
    {
        get { return m_baseMagic; }
    }
    [SerializeField] private int m_baseWisdom;
    public int BaseWisdom
    {
        get { return m_baseWisdom; }
    }
    [SerializeField] private int m_baseDefense;
    public int BaseDefense
    {
        get { return m_baseDefense; }
    }
    [SerializeField] private int m_baseMagicDefense;
    public int BaseMagicDefense
    {
        get { return m_baseMagicDefense; }
    }
    [SerializeField] private int m_baseSpeed;
    public int BaseSpeed
    {
        get { return m_baseSpeed; }
    }
    [SerializeField] private int m_baseLuck;
    public int BaseLuck
    {
        get { return m_baseLuck; }
    }
}

