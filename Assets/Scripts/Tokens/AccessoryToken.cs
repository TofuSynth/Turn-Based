using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AccessoryToken : MonoBehaviour
{
    [CreateAssetMenu(menuName = "Tokens/Accessories Token")]
    public class WeaponToken : ScriptableObject
    {
        [SerializeField] List<Accessories> m_accessories;
        public List<Accessories> Accessories
        {
            get { return m_accessories; }
        }
    }
}

[Serializable]
    public class Accessories
    {
        [SerializeField] private string m_accesoryName;
        public string AccesoryName
        {
            get { return m_accesoryName; }
        }
        [SerializeField, Multiline(5)] private string m_accessoryDescription;
        public string AccessoryDescription
        {
            get { return m_accessoryDescription; }
        }
        [SerializeField] private int m_damage;
        public int Damage
        {
            get { return m_damage; }
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