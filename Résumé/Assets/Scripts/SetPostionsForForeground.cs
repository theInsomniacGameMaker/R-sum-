using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPostionsForForeground : MonoBehaviour
{

    [SerializeField]
    GameObject respawnPostion;

    [SerializeField]
    float startPosition;

    [SerializeField]
    float gap;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        List<Transform> foregroundTransforms = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == "Foreground")
            {
                foregroundTransforms.Add(transform.GetChild(i));
            }
        }


        for (int i = 0; i < foregroundTransforms.Count; i++)
        {
            foregroundTransforms[i].position = new Vector2(startPosition + (i * gap), foregroundTransforms[i].position.y);
        }

        respawnPostion.transform.position = foregroundTransforms[foregroundTransforms.Count - 1].position;
    }
}
