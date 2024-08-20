using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private bool isGameOver = false;
    private string currentSceneName;  // To store the name of the current scene

    void Start()
    {
        // Store the name of the current scene
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
    }

    public void TriggerGameOver()
    {
        
        if (!isGameOver)
        {
            Debug.Log("gameover");
            isGameOver = true;
            Time.timeScale = 0f; // Pause the game
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(currentSceneName); // Reload the stored scene
    }
}
