public interface IUILayer
{
    void Push(UIElement element);
    UIElement Pop();
    UIElement Peek();
    bool IsEmpty();
}
