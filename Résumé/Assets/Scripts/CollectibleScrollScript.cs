﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScrollScript : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector2 direction;

    private void Start()
    {
        direction.Normalize();
    }

    private void Update()
    {
        if (speed == 0.0f)
            Debug.Log("Speed is set to 0 for " + gameObject.name);

        transform.Translate(direction * speed * Time.deltaTime * GameManager.scrollSpeedMultiplier);

        if (transform.position.x < -9.014f)
        {
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        speed = Mathf.Abs(speed);
        direction.Normalize();
    }
}
