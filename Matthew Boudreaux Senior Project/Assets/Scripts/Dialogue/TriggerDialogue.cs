using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    int index = 0;
    public Dialogue[] myDialogue;
    

    public void DialogueTrigger()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(myDialogue[index]);
        if(index < myDialogue.Length - 1)
        {
            index++;
        }
        
    }

}
