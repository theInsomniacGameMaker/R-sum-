using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillPanelScript : MonoBehaviour
{
    Animator selfAnimator;
    GameObject[] children;
    SkillScript[] childrenSkillScripts;
    public static List<int> indicesToFollow = new List<int>();

    [SerializeField]
    TextMeshProUGUI bigText;
    
    public delegate void InAnimationComplete(Vector2 vector2);
    public static event InAnimationComplete onScrollCollected;

    int currentSkillAcquired;
    public static int globalCounter = 0;

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

        for (int i = 0; i < transform.childCount; i++)
        {
            int numberToFillIn = Random.Range(0, transform.childCount);
            bool canfill = true;
            for (int j = 0; j < indicesToFollow.Count; j++)
            {
                if (numberToFillIn == indicesToFollow[j])
                {
                    canfill = false;
                    i--;
                    break;
                }
            }
            if (canfill)
            {
                indicesToFollow.Add(numberToFillIn);
            }
            
        }

        PlayerScript.scrollCollected += MoveIn;
        SkillScript.movementComplete += HilightSkill;

        Debug.Log("Length " + indicesToFollow.Count);
    }

    private void SkillAcquired()
    {
        //Debug.Log("Global Counter " + globalCounter);
        if (globalCounter < transform.childCount)
        {
            while (true)
            {
                currentSkillAcquired = indicesToFollow[globalCounter++];

                if (!HasBeenAcquired(currentSkillAcquired))
                {
                    //bigText.text = "Skill Retrieved\n" + childrenSkillScripts[currentSkillAcquired].GetComponent<TextMeshProUGUI>().text;
                    CallMoveByForEveryChild(-(childrenSkillScripts[currentSkillAcquired].GetComponent<RectTransform>().localPosition));
                    break;
                }
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
    }

    private void MoveOut()
    {
        selfAnimator.SetTrigger("Out");
    }

    private void SetIdle()
    {
        selfAnimator.SetTrigger("Idle");
        SetAllToOriginalPosition();
    }

    private void HilightSkill()
    {
        childrenSkillScripts[currentSkillAcquired].SetToCollected();
        Invoke("MoveOut", .3f);
    }

    private void OnDisable()
    {
        PlayerScript.scrollCollected -= MoveIn;
        SkillScript.movementComplete -= HilightSkill;
    }

    private void SetAllToOriginalPosition()
    {
        foreach (SkillScript skill in childrenSkillScripts)
        {
            skill.SetToOriginalPosition();
        }
    }


}
