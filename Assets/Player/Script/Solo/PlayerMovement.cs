using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 _moveDirection = Vector3.zero;
    private Rigidbody _body;
    public float Speed = 0;
    private bool _isGrounded;

    private float nextTimeToFire = 0f;
    private int punchStateHash = Animator.StringToHash("Base Layer.Punching");
    private int gunStateHash = Animator.StringToHash("Base Layer.Idle_Gun");

    private int delay = 0;

    Player player;

    bool walking = false;

    private Animator animator;

    private Camera fpsCam;

    public AudioClip gunShot;
    public AudioClip hitSound;
    public AudioClip walkSound;
    public AudioClip jumpSound;

    public AudioSource myAudio;

    void Start()
    {
        // On recupère les différentes Component de l'objet unity
        
        animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        player = GetComponent<Player>();

        myAudio = GetComponent<AudioSource>();
        myAudio.clip = gunShot;

        if (Camera.main != null)
        {
            fpsCam = GetComponentInChildren<Camera>();
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        _isGrounded = false;
    }

    void Update()
    {

        if (player.health <= 0) // Le joueur ne peut pas controller les autres personnages en multi
        {                                                // Il ne peut pas non plus se déplacer s'il est mort (health <= 0)
            return;
        }

        _moveDirection.z = Input.GetAxis("Horizontal");
        _moveDirection.x = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(1)) // Le joueur peut mettre en joue son arme (clic droit)
        {
            player.hasGun = !player.hasGun;
        }

        if (Input.GetMouseButton(0)) // clic gauche
        {
            if (!animator.GetBool("hasgun") && Time.time > nextTimeToFire) // S'il reste appuyer, il y a un delay 
            { // Si le joueur n'a pas d'arme ou s'il n'a pas mis son arme en joue, il donne un coup de point
                animator.SetTrigger("Punching");
                Hit(player.punchDamage, player.punchRange); // CmdHit est une fonction Server
                nextTimeToFire = Time.time + player.punchingBuff;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == gunStateHash && Time.time >= nextTimeToFire) // cadence de tir
            {
                // S'il a une arme, alors il peut tirer
                animator.SetTrigger("Shooting");
                myAudio.PlayOneShot(gunShot);
                Hit(player.gunDamage, player.gunRange);
                nextTimeToFire = Time.time + player.gunFireBuff;
            }
        }


        if (animator)
        {

            animator.SetFloat("Speed", Speed); // La variable speed va modifier la vitesse des animations de mouvements
            animator.SetBool("isWalking", (_moveDirection.z * _moveDirection.z + _moveDirection.x * _moveDirection.x) > 0.2 && _isGrounded);
            
            // On joue le son de pas
            if (walking == false && animator.GetBool("isWalking"))
            {
                walking = true;
                myAudio.clip = walkSound;
                myAudio.loop = true;
                myAudio.Play();
            }
            else if (walking == true && !animator.GetBool("isWalking"))
            {
                walking = false;
                myAudio.Stop();
            }


        }
    }

    private void FixedUpdate()
    {

        if (player.health <= 0)
            return;

        // On change la vitesse de déplacement du joueur en fonction de son état (en l'air, entrain de courir)
        if (_isGrounded || Speed == 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && !animator.GetBool("hasgun"))
                Speed = player.running_speed;
            else
                Speed = player.walking_speed;

        }

        if (!_isGrounded)
        { //pendant qu'on est dans les air on desactive l'animation de prise d'élan du saut

            animator.SetBool("hasJumped", false);
        }

        // On récupère les entrées clavier
        _moveDirection.z = Input.GetAxis("Horizontal");
        _moveDirection.x = Input.GetAxis("Vertical");

        if ((Input.GetKey(KeyCode.Space) || animator.GetBool("hasJumped")) && _isGrounded)
        {  //on saute
        
            animator.SetBool("hasJumped", true); // on démarre l'animation de prise d'élan pour le saut

            delay++;

            walking = false;
            myAudio.Stop(); // on arrête les bruits de pas

            if (delay == 7) // Le delay permet de synchroniser le saut avec l'animation
            {
                _isGrounded = false;

                myAudio.PlayOneShot(jumpSound, 0.3f); // bruit du saut

                // Pour rendre le saut plus réaliste, on utilise AddForce en mode Impulse
                _body.AddForce(new Vector3(0, player.JumpForce, 0), ForceMode.Impulse);
                Speed *= 0.6f; // la vitesse du joueur est réduite dans les airs
                delay = 0;
            }
        }

        // On modifie directement la velocity du personnage pour les axes X et Z afin de le rendre plus controlable
        // Au lieu d'utiliser AddForce
        _body.velocity = new Vector3(Vector3.Dot(transform.forward, _moveDirection * Speed), _body.velocity.y, Vector3.Dot(transform.right, _moveDirection * Speed));

        // On désactive la kinetic du rigidbody lorsque celui ci est à l'arrêt (pour qu'il ne glisse pas dans les pentes par exemple)
        _body.isKinematic = _body.velocity == Vector3.zero && !(_isGrounded || animator.GetBool("hasJumped"));

        // Si le joueur tombe dans le vide, il respawn
        if (_body.transform.position.y < -20f)
        {
            _body.MovePosition(new Vector3(0,0,0));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = true;
        }
        if (collision.gameObject.tag == "jumpg")
        {
            _body.isKinematic = false;
            _body.AddForce(new Vector3(0, 1200, 0), ForceMode.Impulse); ;
            _isGrounded = false;

        }
    }

    void Hit(float damage, float range)
    {
        RaycastHit hit;
        // On tir un rayon depuis le centre de la camera du joueur jusqu'à une certaine distance
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            ITarget target = hit.transform.GetComponent<ITarget>();
            if (target != null) // Si un joueur est touché
            {
                if (!myAudio.isPlaying)
                {
                    myAudio.PlayOneShot(hitSound);
                }

                target.TakeDamage(damage); // La target va perdre de la vie
            }
        }

    }
}
