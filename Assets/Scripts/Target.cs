using UnityEngine;
using System.Collections;

public interface ITarget
{
    void TakeDamage(float damage, Human caller);
    void Die();

}
