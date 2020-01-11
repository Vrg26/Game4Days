using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWeapon : MonoBehaviour
{
    public int index;
    public float timeForChange = 1f;
    public Sprite[] spriteWeapon;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(RandomWeapon());
    }

    IEnumerator RandomWeapon()
    {
        while (true)
        {
            index = Random.Range(0, spriteWeapon.Length);
            spriteRenderer.sprite = spriteWeapon[index];
            yield return new WaitForSeconds(timeForChange);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().ActiovationWeapon(index);
        }
    }
}
