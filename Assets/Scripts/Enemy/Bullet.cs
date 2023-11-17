using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if(hitTransform.CompareTag("Player"))
        {
            Debug.Log("AHI TENES PUTOOO");
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(10);
        }
        else if (hitTransform.GetComponent<IDamageable>() != null)
        {
            hitTransform.GetComponent<IDamageable>().TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
