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
    private GameObject m_dialogueUi;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;
    private QuestManagementService m_quest;
    private GameStateService m_gameState;
    private ControlsService m_controlsService;
    
    void Start() {
        m_quest = ServiceLocator.GetService<QuestManagementService>();
        m_gameState = ServiceLocator.GetService<GameStateService>();
        m_controlsService = ServiceLocator.GetService<ControlsService>();
        HideDialogueUi();
    }

    public void MakeDialogueUIVisible()
    {
        this.gameObject.SetActive(true);
    }

    void HideDialogueUi()
    {
        this.gameObject.SetActive(false);
    }
    private Dictionary<DialogueToken, int> DialogueProgress = new Dictionary<DialogueToken, int>();

    private Dictionary<DialogueToken, Dictionary<QuestDialogue, bool>> QuestDialogueProgress =
        new Dictionary<DialogueToken, Dictionary<QuestDialogue, bool>>();

   public void StartConversation(DialogueToken dialogueTree)
    {
        m_gameState.DialogueState();
        DialogueProgress.TryAdd(dialogueTree, 0);
        QuestDialogueProgress.TryAdd(dialogueTree, new Dictionary<QuestDialogue, bool>());

        QuestDialogue questDialogue = CheckForValidQuestDialogue(dialogueTree);
        if (questDialogue != null)
        {
            StartCoroutine(PrintQuestDialogue(dialogueTree, questDialogue));
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
                    if (!m_quest.activeQuests.ContainsKey(dialogue.Conditions.activeQuest)) continue;

                    //It is in the list. Check if it's at the required step. If not, move onto the next QuestDialogue
                    if (m_quest.activeQuests[dialogue.Conditions.activeQuest].currentStep ==
                        dialogue.Conditions.requireStepOfActiveQuest) return dialogue;
                    else continue;
                }

                if (dialogue.Conditions.QuestCompleted)
                {
                    //Is the required quest in our completed quest list? If not, move onto the next QuestDialogue in the list
                    if (m_quest.completedQuests.Contains(dialogue.Conditions.QuestCompleted)) return dialogue;
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
            yield return null;
            int totalPages = dialogueText.textInfo.pageCount;
            for (int currentPage = 1; currentPage <= totalPages; currentPage++)
            {
                dialogueText.pageToDisplay = currentPage;
                do
                {
                    yield return null;
                } while (!m_controlsService.isInteractDown);
            }

        }
        if (!dialogueTree.Dialogue[DialogueProgress[dialogueTree]].questRequiredAfterToAdvance &&
            DialogueProgress[dialogueTree] < dialogueTree.Dialogue.Count - 1)
        {
            DialogueProgress[dialogueTree]++;
        }
        ExitDialogue();
    }

    private IEnumerator PrintQuestDialogue(DialogueToken dialogueTree,QuestDialogue questDialogue)
    {
        foreach (Conversation conversation in questDialogue.Conversation)
        {
            FillTextUI(conversation);
            yield return null;
            int totalPages = dialogueText.textInfo.pageCount;
            for (int currentPage = 1; currentPage <= totalPages; currentPage++)
            {
                dialogueText.pageToDisplay = currentPage;
                do
                {
                    yield return null;
                } while (!m_controlsService.isInteractDown);
            }
        }

        QuestDialogueProgress.TryAdd(dialogueTree,new Dictionary<QuestDialogue,bool>());
        QuestDialogueProgress[dialogueTree].TryAdd(questDialogue,true);
        DialogueProgress[dialogueTree] = dialogueTree.QuestDialogue[DialogueProgress[dialogueTree]].goToDialogueStepUponCompletion;
        m_quest.CheckIfNPCHasBeenSpokenTo(dialogueTree);
        ExitDialogue();
    }

    private void FillTextUI(Conversation conversation)
    {
        nameText.text = conversation.name;
        dialogueText.text = conversation.script;
    }
    

    private void ExitDialogue()
    {
        HideDialogueUi();
        m_gameState.NormalState();
    }
}
