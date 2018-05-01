using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealBox_Net : Box_Net {

    // Donne un gain de vie au joueur

    public float heal = 25f;
    public int duration = 5;

    public override void Action(Player_Net p)
    {
        p.CmdHeal(heal);
        StartCoroutine(Effect(p));
    }

    IEnumerator Effect(Player_Net p)
    {
        yield return new WaitForSeconds(duration);

        triggered = false;

        avaible.Play();
    }

}
