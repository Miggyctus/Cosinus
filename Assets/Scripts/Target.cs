using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public float health = 100f;
    private bool damaged;
    public void TakeDamage(float damage)
    {
        health -= damage;
        damaged = true;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public bool TookDamage()
    {
        return damaged;
    }
}
