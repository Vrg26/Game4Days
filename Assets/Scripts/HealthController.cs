using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHp;
    public float hp;
    public Transform lineHealth;
    private PlayerController player;

    private void Start()
    {
        hp = maxHp;
        if(lineHealth != null)
            lineHealth.localScale = new Vector3(0.5f, 1, 1);
        player = GetComponent<PlayerController>();
    }
    public void Respawn()
    {
        hp = maxHp;
    }
    private void Update()
    {
        if (lineHealth != null)
            lineHealth.localScale = new Vector3(hp / 100f, 1, 1);
    }
    public void TakeDamage(float damage , Vector3 posBullet, PlayerController OtherPlayer = null)
    {
        Vector2 direction = new Vector2(transform.position.x - posBullet.x, 0.5f);
        if (!player.isDead)
        {
            hp -= damage;
            if (hp <= 0)
            {
                if (OtherPlayer != null) OtherPlayer.counterScore(true);
                player.Dead();
                hp = maxHp; // delete in playerController
                player.isDead = true;
            }
            else
            {
                player.DamageEffect(direction);
            }
        }
    }
}
