using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance { get; private set; }

    // for the in-game HUD (Heads-Up Display) elements, 
    //such as health bars, score counters, and other elements that are visible during 
    //gameplay and don't interrupt the player's control.
    public UILayer gameLayer;

    // for in-game menus that the player can interact with
    // or without pausing the game, such as inventory screens, skill trees, or map interfaces.
    // These elements usually overlay the game but still allow the game to continue running in the background.
    public UILayer gameMenuLayer;

    // for major game menus, such as the pause screen, main menu, or settings menu.
    // When active, these elements typically take focus and may pause the game or prevent the player from 
    // interacting with other game elements until the menu is closed.
    public UILayer menuLayer;

    // for pop-up windows, dialogs, or confirmations that require the player's 
    // immediate attention and input. These elements usually block interaction with other UI layers until 
    // the player addresses the modal (e.g., confirming an action or dismissing a notification).
    public UILayer modalLayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep this UIManager across different scenes
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one instance
        }
    }

    public UILayer GetGameLayer()
    {
        return gameLayer;
    }

    // Add additional methods to get other layers if needed
    public UILayer GetGameMenuLayer()
    {
        return gameMenuLayer;
    }

    public UILayer GetMenuLayer()
    {
        return menuLayer;
    }

    public UILayer GetModalLayer()
    {
        return modalLayer;
    }
    // Method to hide all other UI layers except the one passed as a parameter
    public void HideOtherLayers(UILayer activeLayer)
    {
        if (gameLayer != activeLayer)
        {
            gameLayer.HideAll();
        }
        if (gameMenuLayer != activeLayer)
        {
            gameMenuLayer.HideAll();
        }
        if (menuLayer != activeLayer)
        {
            menuLayer.HideAll();
        }
        if (modalLayer != activeLayer)
        {
            modalLayer.HideAll();
        }
    }
}


