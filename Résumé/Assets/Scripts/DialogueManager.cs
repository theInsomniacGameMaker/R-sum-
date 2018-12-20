using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();
    [SerializeField]
    private Animator dialogueBoxAnimator;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private AudioClip keyStroke;

  

    private void Start()
    {
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting Conversation with " + dialogue.name);
        dialogueBoxAnimator.SetBool("Open", true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
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
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //Debug.Log(sentence);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        int counter = 0;
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (counter++ % 4 == 0)
                AudioSource.PlayClipAtPoint(keyStroke, Vector2.zero, 0.3f);
            text.text += letter.ToString();
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void EndDialogue()
    {
        GameManager.tutorialOver = true;
        dialogueBoxAnimator.SetBool("Open", false);
    }


}
