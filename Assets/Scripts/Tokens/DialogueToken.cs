using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Tokens/Dialogue Token")]
public class DialogueToken : ScriptableObject
{
    [SerializeField, ReadOnly] private int m_dialogueProgress;
    public int DialogueProgress
    {
        get { return m_dialogueProgress; }
    }
    [SerializeField] private List<Dialogue> m_dialogue = new List<Dialogue>();
    public List<Dialogue> Dialogue
    {
        get { return m_dialogue; }
    }
    private void OnValidate()
    {
        //Loop through all Dialogue entries and assign their keys
        for (int i = 0;i < Dialogue.Count;i++)
        {
            Dialogue[i].SetSaveKey("state",this.GetInstanceID(),"dialogue" + i);
            
            //Loop over each Dialogue's child Conversations and assign their keys
            for (int j = 0; j < Dialogue[i].Conversations.Count; j++)
            {
                Dialogue[i].Conversations[j].SetSaveKey("state",this.GetInstanceID(),"dialogue" + i + ".conversation" + j);
            }
        }
    }
}
[Serializable]
public class Dialogue : BoolStateObject
{
    [SerializeField] private List<DialogueConversations> m_conversations = new List<DialogueConversations>();
    public List<DialogueConversations> Conversations
    {
        get { return m_conversations; }
    }
    public int DialogueTotalStages
    {
        get { return Conversations.Count; }
    }
}

[Serializable]
public class DialogueConversations : BoolStateObject
{
    [SerializeField, Multiline(5)] private string m_dialogue;
    public string Description
    {
        get { return m_dialogue; }
    }
    [SerializeField] private List<DialogueConditions> m_conditions;
    public List<DialogueConditions> Steps
    {
        get { return m_conditions; }
    }
    
}
[Serializable]
public class DialogueConditions
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