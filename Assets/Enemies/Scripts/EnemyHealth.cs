using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject damagePopup;
    bool isAlive = true;

    [SerializeField] float enemyMaxHealth = 10;
    float enemyHealth;

    private void Start()
    {
        enemyHealth = enemyMaxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (isAlive)
        {
            enemyHealth -= damage;

            if (enemyHealth <= 0)
            {
                isAlive = false;
                GameObject player = GameObject.FindWithTag("Player");
                player.SendMessage("ApplyScore", 1);
                player.SendMessage("ApplyExpirience", 1);

                DamagePopupScript.Create(damagePopup, transform.position, "+1", false, true);
                DamagePopupScript.Create(damagePopup, transform.position, Mathf.FloorToInt(damage).ToString(), false, false);
                Destroy(gameObject);
            } else
            {
                DamagePopupScript.Create(damagePopup, transform.position, Mathf.FloorToInt(damage).ToString(), false, false);
            }
        }
    }

    public void ApplyCritDamage(float damage)
    {
        if (isAlive)
        {
            enemyHealth -= damage;

            if (enemyHealth <= 0)
            {
                isAlive = false;
                GameObject player = GameObject.FindWithTag("Player");
                player.SendMessage("ApplyScore", 1);
                player.SendMessage("ApplyExpirience", 1);

                DamagePopupScript.Create(damagePopup, transform.position, "+1", false, true);
                DamagePopupScript.Create(damagePopup, transform.position, Mathf.FloorToInt(damage).ToString(), true, false);
                Destroy(gameObject);
            }
            else
            {
                DamagePopupScript.Create(damagePopup, transform.position, Mathf.FloorToInt(damage).ToString(), true, false);
            }
        }
    }

    public void ApplyMaxHealthCoef(float coef)
    {
        enemyMaxHealth *= coef;
        if (coef <= 2f)
        {
            transform.localScale = Vector3.one * coef;
        } else
        {
            transform.localScale = Vector3.one * 2f;
        }
        enemyHealth = enemyMaxHealth;
    }
}
