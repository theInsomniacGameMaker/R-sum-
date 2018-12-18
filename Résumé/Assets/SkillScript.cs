using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        Debug.Log("corotuine has started");
        StartCoroutine(MoveToPosition(moveBy));
    }

    private IEnumerator MoveToPosition(Vector2 moveBy)
    {
        Vector2 desiredPosition = startPostion + moveBy;
        while ((Vector2)selfRectTransform.localPosition != desiredPosition)
        {
            selfRectTransform.localPosition = Vector2.Lerp(selfRectTransform.localPosition, desiredPosition, Time.deltaTime * 2.0f);
            yield return null;
        }
        GetComponent<TextMeshProUGUI>().color = Color.red;
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
