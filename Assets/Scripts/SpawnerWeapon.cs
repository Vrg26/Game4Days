using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWeapon : MonoBehaviour
{
    public int index;
    public float timeForChange = 1f;
    public Sprite[] spriteWeapon;
    [SerializeField] private ParticleSystem takeEffect;
    [SerializeField] private GameObject weaponObject;
    [SerializeField] private SpriteRenderer spriteRendererWeapon;
    public BoxCollider2D trigger;

    private void Awake()
    {
        takeEffect.Stop();
    }

    private void Start()
    {
        ActivationRandomWeapon();
    }

    private void DeactiveWeapon()
    {
        trigger.enabled = false;
        takeEffect.Play();
        weaponObject.SetActive(false);
        StartCoroutine(ActivationWeapon());
    }

    IEnumerator ActivationWeapon()
    {
        yield return new WaitForSeconds(timeForChange);
        
        ActivationRandomWeapon();
    }

    private void ActivationRandomWeapon()
    {
           if(!trigger.enabled) trigger.enabled = true;
            weaponObject.SetActive(true);
            index = Random.Range(0, spriteWeapon.Length);
            spriteRendererWeapon.sprite = spriteWeapon[index];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().ActiovationWeapon(index);
            DeactiveWeapon();
        }
    }
}
