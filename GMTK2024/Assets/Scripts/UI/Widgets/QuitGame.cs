using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quit game.");
        Application.Quit();  // Quits the game when built
    }

    public void RestartGame()
    {
        Debug.Log("Restart game.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reloads the current scene
    }
}
