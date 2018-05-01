using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Box_Net : NetworkBehaviour
{

    protected bool triggered = false;
    public AudioClip pickUpSound;
    protected ParticleSystem avaible;

    public void Start()
    {
        avaible = GetComponentInChildren<ParticleSystem>();
    }

    public void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

    }

    void OnTriggerEnter(Collider entity)
    {
        Player_Net p = entity.transform.GetComponent<Player_Net>();
       
        if (p != null && !triggered)
        {
            p.PlaySound(pickUpSound, 0.1f, false);
            triggered = true;
            Action(p);
            if (avaible != null)
                avaible.Stop();

        }
    }

    public abstract void Action(Player_Net p);
}
