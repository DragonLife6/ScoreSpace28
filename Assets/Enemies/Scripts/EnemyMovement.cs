using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float damage = 10f;

    public bool deathOnCollision = false;
    
    EnemiesManager enemiesManager;
    
    //Animator animator;

    //public float attackDistance = 1f;
    //[SerializeField] Collider2D attackCollider;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //animator = GetComponent<Animator>();
        enemiesManager = GameObject.Find("EnemyManager").GetComponent<EnemiesManager>();
    }

    void Update()
    {
        Vector3 moveDirection;

        moveSpeed += 0.01f * Time.deltaTime;

        if (player != null)
        {
            moveDirection = (player.position - transform.position);
        } else
        {
            moveDirection = (Vector3.zero - transform.position);
        }

        if (deathOnCollision)
        {
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
        
        if ((moveDirection.x < 0 && transform.localScale.x < 0) || (moveDirection.x > 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        
    }

    public void ApplyDamageCoef(float coef)
    {
        damage *= coef;
    }
    public void ApplySpeedCoef(float coef)
    {
        moveSpeed *= coef;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Нанесення пошкодження гравцю при контакті
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.GetDamage(damage);
            }

            if (deathOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnDestroy()
    {
        if(transform != null && enemiesManager != null)
            enemiesManager.DeleteEnemy(transform);
    }
}
