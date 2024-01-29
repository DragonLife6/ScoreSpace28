using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObjectScript : MonoBehaviour
{
    [SerializeField] float healingAmount = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.CompareTag("Player"))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Powerup");
                collision.gameObject.SendMessage("ApplyHealing", healingAmount);

                Destroy(gameObject);
            }
        }
    }
}
