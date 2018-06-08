﻿using System.Collections.Generic;
using UnityEngine;

public class CitizenController : MonoBehaviour
{

    Citizen citizen;

    public List<Renderer> listToRender;
    public Material _forceField, _wallhack;

    //bool afraid = false;
    float angle;
	float randomAngle;
	
    //public Human target;
    Vector3 forward = new Vector3(1.0f, 0, 0);

    int round = 50;
	bool stopRoutine = false;
	
    public Human target; // for wallhack and scared
    bool isWallhack = false;

    // Use this for initialization
    void Start()
    {
        citizen = GetComponent<Citizen>();
        angle = 90.0f;
        //bot.Scope();
		randomAngle = Random.Range(0, 180);
    }

    // Update is called once per frame
    void Update()
    {

        

        if (target is Player)
        {
            Player p = (Player)target;

            if (p.hasShotCitizen)
            {
                citizen.afraid = true;
                citizen.Forward(false, Vector3.zero);
				/*stopRoutine = true;
				afraidMovement()*/
            }
            else
                citizen.Forward(false, forward);

            if (isWallhack != p.hasWallhack)
            {
                isWallhack = p.hasWallhack;
                activeWallhack(p.hasWallhack);

            }
        }
        else {
            citizen.Forward(false, forward);
        }
    }

    void activeWallhack(bool active)
    {
        if (active)
        {
            foreach (Renderer render in listToRender)
            {
                render.material = _wallhack;
            }
        }
        else
        {
            foreach (Renderer render in listToRender)
            {
                render.material = _forceField;
            }
        }
    }

    private void FixedUpdate()
    {
        if (citizen.dead || citizen.afraid  || stopRoutine)
            return;
        // Routine
            if (round == 100)
            {
                angle += 180.0f;

                citizen.transform.rotation = Quaternion.AngleAxis(angle, citizen.transform.up); // rotate

                round = 0;
            }

            round++;


    }
	
	void afraidMovement()
    {
        citizen.transform.rotation = Quaternion.AngleAxis(randomAngle, citizen.transform.up);
        citizen.Forward(true, forward);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Floor")
        {
            randomAngle = Random.Range(0, 180);
            citizen.transform.rotation = Quaternion.AngleAxis(randomAngle, citizen.transform.up);
        }
    }
}
