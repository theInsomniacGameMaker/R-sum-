using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnInvisible : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;

    private void OnBecameInvisible()
    {
        //Debug.Break();
        transform.position = new Vector2(spawnPosition.position.x, transform.position.y);
    }



}
