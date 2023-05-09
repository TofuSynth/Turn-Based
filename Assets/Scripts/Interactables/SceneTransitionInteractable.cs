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
        public GameObject player
        {
            get { return m_player; }
        }
        
        void Start()
        {
            
            m_sceneTransitionService = ServiceLocator.GetService<SceneTransitionService>();
            m_sceneTransitionService.ReportSpawnTarget(this);
        }

        public override void Interaction()
        {
            m_sceneTransitionService.GoToNewScene(m_targetScene, m_targetSpawn);
            
        }

    }
}