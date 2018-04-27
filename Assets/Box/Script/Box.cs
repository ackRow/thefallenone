using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Box : MonoBehaviour {

    protected bool triggered = false;


    void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}

    void OnTriggerEnter(Collider entity)
    {
        Player p = entity.transform.GetComponent<Player>();

        if (p != null && !triggered)
        {
            triggered = true;
            Action(p);

        }
    }

    public abstract void Action(Player p);
}
