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
    private bool _isGrounded = true;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Speed = 5.0f;
        //_body.velocity c'est la vélocité de l'objet (la vitesse)
        // si la vitesse sur l'axe y est eviron egale a 0 alors ca veux dire que l'object est au sol
        _isGrounded = Mathf.Abs(_body.velocity.y) < 0.001f;
        _moveDirection.z = Input.GetAxis("Horizontal");
        _moveDirection.x = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {  //on saute
            _body.AddForce(Vector3.up * JumpForce);
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

        }
        // si la vitesse depasse speed on bloque l'application de la force
        if (Vector3.Magnitude(new Vector3(_body.velocity.x, 0, _body.velocity.z)) <= Speed)
        {
            _body.AddForce(transform.forward * 1000 * _moveDirection.x);
            _body.AddForce(transform.right * 1000 * _moveDirection.z);
        }


    }

    private void FixedUpdate()
    {
 
    }
}
