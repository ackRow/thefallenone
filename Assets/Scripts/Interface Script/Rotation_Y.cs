using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Y : MonoBehaviour {

    GameObject trophy1;
    GameObject trophy2;

    void Start () {
        trophy1 = GameObject.Find("Trophy1");
        trophy2 = GameObject.Find("Trophy2");
    }
	
	// Update is called once per frame
	void Update () {

        trophy1.transform.Rotate(Vector3.up * Time.deltaTime * 80f);
        trophy2.transform.Rotate(-Vector3.up * Time.deltaTime * 80f);
    }
}
