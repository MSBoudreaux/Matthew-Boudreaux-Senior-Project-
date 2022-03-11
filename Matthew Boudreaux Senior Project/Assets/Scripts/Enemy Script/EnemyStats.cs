using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public int health;
    public int maxHealth;

    public int primaryDamage;
    public int secondaryDamage;

    public float LookRange;
    public float speed;
    public float chaseSpeed;
    public float chaseDistance;

    //Types of enemies. Used by EnemyController to determine enemy behavior.
    public enum EnemyType
    {
        skeleMelee,
        skeleRanged,
        skeleChest
    }
    public EnemyType myType;

    public void Start()
    {
        health = maxHealth;
    }
    public int GetHealth()
    {
        return health;
    }

    public void AddHealth(int inHealth)
    {
        health += inHealth;
    }

}
