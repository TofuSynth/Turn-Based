using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestConditionalCheck : MonoBehaviour
{
    [SerializeField] private QuestToken m_quest;
    public QuestToken Quest
    {
        get { return m_quest; }
    }
    [SerializeField] private int m_requiredStep;
    public int RequiredStep
    {
        get { return m_requiredStep; }
    }
    
    public void CheckQuestProgress()
    {
        //if (Quest.QuestProgress == RequiredStep)
        {
            
        }
    }
}
