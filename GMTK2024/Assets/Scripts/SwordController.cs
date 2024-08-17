using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwignSword : MonoBehaviour
{
    private Animator animator;
    private bool isLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float axis = Input.GetAxis("Horizontal");
        if(axis < -0.2f && !isLeft) {
            isLeft = true;
            animator.SetBool("isLeft", true);
        }
        else if(axis > 0.2f && isLeft){
            isLeft = false;
            animator.SetBool("isLeft", false);
        }

        if(Input.GetMouseButtonDown(0))
            animator.SetTrigger("Attack");
    }
}
