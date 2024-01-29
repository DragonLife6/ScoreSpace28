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
                collision.gameObject.SendMessage("ApplyBonusExpirience", expienceAmount);

                Destroy(gameObject);
            }
        }
    }
}
