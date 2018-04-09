using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // faut utiliser l'UI

public class Player : Human { // Hérite de la classe human

    private Slider playerHealth;

    private Animator ArmAnimator; // les mains en vue fps

    public bool FPSView = true;

    private GameObject gun;
    public GameObject head;

    public GameObject[] ArmExt; // last object is the gun

    public GameObject[] ArmFPS;

    // Use this for initialization
    new void Start () {
        base.Start();
        
        ArmAnimator = GetComponentsInChildren<Animator>()[1];

        if (FPSView)
        {
            head.GetComponent<Renderer>().enabled = false; // On cache la tête du joueur (car vue FPS)
            foreach (var obj in ArmExt)
            {
                obj.GetComponent<Renderer>().enabled = false;
            }

            gun = ArmFPS[ArmFPS.Length - 1];
        }
        else
        {
            head.GetComponent<Renderer>().enabled = true; // On cache la tête du joueur (car vue FPS)
            foreach (var obj in ArmFPS)
            {
                obj.GetComponent<Renderer>().enabled = false;
            }

            gun = ArmExt[ArmExt.Length - 1];
        }

       // playerHealth = FindObjectsOfType<Slider>()[0]; // On recupère le slider
    }
	
	// Update is called once per frame
	new void Update () {

        /* Animation FPS Arm */
        Animate(ArmAnimator);

        base.Update();

        /* UI */
        //playerHealth.value = health;

        /* Hide and Show gun */
        gun.GetComponent<Renderer>().enabled = hasGun && isScoping;

        

    }

    

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override void Die()
    {
        base.Die();
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5); // delay

        dead = false;
        // On relance l'animation Idle et on remet la vie à 100
        _animator.Play("Idle", -1, 0f);
        //transform.position = spawnPoints[Random.Range(0, 4)].transform.position;
        health = 100f;
    }
}
