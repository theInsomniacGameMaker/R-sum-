using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortChildren : MonoBehaviour
{
    private void OnValidate()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().localPosition = new Vector2(0, (i - (childCount / 2)) * 100); 
        }
    }
}
