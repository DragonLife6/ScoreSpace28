using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    float damage = 10f;
    float moveSpeed = 15f;
  

    private void Start()
    {
        Destroy(gameObject, 15f);
    }

    private void Update()
    {
        Vector3 moveDirection = transform.rotation * Vector3.right;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SendMessage("ApplyDamage", damage);
            Destroy(gameObject);
        }
    }
}
