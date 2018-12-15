using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private bool isFalling = true;
    private bool willCharge;
    private float speed;
    private Rigidbody2D selfRigidBody;
    private Animator selfAnimator;


    [SerializeField]
    private Vector2 direction;

    private void Awake()
    {
        selfAnimator = GetComponent<Animator>();
        selfRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (Random.value > 0.5f)
        {
            willCharge = true;
            speed = 6.0f;
        }
        else
        {
            speed = 4.0f;
        }
    }

    private void Update()
    {
        selfAnimator.SetBool("Falling", isFalling);
        selfAnimator.SetBool("Charge", willCharge);

        if (!isFalling)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        Debug.Log("Speed = " + speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isFalling = false;
        }
    }


}
