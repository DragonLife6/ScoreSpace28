using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    
    Animator animator;
    PlayerHealth playerHealth;

    //public AudioClip footstepsSound;
    //private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        //audioSource = GetComponent<AudioSource>();
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
            
            
            /* Player walk animation and sound
            if (movement != Vector3.zero)
            {
                animator.SetFloat("speed", moveSpeed);
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(footstepsSound);
                }
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                animator.SetFloat("speed", 0);
            }
            */

            transform.Translate(movement);

            // Player rotation to movement direction
            if (horizontalInput > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (horizontalInput < 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
