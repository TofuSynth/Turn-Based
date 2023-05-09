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
        [SerializeField] protected GameObject m_player;

        public virtual void Interaction()
        {
            // Specific interaction behaviour defined in child classes
        }

    }
}