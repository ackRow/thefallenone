using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBox : Box {

    // Donne un gain de vie au joueur

    public float heal = 25f;

   public override void Action(Player p)
    {
        p.Heal(heal);
        Destroy(gameObject);
    }

}
