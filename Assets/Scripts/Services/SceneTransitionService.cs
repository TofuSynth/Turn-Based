using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tofu.TurnBased.SceneManagement
{
    public class SceneTransitionService : ServiceBase<SceneTransitionService>
    {
        private PlayerController m_player;
        private SpawnToken m_savedSpawn;
        private void Start()
        {
            m_player = FindObjectOfType<PlayerController>();
        }
        
        public void GoToNewScene(SceneToken sceneToken, SpawnToken spawnToken)
        {
            m_savedSpawn = spawnToken;
            SceneManager.LoadScene(sceneToken.TargetSceneName);
            

        }
        public void ReportSpawnTarget(SceneTransitionInteractable target)
        {
            if (target.m_targetSpawn == m_savedSpawn)
            {
                m_player.transform.position = target.transform.position;
            }
        }
    }
}