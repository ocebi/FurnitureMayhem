using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;

    float moveTreshold = 0.1f;



    void Update()
    {

        if (rb == null) rb = GetComponent<Rigidbody>();

        if (rb.velocity.magnitude > moveTreshold)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void PlayAttack()
    {
        animator.SetTrigger("attack");
    }
}
