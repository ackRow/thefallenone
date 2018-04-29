using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Net : Box_Net
{

    // Donne un gain de vie au joueur

    public float damage = 25f;

    public override void Action(Player_Net p)
    {
        p.TakeDamage(damage, "");
        Destroy(gameObject);
    }

}
