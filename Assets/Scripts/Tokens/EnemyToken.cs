using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Tokens/Enemy Token")]
public class EnemyToken : ScriptableObject
{
        [SerializeField] private string m_enemyName;
        public string EnemyName
        {
            get { return m_enemyName; }
        }
        [SerializeField] private int m_hP;
        public int HP
        {
            get { return m_hP; }
        }
        [SerializeField] private int m_mP;
        public int MP
        {
            get { return m_mP; }
        }
        [SerializeField] private int m_damage;
        public int Damage
        {
            get { return m_damage; }
        }
        [SerializeField] private int m_baseMagic;
        public int Magic
        {
            get { return m_baseMagic; }
        }
        [SerializeField] private int m_wisdom;
        public int Wisdom
        {
            get { return m_wisdom; }
        }
        [SerializeField] private int m_defense;
        public int Defense
        {
            get { return m_defense; }
        }
        [SerializeField] private int m_magicDefense;
        public int MagicDefense
        {
            get { return m_magicDefense; }
        }
        [SerializeField] private int m_speed;
        public int Speed
        {
            get { return m_speed; }
        }
        [SerializeField] private int m_luck;
        public int Luck
        {
            get { return m_luck; }
        }
}


