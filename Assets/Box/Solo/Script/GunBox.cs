using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBox : Box
{
    new void Start()
    {
        transform.Rotate(new Vector3(90, 0, 0));
    }
    new void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

    public override void Action(Player p)
    {
        p.hasGun = true;
        p.Scope();
        Destroy(gameObject);
    }

}
