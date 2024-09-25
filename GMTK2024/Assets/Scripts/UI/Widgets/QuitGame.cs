using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            SceneManager.LoadSceneAsync("TitleScene"); 
        }
        else
        {
            Debug.Log("Quit game.");
            Application.Quit();  // Quits the game when built (not applicable to WebGL)
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restart game.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reloads the current scene
    }


}
