using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Box : MonoBehaviour
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
        Player p = entity.transform.GetComponent<Player>();
        
        if (p != null && !triggered)
        {
            triggered = true;
            p.PlaySound(pickUpSound, 0.3f, false);
            Action(p);
            if(avaible != null)
                avaible.Stop();
        }
    }

    public abstract void Action(Player p);
}
