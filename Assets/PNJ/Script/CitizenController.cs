using System.Collections.Generic;
using UnityEngine;

public class CitizenController : MonoBehaviour
{

    Citizen citizen;

    public List<Renderer> listToRender;
    public Material _forceField, _wallhack;

    //bool afraid = false;
    public float angle;

    //public Human target;
    Vector3 forward = new Vector3(1.0f, 0, 0);

    int round = 50;

    public Human target; // for wallhack and scared
    bool isWallhack = false;

    // Use this for initialization
    void Start()
    {
        citizen = GetComponent<Citizen>();
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
                citizen.Forward(false, Vector3.zero);
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
        if (citizen.dead || citizen.afraid)
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
}
