using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool isOnGround;
    private bool isRolling;

    private Rigidbody2D selfRigidBody;
    private Animator selfAnimator;

    [SerializeField]
    private float forceFactor;

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
        selfAnimator.SetBool("Jump", isOnGround);
        selfAnimator.SetBool("Rolling", isRolling);
    }

    private void PlayerInput()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !isOnGround)
        {
            isOnGround = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && isRolling == false)
        {
            isRolling = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
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
}
