using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{

    public EnemyStats stats;
    public int damage;
    public bool isStressCausing;
    public bool isPrimaryAttack;

    private void Update()
    {
        if(stats != null)
        {
            if (isPrimaryAttack)
            {
                damage = stats.primaryDamage;
            }
            else if (!isPrimaryAttack)
            {
                damage = stats.secondaryDamage;
            }
        }
    }
}
