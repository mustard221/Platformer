using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndingScene : MonoBehaviour
{

    public void OnPlayAgainButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}