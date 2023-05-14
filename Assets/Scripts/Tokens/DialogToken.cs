using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Tokens/Dialog Token")]
public class DialogToken : ScriptableObject
{
    [SerializeField, ReadOnly] private int m_dialogProgress;
    public int QuestProgress
    {
        get { return m_dialogProgress; }
    }
    [SerializeField] private List<Dialog> m_dialog;
    public List<Dialog> Dialog
    {
        get { return m_dialog; }
    }
}
[Serializable]
public class Dialog
{
    [SerializeField] private List<DialogConversations> m_conversations;
    public List<DialogConversations> Conversations
    {
        get { return m_conversations; }
    }
    public int DialogTotalStages
    {
        get { return Conversations.Count; }
    }
}

[Serializable]
public class DialogConversations
{
    [SerializeField, Multiline(5)] private string m_dialog;
    public string Description
    {
        get { return m_dialog; }
    }
    [SerializeField] private List<DialogConditions> m_conditions;
    public List<DialogConditions> Steps
    {
        get { return m_conditions; }
    }
    
}
[Serializable]
public class DialogConditions
{
    [SerializeField, ReadOnly] private bool m_isConditionMet;
    public bool IsConditionMet
    {
        get { return m_isConditionMet; }
    }
    [SerializeField] private QuestToken m_questCompleted;
    public QuestToken QuestCompleted
    {
        get { return m_questCompleted; }
    }
    [SerializeField] private string m_itemRequired;
    public string ItemRequired
    {
        get { return m_itemRequired; }
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