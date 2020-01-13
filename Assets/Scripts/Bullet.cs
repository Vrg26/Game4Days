using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    [SerializeField] GameObject[] effectsDestroy;
    public bool isRotation;
    public bool isDisk;
    Rigidbody2D rb;
    PlayerController player;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Push(Vector2 direction, float lifeTime, float force, float damage, PlayerController player)
    {
        this.player = player;
        rb.AddForce(direction * force, ForceMode2D.Force);
        this.damage = damage;
        if(isRotation)rb.AddTorque(1000f);
        Destroy(gameObject, lifeTime);
    }

    private void DestroyBullet(int index)
    {
        Instantiate(effectsDestroy[index], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (player.isKing)
            {
                collision.GetComponent<HealthController>().TakeDamage(damage, transform.position, player);
            }
            else
            {
                collision.GetComponent<HealthController>().TakeDamage(damage, transform.position);
            }
            DestroyBullet(0);
            return;
        }
        if (isDisk)
        {
            Vector2 directionFly = transform.position - collision.transform.position;
            directionFly.y = Random.Range(0, 2);
            directionFly = directionFly.normalized;
            float rebound = Random.Range(30f, 50f);
            rb.velocity = Vector2.zero;
            rb.AddForce(directionFly  * rebound, ForceMode2D.Impulse);
        }
        else
        {
            DestroyBullet(1);
        }
    }
}
