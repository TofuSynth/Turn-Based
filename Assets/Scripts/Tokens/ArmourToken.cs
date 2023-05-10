using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourToken : MonoBehaviour
{
    [CreateAssetMenu(menuName = "Tokens/Armour Token")]
    public class WeaponToken : ScriptableObject
    {
        [SerializeField] List<Armour> m_armour;
        public List<Armour> Armour
        {
            get { return m_armour; }
        }
    }
}

[Serializable]
public class Armour
{
    [SerializeField] private string m_armourName;
    public string ArmourName
    {
        get { return m_armourName; }
    }
    [SerializeField, Multiline(5)] private string m_armourDescription;
    public string ArmourDescription
    {
        get { return m_armourDescription; }
    }
    [SerializeField] private int m_defense;
    public int ArmourDefense
    {
        get { return m_defense; }
    }
    [SerializeField] private int m_magicDefense;
    public int MagicDefense
    {
        get { return m_magicDefense; }
    }
    [SerializeField] private int m_magicIncreased;
    public int MagicIncreased {
        get{
            return m_magicIncreased;
        }
    }
    [SerializeField] private int m_wisdomIncreased;
    public int WisdomIncreased
    {
        get { return m_wisdomIncreased; }
    }
    [SerializeField] private int m_speedIncreased;
    public int SpeedIncreased
    {
        get { return m_speedIncreased; }
    }
    [SerializeField] private int m_luckIncreased;
    public int LuckIncreased
    {
        get { return m_luckIncreased; }
    }
    [SerializeField] private int m_buyPrice;
    public int BuyPrice
    {
        get { return m_buyPrice; }
    }
    [SerializeField] private int m_sellPrice;
    public int SellPrice
    {
        get { return m_sellPrice; }
    }
}
