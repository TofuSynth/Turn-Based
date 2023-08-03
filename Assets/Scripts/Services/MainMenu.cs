using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneToken m_setup;
    public void StartGame()
    {
        SceneManager.LoadScene(m_setup.TargetSceneName);
    }
}
