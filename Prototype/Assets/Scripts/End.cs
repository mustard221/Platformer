using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with the EndPlatform
        if (collision.gameObject.CompareTag("Ending"))
        {
            LoadEndingScene();
        }
    }

    private void LoadEndingScene()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
