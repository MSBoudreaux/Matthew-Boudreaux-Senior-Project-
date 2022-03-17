using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public FPSController myController;

    public Animator myAnimator;
    public bool isAttacking;

    public bool isBlocking;
    public bool isParrying;
    public bool HasParried;

    public AnimatorOverrideController myOverrideController;

    public GameObject weapon;
    public GameObject shield;

    public AnimationClip AttackAnimation;
    public float BlockSpeedPercent;


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

    }

    public void ParryTriggered()
    {

    }

    public void BlockAnimStart()
    {

    }
    
    public void BlockAnimEnd()
    {

    }
}
