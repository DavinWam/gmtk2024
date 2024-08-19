using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    private UILayer gameMenuLayer;
    private Animator animator;
    void Start()
    {
        // Acquire the game menu layer from the UIManager singleton
        gameMenuLayer = UIManager.Instance.GetGameMenuLayer();

        if (gameMenuLayer == null){
            Debug.LogWarning("GameMenuLayer could not be found.");
        }
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
