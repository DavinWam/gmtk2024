using System.Collections.Generic;
using UnityEngine;

public class UILayer : MonoBehaviour, IUILayer
{
    public enum LayerType{
        None,
        Game,
        GameMenu,
        Modal,
        Menu
    }
    private List<UIElement> uiElements = new List<UIElement>();

    // Reference to the UIManager to access other layers
    private UIManager uiManager;
    public LayerType layerType;
    void Start()
    {
        uiManager = UIManager.Instance; // Get the reference to the UIManager
        switch (layerType){
            case LayerType.Game:
                SetGame();
                break;
            case LayerType.Modal:
                SetModal();
                break;
            case LayerType.GameMenu:
                SetGameMenu();
                break;
            case LayerType.Menu:
                SetMenu();
                break;
        }
    }

    public void Push(UIElement element)
    {
        uiElements.Add(element);
        element.Show(); // Show the UI element
    }

    public UIElement Pop()
    {
        if (uiElements.Count > 0)
        {
            UIElement topElement = uiElements[uiElements.Count - 1];
            uiElements.RemoveAt(uiElements.Count - 1);
            topElement.Hide(); // Hide the UI element
            return topElement;
        }
        return null;
    }

    public UIElement Pop(UIElement element)
    {
        if (uiElements.Contains(element))
        {
            uiElements.Remove(element);
            element.Hide(); // Hide the specific UI element
            return element;
        }
        return null;
    }

    public UIElement Peek()
    {
        return uiElements.Count > 0 ? uiElements[uiElements.Count - 1] : null;
    }

    public bool IsEmpty()
    {
        return uiElements.Count == 0;
    }

    // New method to hide other UI layers
    public void HideOthers(){
         UIManager.Instance.HideOtherLayers(this);
    }

    // Method to hide all UI elements in the current layer
    public void HideAll()
    {
        foreach (UIElement element in uiElements)
        {
            element.Hide();
        }
    }
    public void SetGame(){
        UIManager.Instance.gameLayer = this;
    }
    public void SetMenu(){
        UIManager.Instance.menuLayer = this;
    }
        public void SetGameMenu(){
        UIManager.Instance.gameMenuLayer = this;
    }
        public void SetModal(){
        UIManager.Instance.modalLayer = this;
    }
}
