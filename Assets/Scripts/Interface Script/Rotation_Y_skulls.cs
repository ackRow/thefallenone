using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Y_skulls : MonoBehaviour {
   
    GameObject skull1;
    GameObject skull2;

    void Start()
    {
        skull1 = GameObject.Find("trophy_skull1");
        skull2 = GameObject.Find("trophy_skull2");
    }

    // Update is called once per frame
    void Update()
    {

        skull1.transform.Rotate(Vector3.up * Time.deltaTime * 80f);
        skull2.transform.Rotate(-Vector3.up * Time.deltaTime * 80f);
    }
}
