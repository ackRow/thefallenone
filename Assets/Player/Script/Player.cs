using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; // faut utiliser l'UI

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
    public GameObject head;

    public Target target;

    private Slider playerHealth;


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
            return;
        }
        playerHealth = FindObjectsOfType<Slider>()[0];
        head.GetComponent<Renderer>().enabled = false;
        //Debug.Log(Object.FindObjectsOfType<Slider>()[0]);


    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        playerHealth.value = target.health;

    }
}
