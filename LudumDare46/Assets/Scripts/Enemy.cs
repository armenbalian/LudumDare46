using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
