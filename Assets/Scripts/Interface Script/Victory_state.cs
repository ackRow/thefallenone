using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory_state : MonoBehaviour {

    GameObject skulls;
    GameObject trophies;
    GameObject win;
    GameObject loose;

    private void Start()
    {
        skulls = GameObject.Find("Rotation_skulls");
        skulls.SetActive(false);

        trophies = GameObject.Find("Rotation_trophies");
        trophies.SetActive(false);

        win = GameObject.Find("win");
        win.SetActive(false);

        loose = GameObject.Find("loose");
        loose.SetActive(false);
    }

    public void State(bool winning)
    {
        if (winning)
        {
            trophies.SetActive(true);
            win.SetActive(true);
        }
        else
        {
            skulls.SetActive(true);
            loose.SetActive(true);
        }
    }
}
