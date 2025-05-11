using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public GameManager gameManager;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gameManager.LoadNextLevel();
            gameManager.completeLevel();
        }
    }
}
