using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public Text GroundState;
    public Text SpeedText;
    public Text coordsText;
    public float x;
    public float y;
    public float z;

    //public Vector3 moveDirection = Vector3.zero;
    private Vector3 _moveDirection = Vector3.zero;
    private Rigidbody _body;
    public float Speed;
    private bool _isGrounded;

    private float nextTimeToFire = 0f;
    private int punchStateHash = Animator.StringToHash("Base Layer.Punching");
    private int gunStateHash = Animator.StringToHash("Base Layer.Idle_Gun");

    private int delay = 0;

    [SerializeField]
    Player player;

    //[SerializeField]
    private Animator animator;

    public Camera fpsCam;

    void Start()
    {
        animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _isGrounded = false;
    }

    void Update()
    {
        x = _body.transform.position.x;
        y = _body.transform.position.y;
        z = _body.transform.position.z;
        coordsText.text = "( " + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + " )";
        GroundState.text = "isGrounded: " + _isGrounded.ToString();
        SpeedText.text = "Speed: " + Speed.ToString();
        //print(_isGrounded);
        _moveDirection.z = Input.GetAxis("Horizontal");
        _moveDirection.x = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(1) & player.hasGun)
        {
            animator.SetBool("hasgun", !animator.GetBool("hasgun"));
        }

        if (Input.GetMouseButton(0))
        {
            if (!animator.GetBool("hasgun") && Time.time > nextTimeToFire)
            {
                animator.SetTrigger("Punching");
                Hit(player.punchDamage, player.punchRange);
                nextTimeToFire = Time.time + 1f / player.punchRate;
            }
            else if(animator.GetCurrentAnimatorStateInfo(0).fullPathHash == gunStateHash && Time.time >= nextTimeToFire)
            {
                animator.SetTrigger("Shooting");
                Hit(player.gunDamage, player.gunRange);
                nextTimeToFire = Time.time + 1f / player.gunFireRate;
            }
        }
            

        if (animator)
        {

            animator.SetFloat("Speed", Speed);
            animator.SetBool("isWalking", (_moveDirection.z * _moveDirection.z + _moveDirection.x * _moveDirection.x) > 0.2);
            
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {  // on cours
            Speed = player.running_speed;
        }
        else
        {
            Speed = player.walking_speed;
        }

        if (!_isGrounded)
        { //pendant qu'on est dans les air le mouvement est réduit
            Speed *= 0.6f;
            animator.SetBool("hasJumped", false);
        }

        //_body.velocity c'est la vélocité de l'objet (la vitesse)

        _moveDirection.z = Input.GetAxis("Horizontal");
        _moveDirection.x = Input.GetAxis("Vertical");

        //print(_body.velocity);



        if ((Input.GetKey(KeyCode.Space) || animator.GetBool("hasJumped")) && _isGrounded)
        {  //on saute
            //_body.isKinematic = false;
            animator.SetBool("hasJumped", true);
            //_isGrounded = false;
            delay++;

            if (delay == 7)
            {
                _isGrounded = false;
                //_body.isKinematic = false;
                _body.AddForce(new Vector3(0, player.JumpForce, 0), ForceMode.Impulse);

                delay = 0;
            }
        }

        _body.velocity = new Vector3(Vector3.Dot(transform.forward, _moveDirection * Speed), _body.velocity.y, Vector3.Dot(transform.right, _moveDirection * Speed));

        _body.isKinematic = _body.velocity == Vector3.zero && !(_isGrounded || animator.GetBool("hasJumped"));


        if(_body.transform.position.y < -20f)
        {
            _body.MovePosition(new Vector3(0, 0.06f, -3.6f));
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
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }

    }
}
