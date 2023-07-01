using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Dialogue;
using Tofu.TurnBased.Inventory;
using Tofu.TurnBased.SceneManagement;
using UnityEngine;
using Tofu.TurnBased.Services;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Tofu.TurnBased.Quests
{
    public class QuestManagementService : ServiceBase<QuestManagementService>
    {
        private Dictionary<QuestToken, QuestConditionals> m_activeQuests;
        public Dictionary<QuestToken, QuestConditionals> activeQuests
        {
            get { return m_activeQuests; }
        }
        
        private List<QuestToken> m_completedQuests;
        public List<QuestToken> completedQuests
        {
            get { return m_completedQuests; }
        }
        
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
            int x = 0;
            if (m_activeQuests[quest].currentStep != 0)
            {
                x = m_activeQuests[quest].currentStep;
            }
            QuestConditionals newConditionals = m_activeQuests[quest] = new QuestConditionals();
            int currentStep = x;
            if (currentStep > quest.Steps.Count)
            {
                CompleteQuest(quest);
            }
            else
            {
                QuestStep currentQuestStep = quest.Steps[currentStep];

                foreach (QuestToken completedQuest in currentQuestStep.Conditions.QuestCompleted)
                {
                    newConditionals.questCompleted.Add(quest, 1);
                }

                foreach (DialogueToken NPC in currentQuestStep.Conditions.SpeakTo)
                {
                    newConditionals.spokenTo.Add(NPC, 1);
                }

                foreach (SceneToken area in currentQuestStep.Conditions.AreaVisited)
                {
                    newConditionals.areaVisited.Add(area, 1);
                }

                foreach (EnemyToken enemies in currentQuestStep.Conditions.RequiredKills.Keys)
                {
                    newConditionals.enemyDefeated.Add(enemies, currentQuestStep.Conditions.RequiredKills[enemies]);
                }

                foreach (UsableItemToken item in currentQuestStep.Conditions.ItemRequired.Keys)
                {
                    newConditionals.itemGathered.Add(item, currentQuestStep.Conditions.ItemRequired[item]);
                }
                
                //Completion Requirments.
                newConditionals.areConditionalsCompleted = false;
                foreach (DialogueToken NPC in currentQuestStep.CompleteConditions.SpeakTo)
                {
                    newConditionals.completeSpokenTo.Add(NPC, 1);
                }

                foreach (SceneToken area in currentQuestStep.CompleteConditions.AreaVisited)
                {
                    newConditionals.completeAreaVisited.Add(area, 1);
                }

                foreach (EnemyToken enemies in currentQuestStep.CompleteConditions.RequiredKills.Keys)
                {
                    newConditionals.completeEnemyDefeated.Add(enemies, currentQuestStep.CompleteConditions.RequiredKills[enemies]);
                }

                foreach (UsableItemToken item in currentQuestStep.CompleteConditions.ItemRequired.Keys)
                {
                    newConditionals.completeItemGathered.Add(item, currentQuestStep.CompleteConditions.ItemRequired[item]);
                }

                newConditionals.areConditionalsCompleted = false;
            }
            CheckIfRequiredQuestIsComplete(quest);
            CheckQuestsThatRequireItems();
        }

        public void CheckQuestsThatRequireItems()
        {
            foreach (QuestToken quest in m_activeQuests.Keys)
            { 
                CheckIfRequiredItemIsObtained(quest);
            }
        }
        
        public void CheckIfRequiredQuestIsComplete(QuestToken quest)
        {
            foreach (QuestToken completedQuests in m_completedQuests)
            {
                foreach (KeyValuePair<QuestToken, int> questCompleted in m_activeQuests[quest].questCompleted)
                {
                    if (completedQuests == questCompleted.Key)
                    {
                        m_activeQuests[quest].questCompleted[questCompleted.Key] = 0;
                        CheckIfBaseQuestRequirementsMet(quest);
                    }
                }
            }
        }

        public void CheckIfNPCHasBeenSpokenTo(DialogueToken NPCSpokenTo)
        {
            foreach (QuestToken quest in m_activeQuests.Keys)
            {
                if (m_activeQuests[quest].spokenTo.ContainsKey(NPCSpokenTo))
                {
                    m_activeQuests[quest].spokenTo[NPCSpokenTo] = 0;
                    CheckIfBaseQuestRequirementsMet(quest);
                }
                else if (m_activeQuests[quest].areConditionalsCompleted &&
                         m_activeQuests[quest].completeSpokenTo.ContainsKey(NPCSpokenTo))
                {
                    m_activeQuests[quest].completeSpokenTo[NPCSpokenTo] = 0;
                    QuestStepAdvances(quest);
                }
            }
        }
        
        public void CheckIfAreaHasBeenVisited(SceneToken area)
        {
            foreach(QuestToken quest in m_activeQuests.Keys)
            {
                if (!m_activeQuests[quest].areConditionalsCompleted &&
                    m_activeQuests[quest].areaVisited.ContainsKey(area))
                {
                    m_activeQuests[quest].areaVisited[area] = 0;
                    CheckIfBaseQuestRequirementsMet(quest);
                }
                else if (m_activeQuests[quest].areConditionalsCompleted &&
                          m_activeQuests[quest].completeAreaVisited.ContainsKey(area))
                {
                    m_activeQuests[quest].completeAreaVisited[area] = 0;
                    QuestStepAdvances(quest);
                }
            }
        }

        public void CheckIfRequiredEnemiesHaveBeenKilled(EnemyToken killedEnemy, int amountKilled)
        {
            foreach (QuestToken activeQuest in m_activeQuests.Keys)
            {
                if (!m_activeQuests[activeQuest].areConditionalsCompleted && 
                    m_activeQuests[activeQuest].enemyDefeated.ContainsKey(killedEnemy))
                {
                    m_activeQuests[activeQuest].enemyDefeated[killedEnemy] -= amountKilled;
                    if (m_activeQuests[activeQuest].enemyDefeated[killedEnemy] < 0)
                    {
                        m_activeQuests[activeQuest].enemyDefeated[killedEnemy] = 0;
                        CheckIfBaseQuestRequirementsMet(activeQuest);
                    }
                }
                else if (m_activeQuests[activeQuest].areConditionalsCompleted &&
                         m_activeQuests[activeQuest].completeEnemyDefeated.ContainsKey(killedEnemy))
                {
                    m_activeQuests[activeQuest].completeEnemyDefeated[killedEnemy] -= amountKilled;
                    if (m_activeQuests[activeQuest].completeEnemyDefeated[killedEnemy] < 0)
                    {
                        m_activeQuests[activeQuest].completeEnemyDefeated[killedEnemy] = 0;
                        QuestStepAdvances(activeQuest);
                    }
                }
            }
        }
        
        public void CheckIfRequiredItemIsObtained(QuestToken quest)
        {
            InventoryService inventory = ServiceLocator.GetService<InventoryService>();

            foreach (KeyValuePair<UsableItemToken, int> item in inventory.ownedUsableItems )
            {
                 if (!m_activeQuests[quest].areConditionalsCompleted &&
                     m_activeQuests[quest].itemGathered.ContainsKey(item.Key))
                 {
                     m_activeQuests[quest].itemGathered[item.Key] = 
                         quest.Steps[m_activeQuests[quest].currentStep].Conditions.ItemRequired[item.Key] - item.Value;
                     if (m_activeQuests[quest].itemGathered[item.Key] < 0)
                     {
                         m_activeQuests[quest].itemGathered[item.Key] = 0;
                         CheckIfBaseQuestRequirementsMet(quest);
                     }
                 }
                 else if (m_activeQuests[quest].areConditionalsCompleted && 
                          m_activeQuests[quest].completeItemGathered.ContainsKey(item.Key))
                 {
                     m_activeQuests[quest].completeItemGathered[item.Key] = 
                         quest.Steps[m_activeQuests[quest].currentStep].CompleteConditions.ItemRequired[item.Key] - item.Value;
                     if (m_activeQuests[quest].completeItemGathered[item.Key] < 0)
                     {
                         m_activeQuests[quest].completeItemGathered[item.Key] = 0;
                         QuestStepAdvances(quest);
                     }
                 }
            }
        }

        public void QuestStepAdvances(QuestToken quest)
        {
            InventoryService inventoryService = ServiceLocator.GetService<InventoryService>();
            foreach (KeyValuePair<UsableItemToken, int> item in 
                     quest.Steps[m_activeQuests[quest].currentStep].ItemsRemovedUponCompletition.ItemsRemoved)
            {
                inventoryService.RemoveItemFromInventory(item.Key, item.Value);
            }

            foreach (KeyValuePair<UsableItemToken, int> item in
                     quest.Steps[m_activeQuests[quest].currentStep].ItemsRewardedUponCompletition.ItemsRewarded)
            {
                inventoryService.AddItemToInventory(item.Key, item.Value);
            }

            m_activeQuests[quest].currentStep++;
            UpdateConditionals(quest);
        }

        public void CheckIfBaseQuestRequirementsMet(QuestToken quest)
        {
            if (CheckIfQuestConditionsMet(quest))
            {
                m_activeQuests[quest].areConditionalsCompleted = true;
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
        public bool areConditionalsCompleted = false;
        public Dictionary<DialogueToken, int> completeSpokenTo;
        public Dictionary<SceneToken, int> completeAreaVisited;
        public Dictionary<EnemyToken, int> completeEnemyDefeated;
        public Dictionary<UsableItemToken, int> completeItemGathered;
    }
}