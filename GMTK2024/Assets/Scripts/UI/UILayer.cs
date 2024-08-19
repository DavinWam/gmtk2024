using System.Collections.Generic;
using UnityEngine;

public class UILayer : MonoBehaviour, IUILayer
{
    private List<UIElement> uiElements = new List<UIElement>();

    // Reference to the UIManager to access other layers
    private UIManager uiManager;

    void Start()
    {
        uiManager = UIManager.Instance; // Get the reference to the UIManager
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
        uiManager.HideOtherLayers(this);
    }

    // Method to hide all UI elements in the current layer
    public void HideAll()
    {
        foreach (UIElement element in uiElements)
        {
            element.Hide();
        }
    }
}
