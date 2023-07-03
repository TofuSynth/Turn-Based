using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Dialogue;
using Tofu.TurnBased.Interactables;
using Tofu.TurnBased.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace Tofu.TurnBased.NPC
{
    public class NPCInteractable : Interactable
    {
        private DialogueService dialogueService;
        [SerializeField] private DialogueToken dialogueToken;
        

        private void Start()
        {
            dialogueService = ServiceLocator.GetService<DialogueService>();
        }

        public override void Interaction()
        {
            dialogueService.gameObject.SetActive(true);
            dialogueService.StartConversation(dialogueToken);
        }
    }
}