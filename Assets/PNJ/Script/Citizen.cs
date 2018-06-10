using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Human
{
    // Use this for initialization

    public bool afraid = false;
    private bool wasAfraid = false;

    protected override void Start()
    {
        base.Start();
        if (username == "")
            username = "Citizen";
        walking_speed = 2.5f;
        running_speed = 6f;
        health = 100.0f;

        //if (myAudio != null && !walking)
        //    PlaySound(punchSound, 1.0f, false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(!walking && wasAfraid != afraid)
        {
            if (myAudio != null)
            {
                myAudio.loop = false;
                myAudio.Stop();
                PlaySound(gunShotSound, 0.4f, false);
               
            }
            _capsCollider.height /= 1.1f;
            _capsCollider.center /= 1.1f;
            wasAfraid = afraid;
        }

        if (_animator && !dead)
        {
            _animator.SetFloat("Afraid", afraid ? 1.0f : 0.0f);
        }
    }

    public override void Die()
    {
        base.Die();
        _body.isKinematic = true;
        _capsCollider.isTrigger = true;


        StartCoroutine(Clean());

    }

    public override void TakeDamage(float damage, Human caller)
    {
        base.TakeDamage(damage, caller);

        if(caller is Player)
        {
            Player p = (Player)caller;
            p.hasShotCitizen = true;
        }
        


    }

    IEnumerator Clean()
    {
        yield return new WaitForSeconds(5); // delay
        Destroy(gameObject);

    }

}
