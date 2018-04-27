using UnityEngine;
using System.Collections;

public interface ITarget_Net
{
    void TakeDamage(float damage, Player_Net caller);
    //void Die(); Useless

}
