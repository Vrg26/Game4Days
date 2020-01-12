using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHp;
    private int hp;
    private PlayerController player;

    private void Start()
    {
        hp = maxHp;
        player = GetComponent<PlayerController>();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

    }
}
