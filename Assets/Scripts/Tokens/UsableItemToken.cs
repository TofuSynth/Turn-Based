using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tokens/Usable Item Token")]
public class UsableItemToken : ScriptableObject
{

    [SerializeField] private string m_itemName;
    public string ItemName
    {
        get { return m_itemName; }
    }
    [SerializeField] private string m_itemDescription;
    public string ItemDescription
    {
        get { return m_itemDescription; }
    }
    [SerializeField] private bool m_revivesDead;
    public bool RevivesDead
    {
        get { return m_revivesDead; }
        
    }
    [SerializeField] private int m_restoreHPValue;
    public int restoreHPValue
    {
        get { return m_restoreHPValue; }
    } 
    [SerializeField] private int m_restoreMPValue;
    public int RestoreMPValue
    {
        get { return m_restoreMPValue; }
    }
    [SerializeField] private bool m_curesPoison;
    public bool CuresPoison
    {
        get { return m_curesPoison; }
    }
    [SerializeField] private bool m_curesBlind;
    public bool CuresBlind
    {
        get { return m_curesBlind; }
    }
    [SerializeField] private bool m_curesSilence;
    public bool CuresSilence
    {
        get { return m_curesSilence; }
    }
}
