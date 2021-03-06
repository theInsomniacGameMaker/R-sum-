﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private bool isFalling = true;
    private bool willCharge;
    private bool willChargeOriginal;
    private bool willChargeHolder;
    private float speed;
    private Rigidbody2D selfRigidBody;
    private Animator selfAnimator;
    private RewindTime selfRewindTime;
    private bool firstFall = true;
    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    private GameObject deathFX;


    [SerializeField]
    private AudioClip landGrunt;


    private void Awake()
    {
        selfAnimator = GetComponent<Animator>();
        selfRigidBody = GetComponent<Rigidbody2D>();
        selfRewindTime = GetComponent<RewindTime>();
    }

    private void Start()
    {
        selfRigidBody.gravityScale = 0;

        PlayerScript.onPlayerDeath += PlayerKilled;
        GetComponent<RewindTime>().rewindCompleted += RewindEnded;

        if (Random.value > 0.5f)
        {
            willCharge = true;
            speed = 6.0f;
        }
        else
        {
            speed = 4.0f;
        }

        willChargeOriginal = willCharge;
    }

    private void Update()
    {
        selfAnimator.SetBool("Falling", isFalling);
        selfAnimator.SetBool("Charge", willCharge);

        if (!isFalling)
        {
            transform.Translate(direction * speed * Time.deltaTime * GameManager.scrollSpeedMultiplier);
        }

        if (transform.position.x < 7.34)
        {
            selfRigidBody.gravityScale = 2;
        }
        else
        {
            if (!selfRewindTime.IsRewinding())
                transform.Translate(direction * 4 * Time.deltaTime);

            selfRigidBody.gravityScale = 0;

        }

        if (transform.position.x <= -10.059f || transform.position.y < -4.0f)
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

        if (selfRigidBody.velocity.x < -0.001f)
        {
            willCharge = false;
            isFalling = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isFalling = false;
            willCharge = willChargeOriginal;
            if (firstFall)
            {
                firstFall = false;
                AudioSource.PlayClipAtPoint(landGrunt, Vector2.zero, 1.0f);
            }
         
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Bubble") && transform.GetChild(1) != collision.gameObject)
        {
            speed = 4.0f;
            willCharge = false;
            willChargeOriginal = false;
        }

    }

    public void TakeDamage()
    {
        speed = 0.0f;
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void PlayerKilled()
    {
        willChargeHolder = willCharge;
        willCharge = false;
    }

    private void RewindEnded()
    {
        Invoke("DelayedRewindCompleted", 1.0f);
    }

    private void DelayedRewindCompleted()
    {
        willCharge = willChargeHolder;
        selfAnimator.enabled = true;
    }

    private void OnDisable()
    {
        PlayerScript.onPlayerDeath -= PlayerKilled;
        selfRewindTime.rewindCompleted -= RewindEnded;
    }
}
