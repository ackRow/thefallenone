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

    public bool FPSView = true;

    private Rigidbody _body;
    public GameObject gun;
    public GameObject head;
    public GameObject[] Arm;
    public GameObject Stomach;

    private Slider playerHealth;

    private Animator animator;

    public Animator ArmAnimator;



    public Vector3 Position
    {    
        get { return _body.transform.position; }
    }
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        ArmAnimator = GetComponentsInChildren<Animator>()[1];

        //gun.GetComponent<Renderer>().enabled = hasGun;

        if (FPSView)
        {
            gun.GetComponent<Renderer>().enabled = false;
            head.GetComponent<Renderer>().enabled = false; // On cache la tête du joueur (car vue FPS)
            foreach (var obj in Arm)
            {
                obj.GetComponent<Renderer>().enabled = false;
            }
        }

        

        playerHealth = FindObjectsOfType<Slider>()[0]; // On recupère le slider


    }

    // Update is called once per frame
    void Update () {
        
        playerHealth.value = health;

        gun.GetComponent<Renderer>().enabled = hasGun;

        animator.SetBool("hasgun", hasGun);
        ArmAnimator.SetBool("hasGun", hasGun);
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
