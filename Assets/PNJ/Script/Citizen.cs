using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Human
{
    // Use this for initialization

    public bool afraid = false;

    protected override void Start()
    {
        base.Start();
        if (username == "")
            username = "Citizen";

        health = 50.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

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
        afraid = true;


    }

    IEnumerator Clean()
    {
        yield return new WaitForSeconds(5); // delay
        Destroy(gameObject);

    }

}
