using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField, ReadOnly] Rigidbody rb;
    [SerializeField, ReadOnly] Animator animator;

    float moveTreshold = 0.1f;

    [Button]
    private void SetRefs()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnValidate()
    {
        SetRefs();
    }


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
