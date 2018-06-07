using System.Collections.Generic;
using UnityEngine;

public class CitizenController : MonoBehaviour
{

    Citizen citizen;

    public List<Renderer> listToRender;
    public Material _forceField, _wallhack;

    bool afraid = false;
    float angle;

    //public Human target;
    Vector3 forward = new Vector3(1.0f, 0, 0);

    int round = 50;

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

        if (afraid)
            citizen.Forward(false, Vector3.zero);
        else
            citizen.Forward(false, forward);
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
        // Routine
            if (round == 100)
            {
                angle += 180.0f;

                citizen.transform.rotation = Quaternion.AngleAxis(angle, citizen.transform.up); // rotate

                round = 0;
            }

            round++;


    }
}
