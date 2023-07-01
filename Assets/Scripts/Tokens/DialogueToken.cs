using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
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
        [SerializeField] private List<QuestDialogue> m_questDialogue = new List<QuestDialogue>();
        public List<QuestDialogue> QuestDialogue
        {
            get { return m_questDialogue; }
        }
        private void OnValidate() {
            for (int i = 0;i < m_dialogue.Count;i++) {
                m_dialogue[i].SetStepIndex(i);
            }
        }
    }

    [Serializable]
    public class Dialogue
    {
        [ReadOnly, SerializeField] private int m_dialogueStep ;
        [SerializeField] private bool m_questRequiredAfterToAdvance;
        public bool questRequiredAfterToAdvance
        {
            get { return m_questRequiredAfterToAdvance; }
        }
        [SerializeField] List<Conversation> m_conversation;
        public List<Conversation> Conversation
        {
            get { return m_conversation; }
        }
        public void SetStepIndex(int index) {
            this.m_dialogueStep = index;
        }
    }
    
    [Serializable]
    public class QuestDialogue
    {
        [SerializeField] private int m_goToDialogueStepUponCompletion;
        public int goToDialogueStepUponCompletion
        {
            get { return m_goToDialogueStepUponCompletion; }
        }
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
        [SerializeField] private string m_name;
        public string name
        {
            get { return m_name; }
        }
        
        [SerializeField, Multiline(5)] private string m_script;
        public string script
        {
            get { return m_script; }
        }
    }

    [Serializable]
    public class DialogueConditions
    {
        [Space][InfoBox("Only pick one of the Quest options below")]
        [SerializeField] private QuestToken m_questCompleted;
        public QuestToken QuestCompleted
        {
            get { return m_questCompleted; }
        }

        [SerializeField] private QuestToken m_activeQuest;
        public QuestToken activeQuest
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