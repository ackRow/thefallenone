using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour {

    // Use this for initialization
    public bool FirstPerson = true;
    /*
     * Permet de ne pas render la tête de perso
     * A seulement activer en solo et en vue fps
     * 
     */
	void Start () {

        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = !FirstPerson;
        }
    }
	
	// Update is called once per frame
	void Update () {


    }
}
