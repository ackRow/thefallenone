using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI; // faut utiliser l'UI
using UnityEngine.SceneManagement;

public class Player_Net : NetworkBehaviour, ITarget_Net
{
    /* Variable Definition */

    public float walking_speed = 4.0f;
    public float running_speed = 8.0f;

    public float JumpForce = 650.0f;

    public bool hasGun = true; // Possède une arme   

    /* Default value for gun and punch */

    public float gunDamage = 25f;
    public float gunRange = 20f;
    public float gunFireBuff = 0.6f;

    public float punchDamage = 50f;
    public float punchRange = 1f;
    public float punchingBuff = 0.95f;

    /* Component */
    protected Rigidbody _body;
    protected Animator _animator;
    protected CapsuleCollider _capsCollider;

    protected float _capsHeight;
    protected Vector3 _capsCenter;

    /* Movement variable */

    protected Vector3 _moveDirection = Vector3.zero;

    protected float jumpMult = 1.0f;
    protected float Speed = 0.0f;

    public bool crouching = false;
    public bool canStand = true;
    protected bool walking = false;
    protected bool jumping = false;

    [SyncVar]
    public bool dead = false;

    protected float jumpDelay = 0.0f; // sync jump animation

    protected bool isScoping = false; // Est entrain de viser
    protected float nextTimeToAttack = 0f;
    protected bool attacking = false;


    protected bool hasHitTarget = false;
    protected bool isHit = false;

    /* Local variable */
    private Slider playerHealth;

    private Animator ArmAnimator; // les mains en vue fps

    private PlayerController_Net controller;

    public bool FPSView = true;

    private GameObject gun;
    public GameObject head;

    public GameObject[] ArmExt; // last object is the gun

    public GameObject[] ArmFPS;


    /* Online */
    public NetworkStartPosition[] spawnPoints;

    public string username;

    [SyncVar]
    public float health = 100;

    public bool pause = true;


    public Vector3 Position
    {    
        get { return _body.transform.position; }
    }


    /* --- Init Function --- */


    void Start () {

        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _capsCollider = GetComponent<CapsuleCollider>();
        _capsHeight = _capsCollider.height;
        _capsCenter = _capsCollider.center;

        controller = GetComponent<PlayerController_Net>();
        ArmAnimator = GetComponentsInChildren<Animator>()[1];

        spawnPoints = FindObjectsOfType<NetworkStartPosition>();

        if (!isLocalPlayer)
        {
            GetComponentInChildren<moveLook_Online>().local = false; // On desactive la camera fps des autres joueurs
            head.GetComponent<Renderer>().enabled = true; // On affiche la tête des autres joueurs
            FPSView = false;
        }

        if (FPSView)
        {
            head.GetComponent<Renderer>().enabled = false; // On cache la tête du joueur (car vue FPS)
            foreach (var obj in ArmExt)
            {
                obj.GetComponent<Renderer>().enabled = false;
            }

            gun = ArmFPS[ArmFPS.Length - 1];

            if (StaticInfo.Username != "")
                username = StaticInfo.Username;
            else
                username = "Online Player";
        }
        else
        {
            head.GetComponent<Renderer>().enabled = true; // On cache la tête du joueur (car vue FPS)
            foreach (var obj in ArmFPS)
            {
                obj.GetComponent<Renderer>().enabled = false;
            }

            gun = ArmExt[ArmExt.Length - 1];
        }

    }

