using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;


    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue myDialogue)
    {
        Debug.Log("Trigger dialogue: " + myDialogue.name);

        nameText.text = myDialogue.name;

        sentences.Clear();
        foreach (string sentence in myDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("done talking!");
        FindObjectOfType<FPSController>().SetState(FPSController.State.FreeMovement);
        nameText.text = null;
        dialogueText.text = null;
    }

}
