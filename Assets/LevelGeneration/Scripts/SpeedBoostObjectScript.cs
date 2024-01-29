using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostObjectScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Powerup");
                collision.gameObject.SendMessage("ApplySpeedBoost");

                Destroy(gameObject);
            }
        }
    }
}
