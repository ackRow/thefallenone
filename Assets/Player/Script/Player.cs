using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    public float walking_speed = 4.0f;
    public float running_speed = 8.0f;

    public float gunDamage = 25f;
    public float gunRange = 20f;
    public float gunFireBuff = 0.267f;
    public float punchDamage = 10f;
    public float punchRange = 0.1f;
    public float punchingBuff = 1f;
    public float JumpForce = 650.0f;

    public bool hasGun = false;

    private Rigidbody _body;
    public GameObject gun;

    public Target target;


    public Vector3 Position
    {    
        get { return _body.transform.position; }
    }
    // Use this for initialization
    void Start () {
        
        gun.GetComponent<Renderer>().enabled = hasGun;
        target = GetComponent<Target>();

        if (!isLocalPlayer)
        {
            GetComponentInChildren<moveLook>().local = false;
        }

    }
	
	// Update is called once per frame
	void Update () {
		

	}
}
