using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWeapon : MonoBehaviour
{
    public int index;
    private float timeForChange = 1f;
    [SerializeField] private float maxTime = 10f, minTime = 1f;
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

    public void DeactiveWeapon()
    {
        trigger.enabled = false;
        takeEffect.Play();
        weaponObject.SetActive(false);
        StartCoroutine(ActivationWeapon());
    }

    IEnumerator ActivationWeapon()
    {
        timeForChange = Random.Range(minTime, maxTime);
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
}
