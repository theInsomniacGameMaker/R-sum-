using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting Conversation with " + dialogue.name);
        sentences.Clear();

        foreach (string sentence in sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count ==0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        Debug.Log(sentence);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        //set to the text field or whatever
        foreach (char letter in sentence.ToCharArray())
        {
            // add to the 
            //TextAlignment += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
