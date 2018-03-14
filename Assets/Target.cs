using UnityEngine;
using UnityEngine.Networking;

public class Target : NetworkBehaviour
{

    public float health = 100f;

    Animator animator;
    NetworkAnimator net_animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        net_animator = GetComponent<NetworkAnimator>();
    }

    [Command]
    public void CmdTakeDamage (float amount)
    {
        if (isServer)
        {
            health -= amount;
        }
        if (health <= 0f)
            Die();
        else
            net_animator.SetTrigger("isHit");
    }

    private void Die()
    {
        animator.SetBool("Dead", true);
        Destroy(gameObject, 10f);
        
    }
}
