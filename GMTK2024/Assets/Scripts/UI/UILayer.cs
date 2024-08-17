using System.Collections.Generic;
using UnityEngine;

public class UILayer : MonoBehaviour, IUILayer
{
    private Stack<UIElement> uiStack = new Stack<UIElement>();

    public void Push(UIElement element)
    {
        uiStack.Push(element);
        element.Show(); // Assuming UIElement has a Show method to make it visible
    }

    public UIElement Pop()
    {
        if (uiStack.Count > 0)
        {
            UIElement topElement = uiStack.Pop();
            topElement.Hide(); // Assuming UIElement has a Hide method to make it invisible
            return topElement;
        }
        return null;
    }

    public UIElement Peek()
    {
        return uiStack.Count > 0 ? uiStack.Peek() : null;
    }

    public bool IsEmpty()
    {
        return uiStack.Count == 0;
    }
}
