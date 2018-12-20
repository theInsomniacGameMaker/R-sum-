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
                float offset = transform.position.x - resetPostion;
                transform.position = new Vector2(spawnPosition.position.x + offset, transform.position.y);

                if (gameObject.name == "Foreground")
                {
                    if (!selfRewindTime.IsRewinding() && GameManager.tutorialOver)
                    {
                        switch (SpawnManager.resetCount++ % 3)
                        {
                            case 0:
                                Instantiate(enemy, new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), 6.0f), Quaternion.identity);
                                break;
                            case 1:
                                Instantiate(scroll, (Vector2)(transform.GetChild(Random.Range(0, transform.childCount)).position), Quaternion.identity);
                                break;
                            case 2:
                                //Instantiate(enemy, new Vector2(transform.position.x + Random.Range(-1.0f, 1.0f), 6.0f), Quaternion.identity);
                                break;
                            case 3:
                                break;

                        }
                    }
                }
            }
        }
    }

}
