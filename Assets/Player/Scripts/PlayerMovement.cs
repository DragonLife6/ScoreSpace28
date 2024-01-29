using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    float basicMoveSpeed;
    
    [SerializeField] Animator animator;
    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        basicMoveSpeed = moveSpeed;
    }


    public void ApplySpeedBoost()
    {
        // Player speed boost sound
        moveSpeed = moveSpeed * 1.5f;
        StartCoroutine(ResetSpeed());
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(5f);
        moveSpeed = basicMoveSpeed;
    }

    void Update()
    {
        if (playerHealth.isAlive)
        {
            Vector3 movement;
            float horizontalInput;
            
            horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;


            //Player walk animation and sound
            if (movement != Vector3.zero)
            {
                animator.SetFloat("Speed", moveSpeed);
                // Steps sounds, I guess
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }
            

            transform.Translate(movement);

            /*// Player rotation to movement direction
            if (horizontalInput > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (horizontalInput < 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }*/
        }
    }
}
