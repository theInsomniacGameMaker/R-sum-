using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelScript : MonoBehaviour
{
    Animator selfAnimator;
    GameObject[] children;
    SkillScript[] childrenSkillScripts;

    public delegate void AnimationComplete(Vector2 vector2);
    public static event AnimationComplete onScrollCollected;

    private void Awake()
    {
        selfAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        children = new GameObject[transform.childCount];
        childrenSkillScripts = new SkillScript[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
            childrenSkillScripts[i] = children[i].GetComponent<SkillScript>();
        }

        PlayerScript.scrollCollected += MoveIn;
    }

    private void SkillAcquired()
    {
        Debug.Log("This Skill Acquiured function was called");
        int count = 0;
        while (true)
        {
            int randomIndex = Random.Range(0, transform.childCount);

            if (!HasBeenAcquired(randomIndex))
            {
                CallMoveByForEveryChild(-(childrenSkillScripts[randomIndex].GetComponent<RectTransform>().localPosition));
                break;
            }

            if (count++ >= transform.childCount)
            {
                break;
            }
        }
    }

    //is the skill already ben collected by the player
    private bool HasBeenAcquired(int i)
    {
        return childrenSkillScripts[i].HasBeenCollected();
    }

    //this function sends a message to all of the children to move by a certain amount
    private void CallMoveByForEveryChild(Vector2 moveBy)
    {
        onScrollCollected(moveBy);
    }

    private void MoveIn()
    {
        selfAnimator.SetTrigger("In");
        Debug.Log("In");
    }

    private void MoveOut()
    {
        selfAnimator.SetTrigger("Out");
    }

    private void SetIdle()
    {
        selfAnimator.SetTrigger("Idle"); 
    }
}
