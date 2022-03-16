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
     * Take Damage: Perform damage taken behavior, then return to Active, or enter Death if at 0 health
     * Death: Play death animation, then destroy self
     */
    public enum EnemyState
    {
        Asleep,
        Ambush,
        Active,
        Searching,
        Return,
        Fighting,
        Attack,
        TakeDamage,
        Death
    }

    public EnemyState state;
    public EnemyStats stats;

    public float lookRadius;
    public Transform target;
    public Transform raycastPoint;
    public Vector3 idlePos;
    NavMeshAgent agent;
    public bool hasFound;

    Coroutine c;
    //Set equal to length of hitstun animation
    public float hitstunTime = 1f;
    public float dieTime = 1f;

    public void Awake()
    {

    }

    public void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();
        lookRadius = stats.LookRange;
        agent.speed = stats.speed;
        idlePos = transform.position;
        
    }

    public void Update()
    {

        if (hasFound)
        {
            agent.speed = stats.chaseSpeed;
        }

        if((target.position - transform.position).magnitude <= stats.chaseDistance)
        {
            state = EnemyState.Fighting;
        }
        
        switch (state)
        {
            case EnemyState.Asleep:
                break; 
            case EnemyState.Ambush:

                hasFound = LookForPlayer();
                if (hasFound)
                {
                    state = EnemyState.Active;
                    Debug.Log("has found player!");
                    agent.SetDestination(target.position);
                }

                break;
            case EnemyState.Active:

                hasFound = LookForPlayer();

                if (hasFound)
                {
                    agent.SetDestination(target.position);
                    break;
                }

                if (!hasFound)
                {
                    state = EnemyState.Searching;
                }
                break;
            case EnemyState.Searching:

                hasFound = LookForPlayer();

                if(agent.pathStatus == NavMeshPathStatus.PathComplete && !hasFound)
                {
                    Debug.Log("has lost player!");
                    agent.SetDestination(idlePos);
                    state = EnemyState.Return;
                    break;
                }
                else if (hasFound)
                {
                    state = EnemyState.Active;
                    break;
                }

                break;
            case EnemyState.Fighting:
                RotateToFacePlayer();
                agent.SetDestination(transform.position);

                if ((target.position - transform.position).magnitude >= stats.chaseDistance)
                {
                    state = EnemyState.Active;
                }
                break;
            case EnemyState.Return:
                if((transform.position.x == idlePos.x) && (transform.position.z == idlePos.z))
                {
                    state = EnemyState.Ambush;
                }
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.TakeDamage:
                break;
            case EnemyState.Death:
                Destroy(transform.gameObject);
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

    public void RotateToFacePlayer()
    {
        Quaternion lookRotation;
        Vector3 direction = (target.position - transform.position).normalized;

        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
    }

    public bool LookForPlayer()
    {
        bool hasFoundPlayer = false;


        Vector3 start = raycastPoint.position;
        Vector3 targetPos = target.position;
        Vector3 castDirection = (target.position - start);
        RaycastHit hit;

        Debug.DrawRay(start, castDirection, Color.red, 0.01f);
        if(Physics.Raycast(new Ray(start, castDirection.normalized), out hit, lookRadius))
        {
            if (hit.collider.tag == "Player")
            {
                hasFoundPlayer = true;
            }
        }

        return hasFoundPlayer;
        
    }

    public void TakeDamage(int inDamage)
    {
        stats.TakeDamage(inDamage);
        if(stats.GetHealth() <= 0)
        {
            state = EnemyState.TakeDamage;
            c = StartCoroutine(die(dieTime));
        }
        else
        {
            state = EnemyState.TakeDamage;
            c = StartCoroutine(hitstun(hitstunTime));
        }
    }

    IEnumerator hitstun(float time)
    {
        yield return new WaitForSeconds(time);
        state = EnemyState.Active;
    }

    IEnumerator die(float time)
    {
        //Play death animation
        yield return new WaitForSeconds(time);
        state = EnemyState.Death;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pHitbox"))
        {
            TakeDamage(other.GetComponent<PlayerHitbox>().damage);
        }
    }


}
