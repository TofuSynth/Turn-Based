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
        [SerializeField] private SpawnToken m_targetSpawn;
        private SceneTransitionSevice m_sceneTransitionService;
        private TransitionSpawnService m_transitionSpawnService;

        protected override void Start()
        {
            base.Start();
            m_sceneTransitionService = ServiceLocator.GetService<SceneTransitionSevice>();
            m_transitionSpawnService = ServiceLocator.GetService<TransitionSpawnService>();
        }

        protected override void Interaction()
        {   
            m_transitionSpawnService.ClearSpawns();
            m_sceneTransitionService.GoToNewScene(m_targetScene, m_targetSpawn);
            m_transitionSpawnService.FillSpawnDictionary();
        }

    }
}