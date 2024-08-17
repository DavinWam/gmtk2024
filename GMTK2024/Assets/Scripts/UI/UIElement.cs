using UnityEngine;

public class UIElement : MonoBehaviour
{
    public void Show()
    {
        SetChildrenActive(true);
    }

    public void Hide()
    {
        SetChildrenActive(false);
    }

    private void SetChildrenActive(bool isActive)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }
}
