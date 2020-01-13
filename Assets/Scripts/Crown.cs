﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    public ParticleSystem destroyCrownEffects;
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
        }
    }
}
