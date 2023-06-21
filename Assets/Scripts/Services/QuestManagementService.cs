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
                UpdateConditionals(newQuest);
            }
        }

        public void UpdateConditionals(QuestToken quest)
        {
            QuestConditionals newCondtionals = m_activeQuests[quest];
            int currentStep = newCondtionals.currentStep;
            if (currentStep > quest.Steps.Count)
            {
                CompleteQuest(quest);
            }
            else
            {
                QuestStep currentQuestStep = quest.Steps[currentStep];

                foreach (QuestToken completedQuest in currentQuestStep.Conditions.QuestCompleted)
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

        public void CheckIfQuestStepAdvances(QuestToken quest)
        {
            if (!CheckIfQuestConditionsMet(quest))
            {
                m_activeQuests[quest].currentStep++;
                UpdateConditionals(quest);
            }
        }

        public bool CheckIfQuestConditionsMet(QuestToken quest)
        {
            foreach (KeyValuePair<QuestToken, int> questCompleted in m_activeQuests[quest].questCompleted)
            {
                if (questCompleted.Value != 0)
                {
                    return false;
                }   
            }
            foreach (KeyValuePair<DialogueToken, int> spokenTo in m_activeQuests[quest].spokenTo)
            {
                if (spokenTo.Value != 0)
                {
                    return false;
                } 
            }
            foreach (KeyValuePair<SceneToken, int> areaVisited in m_activeQuests[quest].areaVisited)
            {
                if (areaVisited.Value != 0)
                {
                    return false;
                } 
            }
            foreach (KeyValuePair<EnemyToken, int> enemyDefeated in m_activeQuests[quest].enemyDefeated)
            {
                if (enemyDefeated.Value != 0)
                {
                    return false;
                } 
            }
            foreach (KeyValuePair<UsableItemToken, int> itemGathered in m_activeQuests[quest].itemGathered)
            {
                if (itemGathered.Value != 0)
                {
                    return false;
                } 
            }
            return true;
        }
        
        public void CompleteQuest(QuestToken completedQuest)
        {
            m_activeQuests.Remove(completedQuest);
            m_completedQuests.Add(completedQuest);
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