    /* Helper Function */
    protected void Animate(Animator _animator)
    {
        if (_animator && !dead)
        {

            _animator.SetFloat("Speed", Speed); // La variable speed va modifier la vitesse des animations de mouvements



            if (hasGun)
                _animator.SetFloat("Scope", isScoping ? 1.0f : 0.0f);

            if (attacking)
            {
                //crouching = false;
                attacking = false;
                _animator.SetTrigger("Attack");

            }

            _animator.SetFloat("Crouch", crouching ? 1.0f : 0.0f);

            _animator.SetBool("Walk", walking && !jumping);

            _animator.SetBool("Jump", jumping);

        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0, 0.9f, 0), -Vector3.up, 1f);
    }


    /* Affichage en local */

    [GUITarget]
    void GUIHealthSync() // On synchronise avec le GUI du client pour update la barre de vie en temps réel
    {
        playerHealth = FindObjectsOfType<Slider>()[0]; // On recupère le slider
        playerHealth.value = health;
    }

    public void adjustCollider(bool crouching)
    {
        if (crouching)
        {
            _capsCollider.height = _capsCollider.height / 2f;
            _capsCollider.center = _capsCollider.center / 2f;
        }
        else
        {
            _capsCollider.height = _capsHeight;
            _capsCollider.center = _capsCenter;
        }
    }


    /* S'execute sur le serveur */

    [Command] 
    void CmdAttackOnline(Vector3 _position, Vector3 _direction, bool useGun, string Token)
    {
        RaycastHit hit;
        // On tir un rayon depuis le centre de la camera du joueur jusqu'à une certaine distance
        if (Physics.Raycast(_position, _direction, out hit, (useGun ? gunRange : punchRange)))
        {
            ITarget_Net target = hit.transform.GetComponent<ITarget_Net>();
            if (target != null) // Si un joueur est touché
            {
                //RpcPlayHitSound();
                hasHitTarget = true;
                target.TakeDamage((useGun ? gunDamage : punchDamage), Token); // La target va perdre de la vie
            }
        }
    }

    [Command]
    public void CmdTakeDamage(float amount, string caller_token) // run server side
    {
        if (dead)
            return;

        health -= amount;
        if (health <= 0f)
        {
            updateStat(caller_token, StaticInfo.Stat.kill);
            dead = true;
            Die();
        }
        else
            _animator.SetTrigger("isHit"); // animation lorsqu'on est touché
    }

    [Command] // La fonction est synchronisé avec le server
    void CmdRespawn()
    {
        health = 100f;
        dead = false;
    }

    [Command]
    public void CmdHeal(float val)
    {
        if (dead)
            return;

        health += val;
        if (health > 100.0f)
            health = 100.0f;

    }

    /* S'execute sur le client */

    /*[ClientRpc]
    void RpcPlayHitSound() // On lance le son de hit chez les clients
    {
        if (!myAudio.isPlaying)
        {
            myAudio.PlayOneShot(hitSound);
        }
    }*/


    [ClientRpc] // La fonction est synchronisé avec le client
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Debug.Log(StaticInfo.Token + "dead");
            updateStat(StaticInfo.Token, StaticInfo.Stat.death);
            StartCoroutine(Respawn()); // Permet de mettre un delay dans la fonction
        }
    }

    IEnumerator Respawn()
    {
        _animator.Play("Die", -1, 0f); // On lance l'animation de mort

        yield return new WaitForSeconds(5); // delay


        // On relance l'animation Idle et on remet la vie à 100
        _animator.Play("Idle", -1, 0f);
        _body.MovePosition(spawnPoints[Random.Range(0, 4)].transform.position);

        CmdRespawn();
    }



    /* --- Update Function --- */


    void Update () {

        /* Online */
        gun.GetComponent<Renderer>().enabled = hasGun && isScoping; //  Hide and Show gun
        //dead = health <= 0f;

        if (!isLocalPlayer || pause)
            return;

        /* Local State */        
        canStand = !Physics.Raycast(transform.position + new Vector3(0, _capsCollider.height, 0), Vector3.up, _capsCollider.height);
        if (!IsGrounded() && crouching)
        {
            Stand();
        }

        /* Animation FPS Arm */
        Animate(ArmAnimator);
        Animate(_animator);

        /* Sound */

        /* UI */
        GUIHealthSync();
       
    }

    protected void FixedUpdate() // Moving, Physic Stuff
    {
        if (dead || !isLocalPlayer || pause)
            return;


        // On modifie directement la velocity du personnage pour les axes X et Z afin de le rendre plus controlable
        // Au lieu d'utiliser AddForce
        _body.velocity = new Vector3(Vector3.Dot(transform.forward, _moveDirection * Speed), _body.velocity.y, Vector3.Dot(transform.right, _moveDirection * Speed));

        if (jumping && Time.time > jumpDelay)  // Le delay permet de synchroniser le saut avec l'animation
        {
            // Pour rendre le saut plus réaliste, on utilise AddForce en mode Impulse
            _body.AddForce(new Vector3(0, JumpForce * jumpMult, 0), ForceMode.Impulse);
            jumpMult = 1.0f;
            jumping = false; // a présent, le joueur n'est plus entrain de sauter mais entrain de retomber
        }

        // Si le joueur tombe dans le vide, il respawn
        if (_body.transform.position.y < -20f)
        {
            _body.MovePosition(spawnPoints[Random.Range(0, 4)].transform.position);
        }
    }


    /* --- Action Function (called by the controller) --- */

    
    public void Attack(Vector3 _position, Vector3 _direction)
    {
        if (!isLocalPlayer)
            return;

        if (Time.time > nextTimeToAttack)
        {
            if (crouching)
                Stand();
            attacking = true;
            nextTimeToAttack = Time.time + (isScoping ? gunFireBuff : punchingBuff);
            //Playing gun shot sound

            CmdAttackOnline(_position, _direction, isScoping, StaticInfo.Token);
        }
    }


    public void Stand()
    {
        // Override this in child
        if (canStand)
        {
            crouching = false;
            if (!crouching)
                controller.adjustingCamera(false);
            adjustCollider(crouching);
        }

    }

    public void Jump()
    {

        if (crouching)
        {
            Stand();
            return;
        }

        if (!jumping && IsGrounded())
        {
            walking = false;
            jumping = true;


            jumpDelay = Time.time + 0.15f;
        }

    }

    public bool Crouch()
    {
        crouching = !crouching;
        adjustCollider(crouching);
        return crouching;
    }

    public void Scope()
    {
        if (hasGun)
            isScoping = !isScoping;
        if (crouching)
            Stand();
    }

    public void Forward(bool run, Vector3 _direction)
    {
        if (!isScoping && IsGrounded())
            Speed = run ? running_speed : walking_speed;
        else
            Speed = walking_speed;  // si le joueur est en l'air ou s'il vise, il ne peut pas courir

        if (crouching && run)
            Stand();

        walking = (_moveDirection.z * _moveDirection.z + _moveDirection.x * _moveDirection.x) > 0.2;

        _moveDirection = _direction;
    }


    /* --- Event Function (or callback) --- */


    public void TakeDamage(float damage, string caller_token)
    {
        CmdTakeDamage(damage, caller_token);
    }

    


    private void Die()
    {
        RpcRespawn();
    }

    public void Win()
    {
       // Debug.Log(StaticInfo.Token);

        if (isLocalPlayer)
        {
            updateStat(StaticInfo.Token, StaticInfo.Stat.win);
        }

        updateStat(StaticInfo.Token, StaticInfo.Stat.play);

        
        Cursor.lockState = CursorLockMode.None;
        (GameObject.Find("PauseScript").GetComponent<PauseMenuScript>()).isActive = true;
        try
        {
            NetworkManager.singleton.StopHost();
        }
        catch { }
        SceneManager.LoadScene("Menu");
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "jumpg")
        {
            Jump();
            jumpMult = 2.0f;
        }
    }
    public void updateStat(string Token, StaticInfo.Stat stat)
    {
        if (Token == "")
            return;

        WWWForm form = new WWWForm();
        form.AddField("token", Token);
        form.AddField(stat.ToString(), 1);

        WWW www = new WWW("https://thefallen.one/sync/userInfo.php", form);

        StartCoroutine(WaitForRequest<UserData>(www));
    }

    IEnumerator WaitForRequest<T>(WWW data)
    {
        yield return data; // Wait until the download is done
        if (data.error != null)
        {
            Debug.Log("There was an error sending request: " + data.error);
        }
        else
        {
            T jsonClass = JsonUtility.FromJson<T>(data.text);
            ((IJsonClass)jsonClass).ProcessData(this);
        }
    }


}
