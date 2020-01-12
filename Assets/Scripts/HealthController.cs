using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHp;
    private float hp;
    private PlayerController player;

    private void Start()
    {
        hp = maxHp;
        player = GetComponent<PlayerController>();
    }
    public void Respawn()
    {
        hp = maxHp;
    }
    public void TakeDamage(float damage , Vector3 posBullet)
    {
        Vector2 direction = new Vector2(transform.position.x - posBullet.x, 0.5f);
        hp -= damage;
        Debug.Log(hp);
        if(hp <= 0)
        {
            player.Dead();
            player.isDead = true;
        }
        else
        {
            player.DamageEffect(direction);
        }
    }
}
