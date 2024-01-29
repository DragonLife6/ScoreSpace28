using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject damagePopup;
    HitFlashScript hitFlash;
    bool isAlive = true;

    [SerializeField] float enemyMaxHealth = 10;
    [SerializeField] int scorePoints = 1;
    float enemyHealth;

    [SerializeField] Vector3 basicScale;

    private void Start()
    {
        enemyHealth = enemyMaxHealth;
        hitFlash = GetComponent<HitFlashScript>();

        Destroy(gameObject, 30f);
    }

    public void ApplyDamage(float damage)
    {
        if (isAlive)
        {
            enemyHealth -= damage;

            if (enemyHealth <= 0)
            {
                //AUDIO SPIDER DEAD
                FMODUnity.RuntimeManager.PlayOneShot("event:/SpiderDead");

                isAlive = false;
                GameObject player = GameObject.FindWithTag("Player");
                player.SendMessage("ApplyScore", scorePoints);
                player.SendMessage("ApplyExpirience", scorePoints);

                DamagePopupScript.Create(damagePopup, transform.position, "+" + scorePoints.ToString(), false, true);
                Destroy(gameObject);
            } 
            DamagePopupScript.Create(damagePopup, transform.position, Mathf.FloorToInt(damage).ToString(), false, false);
            hitFlash.HitFlash();
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
                player.SendMessage("ApplyScore", scorePoints);
                player.SendMessage("ApplyExpirience", scorePoints);

                DamagePopupScript.Create(damagePopup, transform.position, "+" + scorePoints.ToString(), false, true);
                Destroy(gameObject);
            }
            DamagePopupScript.Create(damagePopup, transform.position, Mathf.FloorToInt(damage).ToString(), true, false);
            hitFlash.HitFlash();
        }
    }

    public void ApplyMaxHealthCoef(float coef)
    {
        enemyMaxHealth *= coef;
        enemyHealth = enemyMaxHealth;
    }

    public void ApplySizeCoef(float coef)
    {
        if (coef == 0)
        {
            transform.localScale = basicScale;
        }
        else if (coef <= 1.5f)
        {
            transform.localScale = basicScale * coef;
        }
        else
        {
            transform.localScale = basicScale * 1.5f;
        }
    }
}
