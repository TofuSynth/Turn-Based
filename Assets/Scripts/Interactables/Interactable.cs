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
        [SerializeField] private GameObject m_player;
        private bool m_isTriggerConditionMet = false;

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

        }

        protected virtual void Interaction()
        {
            // Specific interaction behaviour defined in child classes
        }

    }
}