using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButtons : MonoBehaviour
{
    public void DeathMenuButtons(string sceneName)
    {
        // Load the game scene
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading Game Scene");
    }
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restarting Game");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
