using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tofu.TurnBased.Dialogue;
using Tofu.TurnBased.Quests;
using UnityEngine;
using Tofu.TurnBased.Services;
using Unity.VisualScripting;
using UnityEngine.UIElements;


public class DialogueService : ServiceBase<DialogueService>
{
    private GameObject DialogueUi;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;
    private QuestManagementService quest;
    private GameStateService gameState;
    private ControlsService controlsService;
    private bool continueConversation = false;
    
    void Start() {
        quest = ServiceLocator.GetService<QuestManagementService>();
        gameState = ServiceLocator.GetService<GameStateService>();
        controlsService = ServiceLocator.GetService<ControlsService>();
        HideUI();
    }

    public void VisibleUI()
    {
        this.gameObject.SetActive(true);
    }

    void HideUI()
    {
        this.gameObject.SetActive(false);
    }
    private Dictionary<DialogueToken, int> DialogueProgress = new Dictionary<DialogueToken, int>();

    private Dictionary<DialogueToken, Dictionary<QuestDialogue, bool>> QuestDialogueProgress =
        new Dictionary<DialogueToken, Dictionary<QuestDialogue, bool>>();

   public void StartConversation(DialogueToken dialogueTree)
    {
        gameState.DialogueState();
        DialogueProgress.TryAdd(dialogueTree, 0);
        QuestDialogueProgress.TryAdd(dialogueTree, new Dictionary<QuestDialogue, bool>());

        QuestDialogue questDialogue = CheckForValidQuestDialogue(dialogueTree);
        if (questDialogue != null)
        {
            PrintQuestDialogue(dialogueTree, questDialogue);
        }
        else
        {
            Dialogue currentDialogue = dialogueTree.Dialogue[DialogueProgress[dialogueTree]];
            StartCoroutine(PrintDialogue(dialogueTree));
        }
        
    }
    private QuestDialogue CheckForValidQuestDialogue(DialogueToken dialogueTree)
    {
        foreach (QuestDialogue dialogue in dialogueTree.QuestDialogue)
        {
            QuestDialogueProgress[dialogueTree].TryAdd(dialogue, false);
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
        
    private IEnumerator PrintDialogue(DialogueToken dialogueTree)
    {
        foreach (Conversation conversation in dialogueTree.Dialogue[DialogueProgress[dialogueTree]].Conversation)
        {
            FillTextUI(conversation);
            if (controlsService.isInteractDown)
            {
                ProgressDialogue();
            }
            while (!continueConversation) {
                yield return null;
            }

            continueConversation = false;
        }
        if (!dialogueTree.Dialogue[DialogueProgress[dialogueTree]].questRequiredAfterToAdvance &&
            DialogueProgress[dialogueTree] < dialogueTree.Dialogue.Count - 1)
        {
            DialogueProgress[dialogueTree]++;
        }
    }

    private void PrintQuestDialogue(DialogueToken dialogueTree,QuestDialogue questDialogue)
    {
        foreach (Conversation conversation in questDialogue.Conversation)
        {
            FillTextUI(conversation);
        }

        QuestDialogueProgress.TryAdd(dialogueTree,new Dictionary<QuestDialogue,bool>());
        QuestDialogueProgress[dialogueTree].TryAdd(questDialogue,true);
        DialogueProgress[dialogueTree] = dialogueTree.QuestDialogue[DialogueProgress[dialogueTree]].goToDialogueStepUponCompletion;
        quest.CheckIfNPCHasBeenSpokenTo(dialogueTree);
    }

    private void FillTextUI(Conversation conversation)
    {
        nameText.text = conversation.name;
        dialogueText.text = conversation.script;
    }
    
    private void ProgressDialogue() {
        continueConversation = true;
    }

   /* private IEnumerator PrintDialogueCoroutine() {
        foreach (Conversation conversation in dialogueTree.Dialogue[DialogueProgress[dialogueTree]].Conversation) {
            print(conversation.name);
            print(conversation.script);
  
            //Every frame, we check if this boolean is false. If it is, we wait.
            while (!continueConversation) {
                yield return null; //This means "Wait for a frame"
            }
            //Reset the flag before going to the next iteration of the loop
            continueConversation = false;
        }
    }
*/}
