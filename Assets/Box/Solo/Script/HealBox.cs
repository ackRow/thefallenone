using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBox : Box {

    // Donne un gain de vie au joueur

    public float heal = 25f;

   public override void Action(Player p)
    {
        FindObjectsOfType<FadeAnim>()[0].GetBoxFade("heal");
        p.Heal(heal);
        Destroy(gameObject);
    }

}
