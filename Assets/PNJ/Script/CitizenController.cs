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
    //public Human target;
    Vector3 forward = new Vector3(1.0f, 0, 0);

    int round = 50;

    public Human target; // for scared

    // Use this for initialization
    void Start()
    {
        citizen = GetComponent<Citizen>();
        randomAngle = Random.Range(0, 180);
        //bot.Scope();
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
                //citizen.Forward(false, Vector3.zero);
            }
            else
            {
                if (walking)
                    citizen.Forward(false, forward);
                else
                    citizen.Forward(false, Vector3.zero);
            }

        }
        else
        {
            citizen.Forward(false, Vector3.zero);
        }
    }

    private void FixedUpdate()
    {
        if (citizen.dead || !walking)
            return;
        // Routine
        if (round == 600)
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
}
