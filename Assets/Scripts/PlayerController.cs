using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    //Delete
    public float distance = 0.1f;
    //


    public int score;
    public Text scoreText;

    public int PlayerNum;

    [SerializeField] Transform[] pointsSpawn;


    public AnimationCurve damageAnimationCurve;

    [SerializeField] private float Speed = 10f;
    [SerializeField] private float forceJump = 100f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform parentWeapon;
    [SerializeField] private HealthController Health;
    [SerializeField] private GameObject crown;


    private bool isLooksToRight = true;
    private bool isGround;
    private bool isHook;
    public bool isDead = true;
    private bool movement = true;
    public bool isWeaponActive;

    public bool isKing;

    public Rigidbody2D rb;
    private Animator animator;

    private SpawnerWeapon spawner;

    private Vector2 directionJump;

    private List<Weapon> weapons;
    private Weapon currentWeapon;



    [SerializeField] GameObject[] prefabsWeapon;
    [SerializeField] GameObject CronwPlayer;

    private void Awake()
    {
        CronwPlayer.SetActive(false);
        weapons = new List<Weapon>();
        for (int i = 0; i < prefabsWeapon.Length; i++)
        {
            weapons.Add(Instantiate(prefabsWeapon[i], parentWeapon.position, Quaternion.identity, parentWeapon).GetComponent<Weapon>());
        }
        currentWeapon = weapons[1];

    }
    private void Start()
    {
        scoreText.text = score.ToString();
        MoveToSpawn();
        isDead = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        Health = GetComponent<HealthController>();
        StartCoroutine(Spawn());
    }

    public void counterScore(bool plus)
    {
        score += plus ? 1 : -1;
        scoreText.text = score.ToString();
    }

    private void MoveToSpawn()
    {
        if (pointsSpawn.Length > 0)
        {
            transform.position = pointsSpawn[Random.Range(0, pointsSpawn.Length)].position;
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.2f);
        isDead = false;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (spawner != null && !isWeaponActive)
            {
                ActiovationWeapon(spawner.index);
                spawner.DeactiveWeapon();
                isWeaponActive = true;
            }


            else if (spawner != null && Input.GetButtonDown("p" + PlayerNum + "TakeWeapon"))
            {
                ActiovationWeapon(spawner.index);
                spawner.DeactiveWeapon();
                isWeaponActive = true;
            }



            if (Input.GetButton("p" + PlayerNum + "Fire") && currentWeapon != null && currentWeapon.isActiveAndEnabled)
            {
                currentWeapon.OpenFire();
            }
            if (movement) UpdateJumping();
            AnimationMove();
            if (rb.velocity.x > 0.1f && !isLooksToRight) Flip();
            else if (rb.velocity.x < -0.1f && isLooksToRight) Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (movement)
            {
                float axis = Input.GetAxis("p" + PlayerNum + "Horizontal");
                if (Mathf.Abs(axis) > 0.3f)
                    rb.velocity = new Vector2(axis * Speed, rb.velocity.y);
                else
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
        }
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
        animator.SetBool("IsHook", isHook && !isGround);
        animator?.SetBool("Run", Mathf.Abs(rb.velocity.x) > 0.1f && isGround);

    }

    private void UpdateJumping()
    {
        if (Input.GetButtonDown("p" + PlayerNum + "Jump") && (isGround || isHook))
        {
            if (isHook) StartCoroutine(BlockMovmentX(0.3f));
            // rb.velocity = new Vector2(directionJump.x * 10, 10);
            rb.AddForce((Vector2.up + directionJump * 0.5f) * forceJump, ForceMode2D.Impulse);
            animator?.SetTrigger("Jump");

        }
    }


    IEnumerator BlockMovmentX(float time)
    {
        movement = false;
        yield return new WaitForSeconds(time);
        movement = true;
    }


    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);
        isGround = hit.collider != null;
    }

    public void ActiovationWeapon(int index)
    {
        animator.SetTrigger("ChangeWeapon");
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }
        currentWeapon = weapons[index];
        currentWeapon.gameObject.SetActive(true);
        isWeaponActive = true;
    }

    public void DamageEffect(Vector2 direction)
    {
        if (!isDead)
        {
            StartCoroutine(BlockMovmentX(0.3f));
            rb.velocity = direction * 10;
            animator.SetTrigger("Damage");
        }
    }

    public void Dead()
    {
        if (!isDead)
        {
            animator.SetTrigger("Dead");
            StartCoroutine(Respawn());
            if (isKing) LoseCrown();
        }
    }


    public void BecomeKing()
    {
        isKing = true;
        CronwPlayer.SetActive(true);
    }
    public void LoseCrown()
    {
        Instantiate(crown, transform.position, Quaternion.identity);
        CronwPlayer.SetActive(false);
        isKing = false;
    }

    IEnumerator Respawn()
    {
        Health.Respawn();
        currentWeapon.gameObject.SetActive(false);
        isWeaponActive = false;
        yield return new WaitForSeconds(2.2f);
        MoveToSpawn();
        yield return new WaitForSeconds(1.2f);
        isDead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            directionJump = new Vector2(collision.GetContact(0).normal.x, 0);
            isHook = directionJump.x != 0;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            directionJump = new Vector2(collision.GetContact(0).normal.x, 0);
            isHook = directionJump.x != 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isHook = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            spawner = collision.GetComponent<SpawnerWeapon>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            spawner = null;
        }
    }
}
