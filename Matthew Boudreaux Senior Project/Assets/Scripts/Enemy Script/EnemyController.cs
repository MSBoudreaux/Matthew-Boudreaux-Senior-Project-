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
        Patrol,
        Active,
        Searching,
        Return,
        Fighting,
        Attack,
        TakeDamage,
        Parried,
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

    public bool isPatroller;
    public Transform[] patrolRoute;
    public int patrolRouteIndex = 0;
    public Transform currentPatrolTarget;

    public EnemyAnimator myAnim;
    public EnemyHitbox myHitbox;

    public Coroutine c;

    //Set equal to length of hitstun animation
    public float hitstunTime = .1f;
    public float dieTime = 3f;
    public float parryTime = 1f;

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

        if (isPatroller)
        {
            state = EnemyState.Patrol;
        }

        
    }

    public void Update()
    {

        if (hasFound && state != EnemyState.Death)
        {
            agent.speed = stats.chaseSpeed;
        }
        else if(state != EnemyState.Death)
        {
            agent.speed = stats.speed;
        }
        else if (state == EnemyState.Death)
        {
            agent.speed = 0;
        }
        
        switch (state)
        {
            case EnemyState.Asleep:
                break; 
            case EnemyState.Ambush:
                myAnim.IdleAnimStart();


                hasFound = LookForPlayer();
                if (hasFound)
                {
                    myAnim.IdleAnimEnd();
                    state = EnemyState.Active;
                    Debug.Log("has found player!");
                    agent.SetDestination(target.position);
                }

                break;
            case EnemyState.Patrol:

                myAnim.WalkAnimStart();
                hasFound = LookForPlayer();

                if (hasFound)
                {
                    myAnim.WalkAnimEnd();
                    state = EnemyState.Active;
                    agent.SetDestination(target.position);
                }
                else
                {
                    if(agent.remainingDistance <= 1f)
                    {
                        patrolRouteIndex = (patrolRouteIndex + 1) % patrolRoute.Length;
                        currentPatrolTarget = patrolRoute[patrolRouteIndex];
                        agent.SetDestination(currentPatrolTarget.position);

                    }
                    else
                    {
                        currentPatrolTarget = patrolRoute[patrolRouteIndex];
                        agent.SetDestination(currentPatrolTarget.position);

                    }

                }

                break;

                
            case EnemyState.Active:

                myAnim.RunAnimStart();

                hasFound = LookForPlayer();

                if (hasFound)
                {
                    if ((target.position - transform.position).magnitude <= stats.chaseDistance)
                    {
                        myAnim.RunAnimEnd();
                        state = EnemyState.Fighting;
                        break;
                    }

                    agent.SetDestination(target.position);
                    break;
                }

                if (!hasFound)
                {
                    state = EnemyState.Searching;
                }


                break;
            case EnemyState.Searching:


                if(agent.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    hasFound = LookForPlayer();

                    if (!hasFound)
                    {
                        myAnim.RunAnimEnd();
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

                }
                break;
            case EnemyState.Fighting:
                RotateToFacePlayer();

                myAnim.IdleAnimStart();
                agent.SetDestination(transform.position);

                if(c == null)
                {

                    AttemptAttack();
                }

                else if ((target.position - transform.position).magnitude >= stats.chaseDistance)
                {
                    myAnim.IdleAnimEnd();
                    state = EnemyState.Active;
                }
                break;
            case EnemyState.Return:
                myAnim.WalkAnimStart();
                if ((transform.position.x == idlePos.x) && (transform.position.z == idlePos.z))
                {
                    if (!isPatroller)
                    {
                        myAnim.WalkAnimEnd();
                        state = EnemyState.Ambush;
                    }
                    else
                    {
                        myAnim.WalkAnimEnd();
                        state = EnemyState.Patrol;
                    }
                }
                break;

            case EnemyState.Attack:
                break;
            case EnemyState.TakeDamage:
                break;
            case EnemyState.Death:
                agent.isStopped = true;

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
            if (hit.collider.tag == "Player" || hit.collider.tag == "pHitbox")
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
            c = null;

            myAnim.IdleAnimEnd();
            myAnim.RunAnimEnd();
            myAnim.WalkAnimEnd();
            myAnim.AttackAnimEnd();
            //Hitbox

            myAnim.HitstunAnimEnd();
            myAnim.DeathAnimStart();

            c = StartCoroutine(die(dieTime));

            

            state = EnemyState.Death;
        }
        else
        {
            c = null;

            myAnim.IdleAnimEnd();
            myAnim.RunAnimEnd();
            myAnim.WalkAnimEnd();
            myAnim.AttackAnimEnd();

            //Hitbox


            myAnim.HitstunAnimStart();
            state = EnemyState.TakeDamage;
            c = StartCoroutine(hitstun(hitstunTime));
        }
    }

    public void ParryStun()
    {
        c = null;

        Debug.Log("Received parry message");
        myAnim.IdleAnimEnd();
        myAnim.RunAnimEnd();
        myAnim.WalkAnimEnd();
        myAnim.AttackAnimEnd();
        //Hitbox


        myAnim.HitstunAnimStart();
        c = StartCoroutine(hitstun(parryTime));
        state = EnemyState.Parried;
    }

    IEnumerator ParryStunWait(float time)
    {
        yield return new WaitForSeconds(time);
        state = EnemyState.Active;
    }

    IEnumerator hitstun(float time)
    {
        yield return new WaitForSeconds(time);
        myAnim.HitstunAnimEnd();
        //Hitbox


        state = EnemyState.Active;
        c = null;
    }

    IEnumerator die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(transform.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pHitbox"))
        {
            TakeDamage(other.GetComponent<PlayerHitbox>().damage);
        }
    }

    public void AttemptAttack()
    {
        if (c == null) {
            c = null;

            myAnim.IdleAnimEnd();
            state = EnemyState.Attack;
            myAnim.AttackAnimStart();
            //Hitbox

            c = StartCoroutine(AttackWait(stats.attackOffset));
        }
    }

    IEnumerator AttackWait(float time)
    {
        yield return new WaitForSeconds(time);
        state = EnemyState.Active;
        //Hitbox
        myAnim.AttackAnimEnd();
        c = null;
    }


}
