using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(menuName = "Tokens/Quest Token")]
public class QuestToken : ScriptableObject
{
    [SerializeField] private string m_questName;
    public string QuestName
    {
        get { return m_questName; }
    }
    [SerializeField, ReadOnly] private int m_questProgress;
    public int QuestProgress
    {
        get { return m_questProgress; }
    }
    [SerializeField] private List<QuestStep> m_steps;
    public List<QuestStep> Steps
    {
        get { return m_steps; }
    }
    public int QuestTotalSteps
    {
        get { return Steps.Count; }
    }
    [SerializeField, Multiline(5)] private string m_completedQuestDescription;
    public string CompletedQuestDescription
    {
        get { return m_completedQuestDescription; }
    }
}


[Serializable]
public class QuestStep
{
    [SerializeField, Multiline(5)] private string m_description;
    public string Description
    {
        get { return m_description; }
    }
    [SerializeField] private List<QuestConditions> m_conditions;
    public List<QuestConditions> Steps
    {
        get { return m_conditions; }
    }
    
}

[Serializable]
public class QuestConditions
{
    [SerializeField, ReadOnly] private bool m_isConditionMet;
    public bool IsConditionMet
    {
        get { return m_isConditionMet; }
    }
    [SerializeField] private int m_requiredKills;
    public int RequiredKills
    {
        get { return m_requiredKills; }
    }
    [SerializeField] private string m_enemyRequired;
    public string EnemyRequired
    {
        get { return m_enemyRequired;} 
    }
    [SerializeField] private string m_itemRequired;
    public string ItemRequired
    {
        get { return m_itemRequired; }
    }
    [SerializeField] private int m_itemAmountRequired;
    public int ItemAmountRequired
    {
        get { return m_itemAmountRequired; }
    }
    [SerializeField] private string m_speakTo;
    public string SpeakTo
    {
        get { return m_speakTo; }
    }
    [SerializeField] private string m_areaVisited;
    public string AreaVisited
    {
        get { return m_areaVisited; }
    }
}