using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Human
{
    // Use this for initialization
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
    }


    public override void Die()
    {
        base.Die();
        _body.isKinematic = true;
        _capsCollider.isTrigger = true;


        StartCoroutine(Clean());

    }

    IEnumerator Clean()
    {
        yield return new WaitForSeconds(5); // delay
        Destroy(gameObject);

    }

}
