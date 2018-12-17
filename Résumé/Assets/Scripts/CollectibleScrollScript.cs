using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScrollScript : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x < -9.014f)
        {
            Destroy(gameObject);
        }
    }
}
