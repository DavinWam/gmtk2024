using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetRunningAnimation(bool isRunning)
    {
        if (animator != null)
        {
            animator.SetBool("IsRunning", isRunning);
        }
    }
}
