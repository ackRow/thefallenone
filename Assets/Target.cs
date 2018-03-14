using UnityEngine;
using UnityEngine.Networking;

public class Target : NetworkBehaviour
{
    [SyncVar]
    public float health = 100f;

    Animator animator;
    NetworkAnimator net_animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        net_animator = GetComponent<NetworkAnimator>();
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
        if(!animator.GetBool("Dead"))
            animator.SetBool("Dead", true);
        Destroy(gameObject, 10f);
        
    }
}
