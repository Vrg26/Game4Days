using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    public GameObject destroyCrownEffects;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (!player.isDead)
            {
                player.BecomeKing();
                if (destroyCrownEffects != null) Instantiate(destroyCrownEffects, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            return;
        }
        if (collision.gameObject.tag == "Lawa")
        {
            transform.position = new Vector3(0, 0.3f, 0);
            if (destroyCrownEffects != null) Instantiate(destroyCrownEffects, transform.position, Quaternion.identity);
        }
    }
}
