using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Human
{
    public bool lootGun = false;
    public GameObject gunBox;
    // Use this for initialization
    protected override void Start () {
        base.Start();
        gunFireBuff = 2.0f;
        walking_speed = 2.5f;
        gunDamage = 20.0f;
        if(username == "")
            username = "Guard";
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
    }

	
	public override void Die()
    {
        base.Die();
        _body.isKinematic = true;
        _capsCollider.isTrigger = true;

        if (lootGun)
        {
            Instantiate(gunBox).transform.position = transform.position + new Vector3(0, 0.2f, 0);
        }

        StartCoroutine(Clean());
        
    }

    IEnumerator Clean()
    {
        yield return new WaitForSeconds(5); // delay
       Destroy(gameObject);

    }

}
