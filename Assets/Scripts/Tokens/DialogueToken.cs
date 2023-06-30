using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tofu.TurnBased.Dialogue
{
    [CreateAssetMenu(menuName = "Tokens/Dialogue Token")]
    public class DialogueToken : ScriptableObject
    {
        [SerializeField] private List<Dialogue> m_dialogue = new List<Dialogue>();

        public List<Dialogue> Dialogue
        {
            get { return m_dialogue; }
        }
    }

    [Serializable]
    public class Dialogue
    {
        [SerializeField] DialogueConditions m_conditions;
        public DialogueConditions Conditions
        {
            get {
                return m_conditions;
            }
        }
        
        [SerializeField] List<Conversation> m_conversation;
        public List<Conversation> Conversation
        {
            get { return m_conversation; }
        }
    }

    [Serializable]
    public class Conversation
    {
        [SerializeField] string m_name;
        [SerializeField, Multiline(5)] private string m_script;
    }

    [Serializable]
    public class DialogueConditions
    {
        [SerializeField] private QuestToken m_questCompleted;
        public QuestToken QuestCompleted
        {
            get { return m_questCompleted; }
        }

        [SerializeField] private QuestToken m_activeQuest;
        public QuestToken activeQuestToken
        {
            get { return m_activeQuest; }
        }

        [SerializeField] private int m_requiredStepOfActiveQuest;
        public int requireStepOfActiveQuest
        {
            get { return m_requiredStepOfActiveQuest; }
        }

        [SerializeField] private bool m_isThisConversationAConditional;
        public bool isThisConversationAConditional
        {
            get { return m_isThisConversationAConditional; }
        }
        
        [SerializeField] private bool m_doesThisConversationCompleteTheQuest;
        public bool doesThisConversationCompleteTheQuest
        {
            get { return m_doesThisConversationCompleteTheQuest; }
        }

    }
}