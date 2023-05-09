using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Tofu.TurnBased.Services;
using UnityEngine;

namespace Tofu.TurnBased.Interactables
{
    public class Interactable : MonoBehaviour
    {
        private ControlsService m_controlsService;
        [SerializeField] protected GameObject m_player;
        private bool m_isTriggerConditionMet = false;
        public LayerMask interactables;

        protected virtual void Start()
        {
            m_controlsService = ServiceLocator.GetService<ControlsService>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject == m_player)
            {
                m_isTriggerConditionMet = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == m_player)
            {
                m_isTriggerConditionMet = false;
            }
        }
        
        private void Update()
        {
            if (m_controlsService.isInteractDown && m_isTriggerConditionMet)
            {
                Interaction();
            }
            InteractableRangeCheck();
        }

        private void InteractableRangeCheck()
        {
            if (m_controlsService.isInteractDown)
            {
                Ray InteractionCheck = new Ray(m_player.transform.position, m_player.transform.forward);
                Debug.DrawRay(m_player.transform.position, m_player.transform.forward, Color.magenta, 120f);
                if (Physics.Raycast(InteractionCheck, m_player.transform.position.z + 5f, interactables))
                {
                    Interaction();
                }
            }
        }

        protected virtual void Interaction()
        {
            // Specific interaction behaviour defined in child classes
        }

    }
}