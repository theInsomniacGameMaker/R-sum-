using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnInvisible : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private GameObject scroll;
    [SerializeField]
    private GameObject enemy;

    private Transform[] children;
    private RewindTime selfRewindTime;

    private void Awake()
    {
        selfRewindTime = GetComponent<RewindTime>();
    }

    private void OnBecameInvisible()
    {

        if (spawnPosition != null)
        {
            transform.position = new Vector2(spawnPosition.position.x, transform.position.y);
            
            if (gameObject.name == "Foreground")
            {
                if (!selfRewindTime.IsRewinding())
                {
                    if (SpawnManager.resetCount++ % 2 == 0)
                    {
                        Instantiate(scroll, (Vector2)(transform.GetChild(Random.Range(0, transform.childCount)).position), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(enemy, new Vector2(transform.position.x + Random.Range(-0.75f, 0.75f), 6.0f), Quaternion.identity);
                    }
                }
            }
        }
    }


}
