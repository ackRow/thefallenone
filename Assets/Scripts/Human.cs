﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Classe basique d'un humain
 * 
 * (déplacements, animations, attaque, mort)
 * 
 */

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public abstract class Human : MonoBehaviour, ITarget
{

    /* Variable Definition */

    public string username = "Human";

    public float health = 100;

    public float walking_speed = 4.0f;
    public float running_speed = 8.0f;

    public float JumpForce = 650.0f;

    public bool hasGun = false; // Possède une arme   

    /* Default value for gun and punch */

    public Light _flashGun;
    public GameObject GunSmoke;
    public ParticleSystem shotParticle;

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
    public AudioSource myAudio;

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

    public bool dead = false;

    protected float jumpDelay = 0.0f; // sync jump animation

    protected bool isScoping = false; // Est entrain de viser
    protected float nextTimeToAttack = 0f;
    protected bool attacking = false;


    protected bool hasHitTarget = false;
    protected bool isHit = false;

    /* Sound */

    public AudioClip punchSound;
    public AudioClip gunShotSound;
    public AudioClip jumpSound;
    public AudioClip stepSound;
    public AudioClip hitSound;

    public AudioClip switchSound; // son qd le genre joueur change du gun au point et inversement


    public Vector3 Position
    {
        get { return _body.transform.position; }
    }

    protected virtual void Start () {

        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _capsCollider = GetComponent<CapsuleCollider>();
        _capsHeight = _capsCollider.height;
        _capsCenter = _capsCollider.center;
    }


    protected void Animate(Animator _animator)
    {
        if (_animator && !dead)
        {

            _animator.SetFloat("Speed", Speed); // La variable speed va modifier la vitesse des animations de mouvements

            if (hasGun)
                _animator.SetFloat("Scope", isScoping ? 1.0f : 0.0f);     

            _animator.SetFloat("Crouch", crouching ? 1.0f : 0.0f);

            _animator.SetBool("Walk", walking && !jumping);

            _animator.SetBool("Jump", jumping);

            if (attacking)
            {             
                _animator.SetTrigger("Attack");
            }

        }
    }

    public void PlaySound(AudioClip sound, bool loop, float volume)
    {
        if (myAudio != null && sound != null)
        {
            myAudio.clip = sound;
            myAudio.volume = volume;
            myAudio.loop = loop;
            myAudio.Play();
        }
    }

    protected void Sound()
    {

        if (attacking)
            PlaySound(isScoping ? gunShotSound : punchSound, false, 0.5f);

        if(jumping)
            PlaySound(jumpSound, false, 0.5f);

        else if(walking)
            PlaySound(stepSound, false, 0.7f);

        if(hasHitTarget)
            PlaySound(hitSound, false, 0.7f);
    }

   

    protected virtual void Update() // Animating, playing sounds
    {
        /* State */
        canStand = !Physics.Raycast(transform.position + new Vector3(0, _capsCollider.height, 0), Vector3.up, _capsCollider.height);

        if (!IsGrounded() && crouching)
        {
            Stand();
        }

        /* Sound */
        Sound();

        /* Animation */
        Animate(_animator);

        /*State*/
        attacking = false;
        hasHitTarget = false;
    }

    protected virtual void FixedUpdate() // Moving, Physic Stuff
    {
        // Debug.DrawRay(transform.position + new Vector3(0, _capsCollider.height, 0), Vector3.up, Color.red, 0.1f, true);
       

        if (dead)
            return;


        // On modifie directement la velocity du personnage pour les axes X et Z afin de le rendre plus controlable
        // Au lieu d'utiliser AddForce
        _body.velocity = new Vector3(Vector3.Dot(transform.forward, _moveDirection * Speed), _body.velocity.y, Vector3.Dot(transform.right, _moveDirection * Speed));

        if (jumping && Time.time > jumpDelay)  // Le delay permet de synchroniser le saut avec l'animation
        {
            // Pour rendre le saut plus réaliste, on utilise AddForce en mode Impulse
            _body.AddForce(new Vector3(0, JumpForce* jumpMult, 0), ForceMode.Impulse);
            jumpMult = 1.0f;
            jumping = false; // a présent, le joueur n'est plus entrain de sauter mais entrain de retomber
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

    public void Scope()
    {
        if (hasGun)
        {
            isScoping = !isScoping;
            PlaySound(switchSound, false, 0.5f);
        }    

        if (crouching)
            Stand();
    }

    public void Attack(Vector3 _position, Vector3 _direction)
    {
        if (Time.time > nextTimeToAttack)
        {
            if (crouching)
                Stand();

            nextTimeToAttack = Time.time + (isScoping ? gunFireBuff : punchingBuff);
            attacking = true;
            RaycastHit hit;

            if (isScoping && _flashGun != null)
                StartCoroutine(FlashGun());

            // Debug.Log("Attack!");
            // On tir un rayon depuis le centre de la camera du joueur jusqu'à une certaine distance
            if (Physics.Raycast(_position, _direction, out hit, (isScoping ? gunRange : punchRange)))
            {
                if (isScoping && GunSmoke != null)
                {
                    Instantiate(GunSmoke).transform.position = hit.point;
                    shotParticle.Play();
                }
                ITarget target = hit.transform.GetComponent<ITarget>();
                if (target != null) // Si un joueur est touché
                {
                    
                    hasHitTarget = true;
                    target.TakeDamage((isScoping ? gunDamage : punchDamage), this); // La target va perdre de la vie
                }
            }
            
        }
    }

    IEnumerator FlashGun()
    {
        _flashGun.enabled = true;
        yield return new WaitForSeconds(0.05f); // delay

        _flashGun.enabled = false;
    }

    public void TakeDamage(float damage, Human caller)
    {
        if (dead)
            return;

        health -= damage;
        if (health <= 0f)
        {
            Die();
            if (caller != null)
            {
                Debug.Log(caller.username + " killed " + username);
                /*if (caller is Player)  kill stat online only
                {
                    ((Player)caller).updateStat(StaticInfo.Stat.kill);
                }*/
            }
            else
                Debug.Log(username + " died");
        }
            
        else
            _animator.SetTrigger("isHit"); // animation lorsqu'on est touché
    }

    public virtual void Die()
    {
        _animator.Play("Die", -1, 0f);
        dead = true;
        
    }

    public virtual void Stand()
    {

        // Override this in child
        if (canStand)
        {
            crouching = false;
            adjustCollider(crouching);
        }
        
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "jumpg")
        {
            jumpMult = 2.0f;
            Jump();     
        }else
            jumpMult = 1.0f;


    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "aie")
        {
            TakeDamage(1, null);
        }
    }

    protected bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0, 0.9f, 0), -Vector3.up, 1f);
    }
}
