using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Human
{


    // Use this for initialization
    new void Start () {
        base.Start();

        username = "Guard";
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
    }

    /*public void TakeDamage(float damage, Human caller)
    {
        health -= damage;
        if (health <= 0f)
            Die();
        else
            animator.SetTrigger("isHit"); // animation lorsqu'on est touché
    }

    public void Die()
    {
        base.Die();

    }*/

}
