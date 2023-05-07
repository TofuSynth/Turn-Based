using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Interactables;
using UnityEngine;

namespace Tofu.TurnBased.Chests
{
    public class ChestInteractable : Interactable
    {
        [SerializeField] private ChestToken m_chestToken;
        [SerializeField] GameObject m_chestTop;
        [SerializeField] private float m_chestOpenAngle;

        protected override void Start()
        {
            base.Start();
            if (m_chestToken.isOpened)
            {
                m_chestTop.transform.Rotate(m_chestOpenAngle, 0, 0, Space.Self);
            }
            
        }
        
        
        protected override void Interaction()
        {
            if (m_chestToken.isOpened == false)
            {
                m_chestToken.isOpened = true;
                m_chestTop.transform.Rotate(m_chestOpenAngle, 0, 0, Space.Self);
                print(m_chestToken.test);
            }
        }
    }
}