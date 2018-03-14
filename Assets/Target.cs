using UnityEngine;
using UnityEngine.Networking;

public class Target : NetworkBehaviour
{

    public float health = 100f;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
            animator.SetTrigger("isHit");
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 10f);
        
    }
}
