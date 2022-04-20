using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{

    public Animator myAnim;

    public void IdleAnimStart()
    {
        myAnim.SetBool("IsIdle", true);
    }

    public void IdleAnimEnd()
    {
        myAnim.SetBool("IsIdle", false);
    }
    public void AttackAnimStart()
    {
        myAnim.SetTrigger("Attack");
    }

    public void AttackAnimEnd()
    {
        myAnim.ResetTrigger("Attack");
    }

    public void WalkAnimStart()
    {
        myAnim.SetBool("IsWalking", true);
    }

    public void WalkAnimEnd()
    {
        myAnim.SetBool("IsWalking", false);

    }

    public void RunAnimStart()
    {
        myAnim.SetBool("IsRunning", true);

    }

    public void RunAnimEnd()
    {
        myAnim.SetBool("IsRunning", false);

    }

    public void HitstunAnimStart()
    {
        myAnim.SetTrigger("HitStun");
    }
    
    public void HitstunAnimEnd()
    {
        myAnim.ResetTrigger("HitStun");
    }
    

    public void DeathAnimStart()
    {
        myAnim.SetTrigger("Death");
    }



}
