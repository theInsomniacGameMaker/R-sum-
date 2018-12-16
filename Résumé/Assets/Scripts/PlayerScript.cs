using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool isOnGround;
    private bool isRolling;
    private bool isSlashing;

    private Rigidbody2D selfRigidBody;
    private Animator selfAnimator;

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

    }

    private void Update()
    {
        PlayerInput();
        AnimatorUpdate();
    }

    private void PlayerInput()
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

    private void AnimatorUpdate()
    {
        selfAnimator.SetBool("Jump", isOnGround);
        selfAnimator.SetBool("Rolling", isRolling);
        selfAnimator.SetBool("Slashing", isSlashing);
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
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                enemy.gameObject.SendMessage("TakeDamage");
            }
        }
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
