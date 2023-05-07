using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Interactables;
using Tofu.TurnBased.Services;
using UnityEngine;

namespace Tofu.TurnBased.SceneManagement
{
    public class SceneTransitionInteractable : Interactable
    {
        [SerializeField] private SceneToken m_targetScene;
        [SerializeField] public SpawnToken m_targetSpawn;
        private SceneTransitionService m_sceneTransitionService;
        

        protected override void Start()
        {
            base.Start();
            m_sceneTransitionService = ServiceLocator.GetService<SceneTransitionService>();
            m_sceneTransitionService.ReportSpawnTarget(this);
        }

        protected override void Interaction()
        {
            m_sceneTransitionService.GoToNewScene(m_targetScene, m_targetSpawn);
            
        }

    }
}