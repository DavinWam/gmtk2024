using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    public abstract void TurnOn();
    public abstract void TurnOff();
    public abstract void SetDuration(float duration);
    public abstract float GetDuration();
    public abstract void HandleDuration();
}
