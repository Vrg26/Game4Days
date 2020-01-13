using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float shootRate = 1;
    [SerializeField] private float range = 3f;
    [SerializeField] private float speedBulletFly = 10;
    [SerializeField] private int maxNumberbullet = 10;
                     private int numberBulletNow;

    [SerializeField] private ParticleSystem shootEffect;
    [SerializeField] private Transform firePoint;


    [SerializeField] private GameObject prefabBullet;

    public bool isActivWeapon;

    private Animator animator;
    private PlayerController player;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private bool isShooting;

    public void OpenFire()
    {
        


        if (!isShooting)
        {
            StartCoroutine(Shoot());
            numberBulletNow--;
        }
    }

    private void OnDisable()
    {
        numberBulletNow = maxNumberbullet - 1;
    }
    private void OnEnable()
    {
        numberBulletNow = maxNumberbullet - 1;
        isShooting = false;
    }
    IEnumerator Shoot()
    {
        shootEffect.Play();
        isShooting = true;
       // animator?.SetTrigger("Shoot");
        Bullet bullet = Instantiate(prefabBullet, firePoint.position, Quaternion.identity).GetComponent<Bullet>();

        //speedBulletFly += player.rb.velocity.x;

        bullet.Push(transform.right, range, speedBulletFly , damage,player);
        if(numberBulletNow <= 0)
        {
            player.isWeaponActive = false;
            gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

}
