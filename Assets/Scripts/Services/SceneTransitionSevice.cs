using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tofu.TurnBased.SceneManagement
{
    public class SceneTransitionSevice : ServiceBase<SceneTransitionSevice>
    {
        private PlayerController m_player;
        private void Start()
        {
            m_player = FindObjectOfType<PlayerController>();
        }

        public void GoToNewScene(SceneToken sceneToken, SpawnToken spawnToken)
        {
            SceneManager.LoadScene(sceneToken.TargetSceneName);

            //m_player.transform.position = FindObjectOfType<DoorToken>().transform.position;

        }
    }
}