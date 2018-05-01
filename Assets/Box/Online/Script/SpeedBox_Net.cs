using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBox_Net : Box_Net
{

    public float speedValue = 4.0f;
    public int duration = 2;

    public override void Action(Player_Net p)
    {
        StartCoroutine(Effect(p));
    }

    IEnumerator Effect(Player_Net p)
    {
        p.running_speed += speedValue;
        p.walking_speed += speedValue;

        yield return new WaitForSeconds(duration);

        p.running_speed -= speedValue;
        p.walking_speed -= speedValue;

        triggered = false;

        avaible.Play();
    }
}
