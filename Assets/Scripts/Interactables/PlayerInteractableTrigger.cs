using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tofu.TurnBased.Services;


namespace Tofu.TurnBased.Interactables
{
    public class PlayerInteractableTrigger : MonoBehaviour
    {
        private ControlsService m_controlsService;
        public LayerMask m_interactables;
        
        void Start()
        {
            m_controlsService = ServiceLocator.GetService<ControlsService>();
        }

       
        void Update()
        {
            InteractableRangeCheck();
        }
        
        private void InteractableRangeCheck()
        {
            if (m_controlsService.isInteractDown)
            {
                RaycastHit hitInfo;
                Vector3 m_rayStart = this.transform.position - this.transform.forward;
                if (Physics.Raycast(m_rayStart ,this.transform.forward,out hitInfo,1.75f,m_interactables)) {
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