using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Work1");
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Work");
            collision.GetComponent<HealthController>().TakeDamage(100f, transform.position);
        }
    }
}
