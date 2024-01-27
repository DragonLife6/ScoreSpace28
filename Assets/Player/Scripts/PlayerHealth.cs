using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool isAlive = true;

    [SerializeField] float maxPlayerHP = 100f;
    float currentPlayerHP;

    PlayerScore playerScore;

    private void Start()
    {
        currentPlayerHP = maxPlayerHP; 
        playerScore = GetComponent<PlayerScore>();
    }

    public void GetDamage(float damage)
    {
        currentPlayerHP -= damage;

        if(currentPlayerHP <= 0)
        {
            OnPlayerDeath();
            Debug.Log("Player is dead now!");
        }
    }

    void OnPlayerDeath()
    {
        StartCoroutine(playerScore.SendScore());
        Destroy(gameObject);

        // StartCoroutine(RestartLevel()); // Restart level on player death
    }
}
