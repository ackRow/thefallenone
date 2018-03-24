using UnityEngine;
using UnityEngine.UI; // faut utiliser l'UI
using System.Collections;

public class Player : MonoBehaviour, ITarget
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

    public float health = 100;

    public bool hasGun = false;

    private Rigidbody _body;
    public GameObject gun;
    public GameObject head;

    private Slider playerHealth;

    private Animator animator;



    public Vector3 Position
    {    
        get { return _body.transform.position; }
    }
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        gun.GetComponent<Renderer>().enabled = hasGun;
        head.GetComponent<Renderer>().enabled = false; // On cache la tête du joueur (car vue FPS)

        playerHealth = FindObjectsOfType<Slider>()[0]; // On recupère le slider


    }

    // Update is called once per frame
    void Update () {
        
        playerHealth.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
            Die();
        else
            animator.SetTrigger("isHit"); // animation lorsqu'on est touché*/
    }

    public void Die()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        animator.SetTrigger("Dead2"); // On lance l'animation de mort

        yield return new WaitForSeconds(5); // delay


        // On relance l'animation Idle et on remet la vie à 100
        animator.Play("Idle", -1, 0f);
        //transform.position = spawnPoints[Random.Range(0, 4)].transform.position;
        health = 100f;
    }

}
