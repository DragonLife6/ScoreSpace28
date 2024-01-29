using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusExpirienceObjectScript : MonoBehaviour
{
    [SerializeField] int expienceAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Powerup");
                collision.gameObject.SendMessage("ApplyBonusExpirience", expienceAmount);

                Destroy(gameObject);
            }
        }
    }
}
