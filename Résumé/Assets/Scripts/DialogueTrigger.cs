using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager == null)
        {
            Debug.Break();
        }
        else
        {
            Debug.Log("Is not null");
        }

        Debug.Log(dialogueManager.gameObject.name);
    }

    public void TriggerDialogue()
    {
        if (dialogue == null)
        {
            Debug.Log("the Dialogue class is null");
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
