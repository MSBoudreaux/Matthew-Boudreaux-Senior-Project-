using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public Animator animator;
    public bool staysOpen;

    public void TriggerDoor()
    {
        if(staysOpen && !animator.GetBool("isOpen"))
        {
            Debug.Log("Door Opened!");
            animator.SetBool("isOpen", true);
        }
        else

        if(!staysOpen)
        {
            Debug.Log("Door Opened!");
            animator.SetBool("isOpen", !animator.GetBool("isOpen"));
        }

    }

}
