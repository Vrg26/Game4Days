using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    [SerializeField] private float forceJump = 100f;
    [SerializeField] private LayerMask groundLayer;
    private bool isLooksToRight = true;
    private bool isGround;

    private Rigidbody2D rb;
    private Animator animator;


    //Delete
    public float distance = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        UpdateJumping();
        AnimationMove();
        if (rb.velocity.x > 0.1f && !isLooksToRight) Flip();
        else if (rb.velocity.x < -0.1f && isLooksToRight) Flip();
    }

    private void Flip()
    {
        isLooksToRight = !isLooksToRight;
        transform.Rotate(new Vector3(0, 180, 0));
    }
    private void AnimationMove()
    {
        animator?.SetBool("Run",Mathf.Abs(rb.velocity.x) > 0.2f && isGround);
    }

    private void UpdateJumping()
    {
        CheckGround();
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
            animator?.SetTrigger("Jump");
        }
    }
    private void FixedUpdate()
    {
        float axis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(axis * Speed, rb.velocity.y);
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);
        isGround = hit.collider != null;
    }
}
