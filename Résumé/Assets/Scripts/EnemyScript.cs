using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private bool isFalling = true;
    private bool willCharge;
    private bool willChargeHolder;
    private float speed;
    private Rigidbody2D selfRigidBody;
    private Animator selfAnimator;

    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    private GameObject deathFX;

    private void Awake()
    {
        selfAnimator = GetComponent<Animator>();
        selfRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        selfRigidBody.gravityScale = 0;

        PlayerScript.onPlayerDeath += PlayerKilled;
        GetComponent<RewindTime>().rewindCompleted += RewindEnded;

        if (Random.value > 0.1f)
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
            transform.Translate(direction * speed * Time.deltaTime * GameManager.scrollSpeedMultiplier);
        }

        if (transform.position.x < 7.34)
        {
            selfRigidBody.gravityScale = 2;
        }
        else
        {
            transform.Translate(direction * 4 * Time.deltaTime);
        }

        if (transform.position.x <= -9.83f || transform.position.y < -4.0f)
        {
            Destroy(gameObject);
        }

        //Debug.Log("Will Charge: " + willCharge);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isFalling = false;
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
        GetComponent<RewindTime>().rewindCompleted -= RewindEnded;

    }
}
