using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{


    private float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;


    [SerializeField] private float regenCooldown = 3.0f;
    [SerializeField] private float maxHealCooldown = 3.0f;
    [SerializeField] private bool startCD = false;
    [SerializeField] private float regenRate = 0.2f;
    private bool canRegen = false;

    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    TakeDamage(Random.Range(5, 10));
        //}
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    RestoreHealth(Random.Range(5, 10));
        //}

        if (startCD)
        {
            regenCooldown -= Time.deltaTime;
            if (regenCooldown <= 0)
            {
                canRegen = true;
                startCD = false;
            }
        }

        if (canRegen)
        {
            if (health <= maxHealth - 0.01)
            {
                health += Time.deltaTime + regenRate;
            }
            else
            {
                health = maxHealth;
                regenCooldown = maxHealCooldown;
                canRegen = false;
            }

        }
    }
    public void UpdateHealthUI()
    {
        //Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }


    }
    public void TakeDamage(float damage)
    {
        canRegen = false;
        health -= damage;
        lerpTimer = 0f;
        regenCooldown = maxHealCooldown;
        startCD = true;
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
    public float remainingHealth()
    {
        return health;
    }
}