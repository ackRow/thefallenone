using System.Collections.Generic;
using UnityEngine;

public class CitizenController : MonoBehaviour
{

    Citizen citizen;

    public List<Renderer> listToRender;
    public Material _forceField, _wallhack;

    public float angle;
    float randomAngle;

    public bool walking = false;
    public bool running = false;
    //public Human target;
    Vector3 forward = new Vector3(1.0f, 0, 0);

    int round = 0;

    public Human target; // for scared

    // Use this for initialization
    void Start()
    {
        citizen = GetComponent<Citizen>();
        randomAngle = Random.Range(-30, 30);
        //bot.Scope();
    }

    // Update is called once per frame
    void Update()
    {

        if (running)
        {

            citizen.Forward(true, forward);
        }
        else if (target is Player)
        {
            Player p = (Player)target;

            if (p.hasShotCitizen)
            {
                citizen.afraid = true;
                //citizen.Forward(false, Vector3.zero);
            }
                if (walking)
                    citizen.Forward(false, forward);
                else
                    citizen.Forward(false, Vector3.zero);

        }
        else
        {
            citizen.Forward(false, Vector3.zero);
        }
    }

    private void FixedUpdate()
    {
        if (citizen.dead)
            return;

        if (walking)
        {
            // Routine
            if (round == 700)
            {
                /*angle += 180.0f;

                citizen.transform.rotation = Quaternion.AngleAxis(angle, citizen.transform.up); // rotate

                round = 0;*/
                walking = false;
            }
            round++;
        }
        else if (running)
        {
            if (round == 300)
            {
                angle += 180.0f;

                citizen.transform.rotation = Quaternion.AngleAxis(angle, citizen.transform.up); // rotate

                round = 0;
                walking = false;
            }
            round++;
        }
        


    }

void afraidMovement()
    {
        citizen.transform.rotation = Quaternion.AngleAxis(randomAngle, citizen.transform.up);
        citizen.Forward(true, forward);
    }
}
