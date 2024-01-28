using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    float damage = 10f;
    float moveSpeed = 15f;

    int bulletMaxPenetrations = 1;
    int currentPenetration = 0;
    float bulletCritChance = 0f;

    private void Start()
    {   
        //AUDIO
        FMODUnity.RuntimeManager.PlayOneShot("event:/Gunshot");

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        Vector3 moveDirection = transform.rotation * Vector3.right;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    public void SetParameters(float newDamage, float critChance, float penetration)
    {
        damage = newDamage;
        bulletCritChance = critChance;
        bulletMaxPenetrations = Mathf.FloorToInt(penetration);
        currentPenetration = 0;
    }

    private bool CheckCritDamage()
    {
        return Random.Range(0f, 1f) <= bulletCritChance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            currentPenetration++;
            if (CheckCritDamage())
            {
                other.gameObject.SendMessage("ApplyCritDamage", damage * 1.5f);
            }
            else
            {
                other.gameObject.SendMessage("ApplyDamage", damage);
            }

            if (currentPenetration >= bulletMaxPenetrations)
            {
                Destroy(gameObject);
            }
        }
    }
}