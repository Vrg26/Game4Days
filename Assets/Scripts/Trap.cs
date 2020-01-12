using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Work1");
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Work");
            collision.gameObject.GetComponent<HealthController>().TakeDamage(100f, transform.position);
        }
    }
}
