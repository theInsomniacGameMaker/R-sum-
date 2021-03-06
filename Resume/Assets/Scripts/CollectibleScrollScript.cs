﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScrollScript : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector2 direction;


    private RewindTime selfRewindTime;

    private void Awake()
    {
        selfRewindTime = GetComponent<RewindTime>();
    }

    private void Start()
    {
        direction.Normalize();
        selfRewindTime.rewindCompleted += StartAnimator;

    }

    private void Update()
    {
        if (speed == 0.0f)
            Debug.Log("Speed is set to 0 for " + gameObject.name);

        transform.Translate(direction * speed * Time.deltaTime * GameManager.scrollSpeedMultiplier);

        if (transform.position.x < -9.335f)
        {
            Destroy(gameObject);
        }

        if (selfRewindTime.IsRewinding())
        {
            if (transform.position.x > 18.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnValidate()
    {
        speed = Mathf.Abs(speed);
        direction.Normalize();
    }

    private void StartAnimator()
    {
        GetComponent<Animator>().enabled = true;
    }

    private void OnDisable()
    {
        selfRewindTime.rewindCompleted -= StartAnimator;
    }
}
