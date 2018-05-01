using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Net : Box_Net
{

    // Donne un gain de vie au joueur

    public float damage = 25f;
    public int duration = 5;

    public override void Action(Player_Net p)
    {
        p.TakeDamage(damage, "");
        StartCoroutine(Effect(p));
    }

    IEnumerator Effect(Player_Net p)
    {
        yield return new WaitForSeconds(duration);

        triggered = false;

        avaible.Play();
    }

}
