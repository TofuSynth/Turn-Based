using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Tokens/Party Member Token")]
public class PartyMembertokens : ScriptableObject
{
    [SerializeField] private string m_partyMemberName;
    public string PartyMemberName
    {
        get { return m_partyMemberName; }
    }
    [SerializeField] private Sprite m_characterPortrait;
    public Sprite CharacterPortrait
    {
        get { return m_characterPortrait; }
    }
    [SerializeField] private int m_startingLevel;
    public int StartingLevel
    {
        get { return m_startingLevel; }

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
    [Space][InfoBox("This value will be given per level")]
    [SerializeField] private int m_hpGrowth;
    public int HPGrowth
    {
        get { return m_hpGrowth; }
    }
    [SerializeField] private int m_mpGrowth;
    public int MPGrowth
    {
        get { return m_mpGrowth; }
    }
    [SerializeField] private int m_damageGrowth;
    public int DamageGrowth
    {
        get { return m_damageGrowth; }
    }
    [SerializeField] private int m_magicGrowth;
    public int MagicGrowth
    {
        get { return m_magicGrowth; }
    }
    [SerializeField] private int m_defenseGrowth;
    public int DefenseGrowth
    {
        get { return m_defenseGrowth; }
    }
    [SerializeField] private int m_magicDefenseGrowth;
    public int MagicDefenseGrowth
    {
        get { return m_magicDefenseGrowth; }
    }
    [SerializeField] private int m_speedGrowth;
    public int SpeedGrowth
    {
        get { return m_speedGrowth; }
    }
    [SerializeField] private int m_luckGrowth;
    public int LuckGrowth
    {
        get { return m_luckGrowth; }
    }
    [SerializeField] private WeaponToken m_weaponType;
    public WeaponToken WeaponType
    {
        get { return m_weaponType; }
    }
    [SerializeField] private ArmourToken m_armourType;
    public ArmourToken ArmourType
    {
        get { return m_armourType; }
    }
}

