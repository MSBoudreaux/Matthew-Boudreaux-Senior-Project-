using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    int index = 0;
    public Dialogue[] myDialogue;
    public bool isAutoRead; //If a trigger just causes a dialogue to appear without interrupting action.
  

    public void DialogueTrigger()
    {




        FindObjectOfType<DialogueManager>().StartDialogue(myDialogue[index]);
        if(index < myDialogue.Length - 1)
        {
            index++;
        }
        
    }

    public void TimedDialogueTrigger(int i)
    {
        if (isAutoRead)
        {
            FindObjectOfType<DialogueManager>().DisplayTimedSentence(myDialogue[i]);
        }
    }

}
