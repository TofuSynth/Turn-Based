using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tofu.TurnBased.SceneManagement
{
    public class SceneLoading : MonoBehaviour
    {
        public void LoadScene(SceneToken token)
        {
            SceneManager.LoadScene(token.TargetSceneName);
        }
    }
}