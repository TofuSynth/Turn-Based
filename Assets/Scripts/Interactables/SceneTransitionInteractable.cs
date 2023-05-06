using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Interactables;
using Tofu.TurnBased.Services;
using UnityEngine;

namespace Tofu.TurnBased.SceneManagement
{
    public class SceneTransitionInteractable : Interactable
    {
        [SerializeField] private ScriptableObject m_targetScene;
        [SerializeField] private ScriptableObject m_targetDoor;
        private SceneTransitionSevice m_sceneTransitionService;

        protected override void Start()
        {
            m_sceneTransitionService = ServiceLocator.GetService<SceneTransitionSevice>();
        }

        protected override void Interaction()
        {
            m_sceneTransitionService.targetScene = m_targetScene;
            m_sceneTransitionService.targetDoor = m_targetDoor;
        }

    }
}