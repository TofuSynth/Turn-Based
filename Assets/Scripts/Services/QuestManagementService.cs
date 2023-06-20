using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tofu.TurnBased.Services;

namespace Tofu.TurnBased.Quests
{
    public class QuestManagementService : ServiceBase<QuestManagementService>
    {
        private List<QuestToken> m_activeQuests;
        private List<QuestToken> m_completedQuests;
        public Dictionary<QuestToken, QuestConditionals> TrackingQuest;

        public void AcquireNewQuest(QuestToken newQuest)
        {
            if (!m_activeQuests.Contains(newQuest))
            {
                m_activeQuests.Add(newQuest);
                QuestConditionals questConditionals = new QuestConditionals(newQuest);
            }
        }

        public void CompleteQuest(QuestToken completedQuest)
        {
            m_activeQuests.Remove(completedQuest);
            m_completedQuests.Add(completedQuest);
        }
        
    }

    public class QuestConditionals
    {
        private QuestToken m_quest;
        private int m_currentStep = 0;
        public int CurrentStep
        {
            get { return m_currentStep; }
        }
        public QuestConditionals(QuestToken quest)
        {
            m_quest = quest;
        }
        public void QuestTracking(QuestToken Quest)
        {
            
        }
    }
}