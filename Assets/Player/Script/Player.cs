using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float walking_speed = 4.0f;
    public float running_speed = 8.0f;

    public float gunDamage = 25f;
    public float gunRange = 20f;
    public float gunFireRate = 15f;
    public float punchDamage = 10f;
    public float punchRange = 0.1f;
    public float punchRate = 1f;
    public float JumpForce = 650.0f;

    public bool hasGun = false;

    private Rigidbody _body;
    public GameObject gun;


    public Vector3 Position
    {    
        get { return _body.transform.position; }
    }
    // Use this for initialization
    void Start () {
        gun.GetComponent<Renderer>().enabled = hasGun;
	}
	
	// Update is called once per frame
	void Update () {
		

	}
}
