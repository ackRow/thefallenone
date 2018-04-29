using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBox : Box { 

    public float speedValue = 4.0f;
    public int duration = 5;

    public override void Action(Player p)
    {
        StartCoroutine(Effect(p));
    }

    IEnumerator Effect(Player p)
    {
        p.running_speed += speedValue;
        p.walking_speed += speedValue;

        // Grisé

        yield return new WaitForSeconds(duration);

        p.running_speed -= speedValue;
        p.walking_speed -= speedValue;

        // Narmol
        triggered = false;
    }
}
