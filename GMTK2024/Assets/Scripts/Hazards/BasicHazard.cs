using System.Collections;
using UnityEngine;

public class BasicHazard : Hazard
{
    private Collider2D hazardCollider;
    private float duration;

    public virtual void Awake()
    {
        // Get the BoxCollider2D component on this GameObject
        hazardCollider = GetComponent<Collider2D>();

        if (hazardCollider == null)
        {
            Debug.LogError("BoxCollider2D component not found on this object. Please ensure it is attached.");
        }
    }

    // Turn on the fire
    public override void TurnOn()
    {
        // Enable the BoxCollider2D
        if (hazardCollider != null)
        {
            hazardCollider.enabled = true;
        }
    }

    // Turn off the fire
    public override void TurnOff()
    {
        // Disable the BoxCollider2D
        if (hazardCollider != null)
        {
            hazardCollider.enabled = false;
        }
    }

    public override void HandleDuration(){
        Debug.Log("duration");
        if(duration == 0) return;

        StartCoroutine(AutoClose());
    }
     
    IEnumerator AutoClose(){
        yield return new WaitForSeconds(duration);
        TurnOff();
    }
     
    public override void SetDuration(float newDuration)
    {
        duration = newDuration;
    }

    public override float GetDuration()
    {
        return duration;
    }
}
