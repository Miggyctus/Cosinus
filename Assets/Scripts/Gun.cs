using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    public Transform gunBarrel;
    [SerializeField] public AudioSource shootSound;

    float timeSinceLastShot;
    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        shootSound = GetComponent<AudioSource>();

    }
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void StartReload()
    {
        if(!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        shootSound.Stop();

        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }
    public void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                Transform cam = GetComponentInParent<Camera>().transform;
                //instantiate new bullet
                GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, transform.rotation);
                //calculate the direction to the player
                Vector3 shootDirection = cam.forward;
                //add force to rigidbody of the bullet
                bullet.GetComponent<Rigidbody>().velocity = shootDirection * 40;
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                shootSound.Play();
                OnGunShot();
            }
        }
    }
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        //Debug.DrawRay(transform.position, transform.forward);
    }
    private void OnGunShot()
    {
        
    }
}
