using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    private bool hasBeenCollected;
    Vector2 startPostion;
    RectTransform selfRectTransform;
    private void Start()
    {
        selfRectTransform = GetComponent<RectTransform>();

        if (!selfRectTransform)
        {
            Debug.Log("This object does not have a RectTranform");
        }

        startPostion = selfRectTransform.localPosition;
        SkillPanelScript.onScrollCollected += StartMoveToPostion;
    }


    private void StartMoveToPostion(Vector2 moveBy)
    {
        StartCoroutine(MoveToPosition(moveBy));
    }


    private IEnumerator MoveToPosition(Vector2 moveBy)
    {
        Vector2 desiredPosition = startPostion + moveBy;
        while ((Vector2)selfRectTransform.localPosition != desiredPosition)
        {
            selfRectTransform.localPosition = Vector2.Lerp(selfRectTransform.localPosition, desiredPosition, Time.deltaTime);
            yield return null;
        }
    }

    public bool HasBeenCollected()
    {
        return hasBeenCollected;
    }

    private void OnDisable()
    {
        SkillPanelScript.onScrollCollected -= StartMoveToPostion;

    }

}
