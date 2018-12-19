using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnInvisible : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private float resetPostion;

    [SerializeField]
    private GameObject scroll;
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    bool testing;

    private Transform[] children;
    private RewindTime selfRewindTime;

    private void Awake()
    {
        selfRewindTime = GetComponent<RewindTime>();
    }

    private void Update()
    {
        ResetPostion();
    }

    private void OnBecameInvisible()
    {
        if (testing)
        {
            Debug.Log("Postion X to reset at " + transform.position.x);
            Debug.Break();
        }

    }

    private void ResetPostion()
    {
        if (transform.position.x < resetPostion)
        {
            if (spawnPosition != null)
            {
                transform.position = new Vector2(spawnPosition.position.x, transform.position.y);

                if (gameObject.name == "Foreground")
                {
                    if (!selfRewindTime.IsRewinding())
                    {
                        SpawnManager.resetCount++;
                        if (SpawnManager.resetCount % 4 == 1)
                        {
                            Instantiate(scroll, (Vector2)(transform.GetChild(Random.Range(0, transform.childCount)).position), Quaternion.identity);
                        }
                        else if (SpawnManager.resetCount % 4 == 2 || SpawnManager.resetCount % 4 == 3)
                        {
                            Instantiate(enemy, new Vector2(transform.position.x + Random.Range(-1.0f, 1.0f), 6.0f), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

}
