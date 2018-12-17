using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool isOnGround;
    private bool isRolling;
    private bool isSlashing;
    private bool isDead;

    private Rigidbody2D selfRigidBody;
    private Animator selfAnimator;

    public delegate void PlayerDeath();
    public static event PlayerDeath onPlayerDeath;
    public static event PlayerDeath startRewind;

    [SerializeField]
    private float forceFactor;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private Transform attackPosition;

    [SerializeField]
    private GameObject itemFeedback;

    private void Awake()
    {
        selfRigidBody = GetComponent<Rigidbody2D>();
        selfAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        isDead = false;
        GetComponent<RewindTime>().rewindCompleted += RewindCompleted;
    }

    private void Update()
    {
        PlayerInput();
        AnimatorUpdate();
    }

    private void PlayerInput()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !isOnGround)
            {
                isOnGround = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && !isRolling)
            {
                isRolling = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !isSlashing)
            {
                isSlashing = true;
            }
        }
    }

    private void AnimatorUpdate()
    {
        selfAnimator.SetBool("Jump", isOnGround);
        selfAnimator.SetBool("Rolling", isRolling);
        selfAnimator.SetBool("Slashing", isSlashing);
        selfAnimator.SetBool("Dead", isDead);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (Mathf.Round(selfRigidBody.velocity.y) <= 0.0f)
            {
                isOnGround = false;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
            GameManager.scrollSpeedMultiplier = 0;

            if (onPlayerDeath != null)
            {
                onPlayerDeath();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlowDownTime"))
        {
            Time.timeScale = 0.5f;
            SpriteRenderer[] darkenObjects = FindObjectsOfType<SpriteRenderer>();

            foreach (SpriteRenderer obj in darkenObjects)
            {
                if (obj.gameObject != gameObject && collision.transform.parent.gameObject != obj.gameObject)
                    obj.color = new Color(0.5f, 0.5f, 0.5f);
            }
        }

        if (collision.gameObject.CompareTag("Scroll"))
        {
            StartCoroutine(ResetTimeAfterDelay(.3f));
            Instantiate(itemFeedback, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlowDownTime"))
        {
            StartCoroutine(ResetTimeAfterDelay(0.0f));
        }
    }

    private void Jump()
    {
        selfRigidBody.AddForce(new Vector2(0, 1) * forceFactor, ForceMode2D.Impulse);
    }

    private void EndRolling()
    {
        isRolling = false;
    }

    private void EndSlashing()
    {
        isSlashing = false;
    }

    private void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange);

        foreach (Collider2D enemy in enemiesToDamage)
        {
            if (enemy.gameObject.CompareTag("Hitbox"))
            {
                CameraShake.Instance.ShakeCamera(3,0.2f);
                enemy.transform.parent.SendMessage("TakeDamage");
            }
        }
    }

    private void DeathAnimationEnd()
    {
        Invoke("SendCallBack", 0.3f);
    }

    private void SendCallBack()
    {
        isDead = false;

        if (startRewind != null)
        {
            startRewind();
        }
    }

    private void RewindCompleted()
    {
        Invoke("DelayedRewindComplete",1.0f);
    }

    private void DelayedRewindComplete()
    {
        GameManager.scrollSpeedMultiplier = 1;
        selfAnimator.enabled = true;
        isDead = false;
    }

    private IEnumerator ResetTimeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 1.0f;
        SpriteRenderer[] darkenObjects = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer obj in darkenObjects)
        {
            if (obj.gameObject != gameObject )
                obj.color = Color.white;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}