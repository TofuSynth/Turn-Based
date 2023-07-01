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

    private Dictionary<DialogueToken, Dictionary<QuestDialogue, bool>> QuestDialogueProgress =
        new Dictionary<DialogueToken, Dictionary<QuestDialogue, bool>>();

   public void StartConversation(DialogueToken dialogueTree)
    {
        DialogueProgress.TryAdd(dialogueTree, 0);

        QuestDialogue questDialogue = CheckForValidQuestDialogue(dialogueTree);
        if (questDialogue != null)
        {
            PrintQuestDialogue(dialogueTree, questDialogue);
        }
        else
        {
            Dialogue currentDialogue = dialogueTree.Dialogue[DialogueProgress[dialogueTree]];
            PrintDialogue(dialogueTree);
        }
        
    }
private QuestDialogue CheckForValidQuestDialogue(DialogueToken dialogueTree)
    {
        foreach (QuestDialogue dialogue in dialogueTree.QuestDialogue)
        {
            if (!QuestDialogueProgress[dialogueTree][dialogue])
            {
                if (dialogue.Conditions.activeQuest)
                {
                    //Is the required quest in our activeQuest list? If not, move onto the next QuestDialogue in the list
                    if (!quest.activeQuests.ContainsKey(dialogue.Conditions.activeQuest)) continue;

                    //It is in the list. Check if it's at the required step. If not, move onto the next QuestDialogue
                    if (quest.activeQuests[dialogue.Conditions.activeQuest].currentStep ==
                        dialogue.Conditions.requireStepOfActiveQuest) return dialogue;
                    else continue;
                }

                if (dialogue.Conditions.QuestCompleted)
                {
                    //Is the required quest in our completed quest list? If not, move onto the next QuestDialogue in the list
                    if (quest.completedQuests.Contains(dialogue.Conditions.QuestCompleted)) return dialogue;
                    else continue;
                }

                //If we got this far, this quest dialogue doesn't require an activeQuest or a completed quest... which probably shouldn't happen?
                return dialogue;

            }
        }
        //No valid quest dialogue was found, returning null.
        return null; 
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

    private void PrintQuestDialogue(DialogueToken dialogueTree,QuestDialogue questDialogue)
    {
        foreach (Conversation conversation in questDialogue.Conversation)
        {
            print(conversation.name);
            print(conversation.script);
        }

        QuestDialogueProgress.TryAdd(dialogueTree,new Dictionary<QuestDialogue,bool>());
        QuestDialogueProgress[dialogueTree].TryAdd(questDialogue,true);
        DialogueProgress[dialogueTree] = dialogueTree.QuestDialogue[DialogueProgress[dialogueTree]].goToDialogueStepUponCompletion;
        quest.CheckIfNPCHasBeenSpokenTo(dialogueTree);
    }
}
