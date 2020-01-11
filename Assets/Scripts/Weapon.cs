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

    [SerializeField] private Transform firePoint;


    [SerializeField] private GameObject prefabBullet;

    public bool isActivWeapon;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private bool isShooting;

    public void OpenFire()
    {
        


        if (!isShooting)
        {
            if (numberBulletNow > 0) numberBulletNow--;
            else
            {
                gameObject.SetActive(false);
                return;
            }
            StartCoroutine(Shoot());
        }
    }

    private void OnDisable()
    {
        numberBulletNow = maxNumberbullet;
    }
    private void OnEnable()
    {
        numberBulletNow = maxNumberbullet;
        isShooting = false;
    }
    IEnumerator Shoot()
    {
        isShooting = true;
       // animator?.SetTrigger("Shoot");
        Bullet bullet = Instantiate(prefabBullet, firePoint.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Push(transform.right, range, speedBulletFly, damage);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

}
