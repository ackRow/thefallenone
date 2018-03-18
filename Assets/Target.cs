using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Target : NetworkBehaviour // Tout les personnages ayant de la vie implémente cette classe
{
    [SyncVar]
    public float health = 100f; // la vie est synchronisé sur les clients et le serveur

    public NetworkStartPosition[] spawnPoints;

    Animator animator;
    NetworkAnimator net_animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        net_animator = GetComponent<NetworkAnimator>();
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
    }


    public void CmdTakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
            Die();
        else
            animator.SetTrigger("isHit"); // animation lorsqu'on est touché
    }

    private void Die()
    {
        RpcRespawn();  
    }

    [ClientRpc] // La fonction est synchronisé avec le client
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            StartCoroutine(Respawn()); // Permet de mettre un delay dans la fonction

        }
    }

    IEnumerator Respawn()
    {
        net_animator.SetTrigger("Dead2"); // On lance l'animation de mort
       
        yield return new WaitForSeconds(5); // delay
        
        
        // On relance l'animation Idle et on remet la vie à 100
        animator.Play("Idle", -1, 0f); 
        transform.position = spawnPoints[Random.Range(0, 4)].transform.position;
        health = 100f;
    }
}
