using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    /* Enemy States
     * Asleep: Inactive until awoken by WakeUp being called
     * Ambush: Inactive until player enters line of sight
     * Active: Path to player: when in range, enter Attack
     * Attack: Perform attack behavior, then return to Active
     * Take Damage: Perform damage taken behavior, then return to Active
     * Death: Play death animation, then destroy self
     */
    public enum EnemyState
    {
        Asleep,
        Ambush,
        Active,
        Attack,
        TakeDamage,
        Death
    }

    public EnemyState state;

    public float lookRadius;
    Transform target;
    NavMeshAgent agent;

    public void Awake()
    {

    }

    public void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        switch (state)
        {
            case EnemyState.Asleep:
                break;
            case EnemyState.Ambush:
                break;
            case EnemyState.Active:
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.TakeDamage:
                break;
            case EnemyState.Death:
                break;
        }
    }

    public void WakeUp()
    {
        if(state == EnemyState.Asleep)
        {
            state = EnemyState.Active;
        }
    }
}
