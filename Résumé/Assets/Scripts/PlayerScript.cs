using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool isOnGround;
    private bool isRolling;
    private bool isSlashing;

    private Color attackSphereColor;

    private Rigidbody2D selfRigidBody;
    private Animator selfAnimator;

    [SerializeField]
    private float forceFactor;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private Transform attackPosition;

    private void Awake()
    {
        attackSphereColor = Color.red;
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
            if (selfRigidBody.velocity.y <= 0)
                isOnGround = false;
        }
    }

    private void Jump()
    {
        selfRigidBody.AddForce(new Vector2(0, 1) * forceFactor,ForceMode2D.Impulse);
    }

    private void EndRolling()
    {
        isRolling = false;
    }

    private void EndSlashing()
    {
        isSlashing = false;
        attackSphereColor = Color.red;
    }

    private void Attack()
    {
        attackSphereColor = Color.blue;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange);
        //send message to enemies
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = attackSphereColor;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
