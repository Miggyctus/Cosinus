using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] GunData gunDataPlayer;
    [SerializeField] GunData gunDataEnemy;
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if(hitTransform.CompareTag("Player"))
        {
            Debug.Log("AHI TENES PUTOOO");
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(gunDataEnemy.damage);
        }
        else if (hitTransform.GetComponent<IDamageable>() != null)
        {
            hitTransform.GetComponent<IDamageable>().TakeDamage(gunDataPlayer.damage);
            
        }
        Destroy(gameObject);
    }
}
