using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    //Delete
    public float distance = 0.1f;
    //

    [SerializeField] private float Speed = 10f;
    [SerializeField] private float forceJump = 100f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform parentWeapon;
    private bool isLooksToRight = true;
    private bool isGround;

    private Rigidbody2D rb;
    private Animator animator;

   

    private List<Weapon> weapons;
    private Weapon currentWeapon;

    

    [SerializeField] GameObject[] prefabsWeapon;

    private void Awake()
    {
        weapons = new List<Weapon>();
        for (int i = 0; i < prefabsWeapon.Length; i++)
        {
            weapons.Add(Instantiate(prefabsWeapon[i], parentWeapon.position, Quaternion.identity, parentWeapon).GetComponent<Weapon>());
        }
        currentWeapon = weapons[1];
        
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentWeapon != null && currentWeapon.isActiveAndEnabled)
        {
            currentWeapon.OpenFire();
        }
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
        CheckGround();
        animator.SetBool("IsGround", isGround);
        if (isGround)
        {
            animator?.SetBool("Run", Mathf.Abs(rb.velocity.x) > 0.1f);
        }
    }

    private void UpdateJumping()
    {
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

    public void ActiovationWeapon(int index)
    {
        if(currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }
        currentWeapon = weapons[index];
        currentWeapon.gameObject.SetActive(true);
    }
}
