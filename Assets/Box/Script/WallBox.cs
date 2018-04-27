using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBox : Box {

    // Use this for initialization
    public int duration = 5;
    public override void Action(Player p)
    {
        //Destroy(gameObject);

        StartCoroutine(Effect(p));
    }

    IEnumerator Effect(Player p)
    {
        p.hasWallhack = true;

        yield return new WaitForSeconds(duration); // delay

        p.hasWallhack = false;

        triggered = false;
    }
}
