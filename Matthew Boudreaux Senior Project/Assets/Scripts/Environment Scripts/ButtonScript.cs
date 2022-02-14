using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject[] triggeredObjects;
    public bool startsCutscene;

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
        }

    }

}
