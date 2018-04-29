using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBox : Box
{

    public override void Action(Player p)
    {
        p.hasGun = true;
        p.Scope();
        Destroy(gameObject);
    }

}
