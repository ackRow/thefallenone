using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNico : Player
{
    public float runSpeed = 10.0f;
    public float gravity = 9.81f;
    public Vector3 moveDirection = Vector3.zero;
    public float JumpForce = 12.0f;
    private Vector3 _moveDirection = Vector3.zero;
    private Rigidbody _body;
    public float Speed;
    private bool _isGrounded;

    private int delay = 0;

    [SerializeField]
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _isGrounded = false;
    }

    void Update()
    {
        //print(_isGrounded);
        _moveDirection.z = Input.GetAxis("Horizontal");
        _moveDirection.x = Input.GetAxis("Vertical");

        //if (Input.GetMouseButtonDown(0))


        if (animator)
        {

            animator.SetFloat("Speed", Speed);
            animator.SetBool("isWalking", (_moveDirection.z * _moveDirection.z + _moveDirection.x * _moveDirection.x) > 0.2);
            animator.SetBool("isPunching", Input.GetMouseButtonDown(0));
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {  // on cours
            Speed = 10.0f;
        }
        else {
             Speed = 4.0f;
        }

        if (!_isGrounded)
        { //pendant qu'on est dans les air le mouvement est réduit
            Speed *= 0.6f;
            animator.SetBool("hasJumped", false);
        }

        //_body.velocity c'est la vélocité de l'objet (la vitesse)

        _moveDirection.z = Input.GetAxis("Horizontal");
        _moveDirection.x = Input.GetAxis("Vertical");

        print(_moveDirection);

        

       if ( (Input.GetKey(KeyCode.Space) ||  animator.GetBool("hasJumped") ) && _isGrounded)
        {  //on saute
            
            animator.SetBool("hasJumped", true);

            delay++;

            if (delay > 7)
            {
                _body.AddForce(new Vector3(0, 200, 0), ForceMode.Impulse); ;
                _isGrounded = false;
                delay = 0;
            }
        }
        _body.velocity = new Vector3(Vector3.Dot(transform.forward, _moveDirection * Speed), _body.velocity.y, Vector3.Dot(transform.right, _moveDirection * Speed));

        /*if (!_isGrounded)
        { //pendant qu'on est dans les air le mouvement est réduit
            speed = 1.0f;
        }
        // si la vitesse depasse speed on bloque l'application de la force
        if (Vector3.Magnitude(new Vector3(_body.velocity.x, 0, _body.velocity.z)) <= Speed)
        {
            _body.AddForce(transform.forward * 1000 * _moveDirection.x);
            _body.AddForce(transform.right * 1000 * _moveDirection.z);
        }*/
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = true;
        }
        if(collision.gameObject.tag == "jumpg")
        {
            _body.AddForce(new Vector3(0, 600, 0), ForceMode.Impulse); ;
            _isGrounded = false;
        }
    }
}
