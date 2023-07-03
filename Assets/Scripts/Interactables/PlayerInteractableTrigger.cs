using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tofu.TurnBased.Services;


namespace Tofu.TurnBased.Interactables
{
    public class PlayerInteractableTrigger : MonoBehaviour
    {
        private ControlsService m_controlsService;
        private GameStateService m_gameStateService;
        public LayerMask m_interactables;
        
        void Start()
        {
            m_controlsService = ServiceLocator.GetService<ControlsService>();
            m_gameStateService = ServiceLocator.GetService<GameStateService>();
        }

       
        void Update()
        {
            InteractableRangeCheck();
        }
        
        private void InteractableRangeCheck()
        {
            if (m_controlsService.isInteractDown && m_gameStateService.GetState() == GameStateService.GameState.Normal)
            {
                RaycastHit hitInfo;
                Vector3 rayStart = this.transform.position - this.transform.forward;
                if (Physics.Raycast(rayStart ,this.transform.forward,out hitInfo,2.75f,m_interactables)) {
                    //hitInfo.collider.gameObject will get you access to the gameObject you've hit
                    Interactable interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();

                    if (interactable)
                    {
                        interactable.Interaction();
                    }
                    
                }
            }
        }
        
    }
}