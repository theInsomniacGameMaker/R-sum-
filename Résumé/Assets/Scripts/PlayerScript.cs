using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool isJumping;
    private bool isRolling;
    private bool isSlashing;
    private bool isDead;

    private Rigidbody2D selfRigidBody;
    private Animator selfAnimator;

    public delegate void PlayerDeath();
    public static event PlayerDeath onPlayerDeath;
    public static event PlayerDeath startRewind;

    public delegate void ScrollCollected();
    public static event ScrollCollected scrollCollected;

    public delegate void NormalGameStarted();
    public static event NormalGameStarted normalGameStarted;

    [SerializeField]
    private AudioClip bladeSFX;

    [SerializeField]
    private AudioClip enemyDeath;

    [SerializeField]
    private AudioClip scrollCollect;

    [SerializeField]
    private AudioClip jumpSFX;

    [SerializeField]
    private AudioClip deathSFX;

    [SerializeField]
    private AudioClip rollSFX;

    [SerializeField]
    private AudioClip runSFX;

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
            if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
            {
                JumpInput();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && !isRolling)
            {
                RollInput();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !isSlashing)
            {
                SlashInput();
            }
        }
    }

    public void JumpInput()
    {
        isJumping = true;
        isRolling = false;
        isSlashing = false;
    }

    public void RollInput()
    {
        isRolling = true;
        isJumping = false;
        isSlashing = false;
    }

    public void SlashInput()
    {
        isSlashing = true;
        isJumping = false;
        isRolling = false;
    }

    private void AnimatorUpdate()
    {
        selfAnimator.SetBool("Jump", isJumping);
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
                isJumping = false;
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
            //calling the event that goes to the panel
            if (scrollCollected != null)
            {
                scrollCollected();
            }
            StartCoroutine(ResetTimeAfterDelay(.3f));
            Instantiate(itemFeedback, collision.gameObject.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(scrollCollect, Vector2.zero);
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
                enemy.transform.parent.SendMessage("TakeDamage");
                AudioSource.PlayClipAtPoint(enemyDeath, Vector2.zero,10.0f);
            }
        }
    }

    private void DeathAnimationStart()
    {
        isJumping = true;
        isRolling = false;
        isSlashing = false;

        AudioSource.PlayClipAtPoint(deathSFX, Vector2.zero);
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
        Invoke("DelayedRewindComplete", 1.0f);
    }

    private void DelayedRewindComplete()
    {
        GameManager.scrollSpeedMultiplier = 1;
        selfAnimator.enabled = true;
        isDead = false;
        normalGameStarted();
    }

    private IEnumerator ResetTimeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 1.0f;
        SpriteRenderer[] darkenObjects = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer obj in darkenObjects)
        {
            if (obj.gameObject != gameObject)
                obj.color = Color.white;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    private void PlayStepSound()
    {
        AudioSource.PlayClipAtPoint(runSFX, Vector2.zero,0.5f);
    }

    private void PlayBladeSFX()
    {
        AudioSource.PlayClipAtPoint(bladeSFX, Vector2.zero);
    }

    private void PlayJumpSFX()
    {
        AudioSource.PlayClipAtPoint(jumpSFX, Vector2.zero);
    }

    private void PlayRollSFX()
    {
        AudioSource.PlayClipAtPoint(rollSFX, Vector2.zero);
    }
}