using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxScript : MonoBehaviour
{
    private void Start()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
