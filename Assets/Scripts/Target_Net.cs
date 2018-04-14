using UnityEngine;
using System.Collections;

public interface ITarget_Net
{
    void TakeDamage(float damage, Human_Net caller);
    void Die();

}
