using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; // faut utiliser l'UI

public class Player_Net : NetworkBehaviour
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

    public Target_Net target;

    private Slider playerHealth;


    public Vector3 Position
    {    
        get { return _body.transform.position; }
    }
    // Use this for initialization
    void Start () {
        
        gun.GetComponent<Renderer>().enabled = hasGun;
        target = GetComponent<Target_Net>();
        head.GetComponent<Renderer>().enabled = false; // On cache la tête du joueur (car vue FPS)

        if (!isLocalPlayer)
        {
            GetComponentInChildren<moveLook_Net>().local = false; // On desactive la camera fps des autres joueurs
            head.GetComponent<Renderer>().enabled = true; // On affiche la tête des autres joueurs
            return;
        }


    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        GUIHealthSync();
    }

    [GUITarget]
    void GUIHealthSync() // On synchronise avec le GUI du client pour update la barre de vie en temps réel
    {
        playerHealth = FindObjectsOfType<Slider>()[0]; // On recupère le slider
        playerHealth.value = target.health;
    }
}
