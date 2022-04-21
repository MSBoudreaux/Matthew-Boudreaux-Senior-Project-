using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject[] triggeredObjects;
    public bool startsCutscene;
    public bool requiresKey;
    public bool isUnlocked = false;



    public void InteractTrigger()
    {

        foreach (GameObject obj in this.triggeredObjects)
        {
            if (obj.CompareTag("Door"))
            {
                Debug.Log("Door Opening");
                obj.GetComponent<DoorScript>().TriggerDoor();
            }

            else if (obj.CompareTag("Trap"))
            {
                Debug.Log("Dealing Damage");
                obj.GetComponent<DamageScript>().DealDamage();
            }

            else if (obj.CompareTag("Enemy"))
            {
                Debug.Log("Waking Enemy Up");
                obj.GetComponent<EnemyController>().state = EnemyController.EnemyState.Patrol;
                obj.GetComponent<EnemyController>().isPatroller = true;

            }

            else if (obj.CompareTag("PopupTrigger"))
            {
                obj.GetComponent<TriggerDialogue>().DialogueTrigger();
            }
        }

        isUnlocked = true;

    }

}
