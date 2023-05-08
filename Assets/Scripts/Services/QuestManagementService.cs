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

        public void AcquireNewQuest(QuestToken newQuest)
        {
            if (!m_activeQuests.Contains(newQuest))
            {
                m_activeQuests.Add(newQuest);
            }
        }

        public void CompleteQuest(QuestToken completedQuest)
        {
            m_activeQuests.Remove(completedQuest);
            m_completedQuests.Add(completedQuest);
        }
    }
}