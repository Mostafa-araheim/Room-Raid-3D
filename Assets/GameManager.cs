using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject completeLevelUI;
    public void Restart()
    {
        // Restart the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void completeLevel()
    {
        completeLevelUI.SetActive(true);
    }

}
