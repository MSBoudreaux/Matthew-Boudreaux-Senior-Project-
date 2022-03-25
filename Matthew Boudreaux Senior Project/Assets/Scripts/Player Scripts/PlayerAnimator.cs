using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public FPSController myController;

    public Animator myAnimator;
    public bool isAttacking;

    public bool isBlocking;

    //use animator triggers
    public bool isParrying;

    public AnimatorOverrideController myOverrideController;

    public GameObject weapon;
    public GameObject shield;

    public AnimationClip AttackAnimation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AttackAnimStart()
    {
        isAttacking = true;
        myAnimator.SetBool("IsAttacking", isAttacking);

    }

    public void AttackAnimEnd()
    {
        isAttacking = false;
        myAnimator.SetBool("IsAttacking", isAttacking);
    }

    public void ParryAnimStart()
    {
        isParrying = true;
        myAnimator.SetBool("IsParrying", isParrying);
    }

    public void ParryTriggered()
    {
        myAnimator.SetTrigger("HasParried");
        isParrying = false;
        myAnimator.SetBool("IsParrying", isParrying);
    }

    public void BlockAnimStart()
    {
        isBlocking = true;
        isParrying = false;
        myAnimator.SetBool("IsBlocking", isBlocking);
        myAnimator.SetBool("IsParrying", isParrying);
    }
    
    public void BlockAnimEnd()
    {
        isBlocking = false;
        isParrying = false;
        myAnimator.SetBool("IsParrying", isParrying);
        myAnimator.SetBool("IsBlocking", isBlocking);

    }
}
