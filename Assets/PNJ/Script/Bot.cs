using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Human
{

    // Use this for initialization
    new void Start () {
        base.Start();
        gunFireBuff = 2.0f;
        walking_speed = 2.5f;
        gunDamage = 20.0f;
        if(username == "")
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
