using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{
    public float runSpeed = 10.0f;
    public float gravity = 9.81f;
    public Vector3 moveDirection = Vector3.zero;
    public float JumpForce = 400.0f;
    private Vector3 _moveDirection = Vector3.zero;
    private Rigidbody _body;
    public float Speed;
    private bool _isGrounded;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _isGrounded = false;
    }

    void Update()
    {
        print(_isGrounded);
    }

    private void FixedUpdate()
    {
        Speed = 5.0f;

        //_body.velocity c'est la vélocité de l'objet (la vitesse)

        _moveDirection.z = Input.GetAxis("Horizontal");
        _moveDirection.x = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {  //on saute
            _body.AddForce(new Vector3(0, 400, 0), ForceMode.Impulse);
            _isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        { // on se baisse
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {  // on cours
            Speed = 10.0f;
        }

        if (!_isGrounded)
        { //pendant qu'on est dans les air le mouvement est réduit
            speed = 0.5f;
        }
        // si la vitesse depasse speed on bloque l'application de la force
        if (Vector3.Magnitude(new Vector3(_body.velocity.x, 0, _body.velocity.z)) <= Speed)
        {
            _body.AddForce(transform.forward * 1000 * _moveDirection.x);
            _body.AddForce(transform.right * 1000 * _moveDirection.z);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = true;
        }
    }
}
