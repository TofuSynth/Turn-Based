using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tokens/Weapon Token")]
public class WeaponToken : ScriptableObject
{
    [SerializeField] List<Weapons> m_weapons;
    public List<Weapons> Weapons
    {
        get { return m_weapons; }
    }
}

[Serializable]
public class Weapons
{
    [SerializeField] private string m_weaponName;
    public string WeaponName
    {
        get { return m_weaponName; }
    }
    [SerializeField, Multiline(5)] private string m_weaponDescription;
    public string WeaponDescription
    {
        get { return m_weaponDescription; }
    }
    [SerializeField] private int m_damage;
    public int Damage
    {
        get { return m_damage; }
    }
    [SerializeField] private int m_magicIncreased;
    public int MagicIncreased {
        get{
            return m_magicIncreased;
        }
    }
    [SerializeField] private string m_statIncreased;
    public string StatIncreased
    {
        get { return m_statIncreased; }
    }
    [SerializeField] private int m_statAmount;
    public int StatAmount
    {
        get { return m_statAmount; }
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