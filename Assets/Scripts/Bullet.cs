using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    public void Push(Vector2 direction, float lifeTime, float force, float damage)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Force);
        this.damage = damage;
        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Damage! " + damage);
            Destroy(gameObject);
        }
    }
}
