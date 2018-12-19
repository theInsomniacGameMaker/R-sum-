using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRetrievedTextScript : MonoBehaviour
{
    private Vector2 desiredPosition;
    RectTransform selfRectTransform;

    private void OnEnable()
    {
        selfRectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        selfRectTransform = GetComponent<RectTransform>();
    }
   
    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
