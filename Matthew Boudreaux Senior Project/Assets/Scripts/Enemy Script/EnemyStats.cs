using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public int health;
    public int maxHealth;

    public int primaryDamage;
    public int secondaryDamage;
    public float attackOffset;

    public float LookRange;
    public float speed;
    public float chaseSpeed;
    public float chaseDistance;

    public AnimationClip attack1;

    //Types of enemies. Used by EnemyController to determine enemy behavior.
    public enum EnemyAIType
    {
        skeleMelee,
        skeleRanged,
        skeleChest
    }
    public EnemyAIType myType;

    //Used for special effects againt certain enemy types
    public enum EnemyClass
    {
        Undead,
        Monster,
        Corrupted
    }
    public EnemyClass myClass;

    public void Start()
    {
        health = maxHealth;
    }
    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int inHealth)
    {
        health -= inHealth;
    }

}
