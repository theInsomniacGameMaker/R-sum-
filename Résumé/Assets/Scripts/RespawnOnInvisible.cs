using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnInvisible : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private GameObject scroll;


    private Transform[] children;

    private void Awake()
    {
        //Debug.Log(transform.childCount);
    }

    private void OnBecameInvisible()
    {
        if (spawnPosition != null)
        {
            transform.position = new Vector2(spawnPosition.position.x, transform.position.y);

            if (gameObject.name == "Foreground")
            {
                if (SpawnManager.resetCount++ % 2 == 0)
                {
                    Instantiate(scroll, (Vector2)(transform.GetChild(Random.Range(0, transform.childCount)).position), Quaternion.identity);
                }
            }
        }
    }


}
