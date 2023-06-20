using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.SceneManagement;
using UnityEngine;
using Tofu.TurnBased.Services;
using UnityEngine.SceneManagement;

namespace Tofu.TurnBased.Quests
{
    public class QuestManagementService : ServiceBase<QuestManagementService>
    {
        private Dictionary<QuestToken, QuestConditionals> m_activeQuests;
        private List<QuestToken> m_completedQuests;

        public void AcquireNewQuest(QuestToken newQuest)
        {
            if (!m_activeQuests.ContainsKey(newQuest))
            {
                m_activeQuests.Add(newQuest, new QuestConditionals());
                SetConditionals(newQuest);
            }
        }

        public void CompleteQuest(QuestToken completedQuest)
        {
            m_activeQuests.Remove(completedQuest);
            m_completedQuests.Add(completedQuest);
        }

        public void SetConditionals(QuestToken newQuest)
        {
            QuestConditionals newCondtionals = m_activeQuests[newQuest];
            int currentStep = newCondtionals.currentStep;
            QuestStep currentQuestStep = newQuest.Steps[currentStep];
            
            foreach (QuestToken quest in currentQuestStep.Conditions.QuestCompleted)
            { 
                newCondtionals.questCompleted.Add(quest, 1);
            }
            foreach (DialogueToken NPC in currentQuestStep.Conditions.SpeakTo)
            { 
                newCondtionals.spokenTo.Add(NPC, 1);
            }
            foreach (SceneToken area in currentQuestStep.Conditions.AreaVisited)
            { 
                newCondtionals.areaVisited.Add(area, 1);
            }
            foreach (EnemyToken enemies in currentQuestStep.Conditions.RequiredKills.Keys)
            { 
                newCondtionals.enemyDefeated.Add(enemies, currentQuestStep.Conditions.RequiredKills[enemies]);
            }
            foreach (UsableItemToken item in currentQuestStep.Conditions.ItemRequired.Keys)
            {
                newCondtionals.itemGathered.Add(item, currentQuestStep.Conditions.ItemRequired[item]);
            }
        }
    }

    public class QuestConditionals
    {
        public int currentStep = 0;
        public Dictionary<QuestToken, int> questCompleted;
        public Dictionary<DialogueToken, int> spokenTo;
        public Dictionary<SceneToken, int> areaVisited;
        public Dictionary<EnemyToken, int> enemyDefeated;
        public Dictionary<UsableItemToken, int> itemGathered;
        
    }
}