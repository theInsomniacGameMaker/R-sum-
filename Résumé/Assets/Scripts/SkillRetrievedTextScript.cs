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
        selfRectTransform.position = new Vector2(10000, 10000);
    }

    private void Update()
    { 
        selfRectTransform.position = Sinerp(selfRectTransform.position, desiredPosition, Time.deltaTime);

        if ((Vector2)selfRectTransform.position == desiredPosition)
        {
            selfRectTransform.position = new Vector2(10000, 10000);
        }
    }

    public void SetPosition(Vector2 position)
    {
        if (selfRectTransform == null)
        {
            Debug.Log("Is Null");
        }

        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
            selfRectTransform.position = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(position);

        desiredPosition = new Vector2(selfRectTransform.position.x, selfRectTransform.position.y + 100);

    }

    public float Sinerp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
    }

    public Vector2 Sinerp(Vector2 start, Vector2 end, float value)
    {
        return new Vector2(Mathf.Lerp(start.x, end.x, Mathf.Sin(value * Mathf.PI * 0.5f)), Mathf.Lerp(start.y, end.y, Mathf.Sin(value * Mathf.PI * 0.5f)));
    }
}
