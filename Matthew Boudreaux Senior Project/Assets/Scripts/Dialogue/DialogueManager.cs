using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Text popupText;
    public float waitTime = 4f;

    Coroutine c;

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

    public void DisplayTimedSentence(Dialogue myDialogue)
    {
        c = null;
        sentences.Clear();
        sentences.Enqueue(myDialogue.sentences[0]);
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        popupText.text = sentence;
        c = StartCoroutine(clearSentence(waitTime));
    }

    IEnumerator clearSentence(float time)
    {
        yield return new WaitForSeconds(time);
        popupText.text = null;
    }

}
