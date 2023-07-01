using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Dialogue;
using Tofu.TurnBased.Quests;
using UnityEngine;
using Tofu.TurnBased.Services;


public class DialogueService : ServiceBase<DialogueService>
{
    QuestManagementService quest = ServiceLocator.GetService<QuestManagementService>();
    
    private Dictionary<DialogueToken, int> DialogueProgress = new Dictionary<DialogueToken, int>();
    private Dictionary<DialogueToken, int> QuestDialogueProgress = new Dictionary<DialogueToken, int>();

    public void StartConversation(DialogueToken dialogueTree)
    {
        DialogueProgress.TryAdd(dialogueTree, 0);
        
        bool doesDialogueHaveQuestCompletedRequirement =
            dialogueTree.QuestDialogue[DialogueProgress[dialogueTree]].Conditions.QuestCompleted;
        
        bool doesDialogueHaveActiveQuestRequirement =
            dialogueTree.QuestDialogue[DialogueProgress[dialogueTree]].Conditions.activeQuest;
        
        bool isRequiredQuestCompleted = 
            quest.completedQuests.Contains(dialogueTree.QuestDialogue[DialogueProgress[dialogueTree]].Conditions
                .QuestCompleted);

        if (dialogueTree.QuestDialogue.Count > 0)
        {
            
        }
        else
        {
            PrintDialogue(dialogueTree);
        }
        
    }

    private void PrintDialogue(DialogueToken dialogueTree)
    {
        foreach (Conversation conversation in dialogueTree.Dialogue[DialogueProgress[dialogueTree]].Conversation)
        {
            print(conversation.name);
            print(conversation.script);
        }
        if (!dialogueTree.Dialogue[DialogueProgress[dialogueTree]].questRequiredAfterToAdvance)
        {
            DialogueProgress[dialogueTree]++;
        }
    }
}
