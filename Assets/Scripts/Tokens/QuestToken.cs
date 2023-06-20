using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tofu.TurnBased.SceneManagement;
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
    private void OnValidate()
    {
        for (int i = 0;i < Steps.Count;i++)
        {
            Steps[i].SetSaveKey("state",this.GetInstanceID(),"steps" + i);
        }
    }
}


[Serializable]
public class QuestStep : BoolStateObject
{
    [SerializeField, Multiline(5)] private string m_description;
    public string Description
    {
        get { return m_description; }
    }
    [SerializeField] private List<QuestConditions> m_conditions = new List<QuestConditions>();
    public List<QuestConditions> Steps
    {
        get { return m_conditions; }
    }
    
}

[Serializable]
public class QuestConditions
{
    [SerializeField] private List<QuestToken> m_questCompleted;
    public List<QuestToken> QuestCompleted
    {
        get { return m_questCompleted; }
    }
    [SerializeField] private List<DialogueToken> m_speakTo;
    public List<DialogueToken> SpeakTo
    {
        get { return m_speakTo; }
    }
    [SerializeField] private List<SceneToken> m_areaVisited;
    public List<SceneToken> AreaVisited
    {
        get { return m_areaVisited; }
    }
    [SerializeField] private EnemyDictionary m_requiredKills;
    public Dictionary<EnemyToken, int> RequiredKills
    {
        get { return m_requiredKills; }
    }
    [SerializeField] private UsableItemDictionary m_itemRequired;
    public Dictionary<UsableItemToken, int> ItemRequired
    {
        get { return m_itemRequired; }
    }
}

[Serializable] public class EnemyDictionary : UnitySerializedDictionary<EnemyToken, int> { }
[Serializable] public class UsableItemDictionary : UnitySerializedDictionary<UsableItemToken, int> { }

