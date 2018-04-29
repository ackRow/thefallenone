﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : Box {

    // Donne un gain de vie au joueur

    public float damage = 25f;

   public override void Action(Player p)
    {
        p.TakeDamage(damage, p);
        Destroy(gameObject);
    }

}