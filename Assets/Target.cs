using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public float health = 100f;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage (float amount)
    {
        health -= amount;
       

        if (health <= 0f)
            Die();
        else
            animator.SetTrigger("isHit");
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 10f);
        
    }
}
