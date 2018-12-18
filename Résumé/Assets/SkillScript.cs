﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillScript : MonoBehaviour
{
    private bool hasBeenCollected;
    Vector2 startPostion;
    RectTransform selfRectTransform;
    CameraShake mainCameraShake;

    private void Awake()
    {
        mainCameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

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
        while ((Vector2)selfRectTransform.localPosition != Round(desiredPosition))
        {
            selfRectTransform.localPosition = Vector2.Lerp(selfRectTransform.localPosition, desiredPosition, Time.deltaTime * 2.0f);
            yield return null;
        }
        GetComponent<TextMeshProUGUI>().color = Color.red;
        mainCameraShake.StartCameraShake(0.3f,.3f);
        hasBeenCollected = true;
    }

    public bool HasBeenCollected()
    {
        return hasBeenCollected;
    }

    private Vector2 Round(Vector2 vector2)
    {
        return new Vector2(Mathf.Round(vector2.x), Mathf.Round(vector2.y));
    }

    private void OnDisable()
    {
        SkillPanelScript.onScrollCollected -= StartMoveToPostion;
    }



}
