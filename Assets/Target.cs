using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Target : NetworkBehaviour
{
    [SyncVar]
    public float health = 100f;

    public NetworkStartPosition[] spawnPoints;

    Animator animator;
    NetworkAnimator net_animator;

    //public AudioSource myAudio;

    void Start()
    {
        animator = GetComponent<Animator>();
        net_animator = GetComponent<NetworkAnimator>();
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();

        //myAudio = GetComponent<AudioSource>();
        //myAudio.clip = hit;
    }


    public void CmdTakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
            Die();
        //else
        //    net_animator.SetTrigger("isHit");
    }

    private void Die()
    {
       
        //Destroy(gameObject, 10f);
        
        RpcRespawn();

        
        health = 100f;

    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            //if (!animator.GetBool("Dead"))
            StartCoroutine(Respawn());

        }
    }

    IEnumerator Respawn()
    {
        //print(Time.time);
        net_animator.SetTrigger("Dead2");
       
        yield return new WaitForSeconds(5);
        animator.Play("Idle", -1, 0f);
        transform.position = spawnPoints[Random.Range(0, 4)].transform.position;
        //print(Time.time);
    }
}
