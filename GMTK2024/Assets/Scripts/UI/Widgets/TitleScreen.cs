using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour
{
    private UILayer gameMenuLayer;
    private Animator animator;
    public AnimationClip jumpClip;
    public String sceneName;
    void Start()
    {
        // Acquire the game menu layer from the UIManager singleton
        gameMenuLayer = UIManager.Instance.GetGameMenuLayer();

        if (gameMenuLayer == null){
            Debug.LogWarning("GameMenuLayer could not be found.");
        }
    }
    public void StartGame()
    {
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        if (jumpClip != null)
        {
            // Wait for the length of the animation clip
            yield return new WaitForSeconds(jumpClip.length);
        }
        else
        {
            Debug.LogWarning("No animation clip assigned. Starting the game immediately.");
        }

        // Load the scene after the delay
        SceneManager.LoadSceneAsync(sceneName);
    }
    
    public void EndGame()
    {
        SceneManager.LoadSceneAsync("EndScene");
        SpawnManager.Instance.ClearCheckpoint();
    }


    public void PushAllChildren()
    {
        foreach (Transform child in transform)
        {
            UIElement uiElement = child.GetComponent<UIElement>();
            if (uiElement != null)
            {
                uiElement.Show();
                gameMenuLayer.Push(uiElement);
            }
        }
    }

    public void PopAllChildren()
    {
        foreach (Transform child in transform)
        {
            UIElement uiElement = child.GetComponent<UIElement>();
            if (uiElement != null)
            {
                uiElement.Hide();
                gameMenuLayer.Pop(uiElement);
            }
        }
    }
}
