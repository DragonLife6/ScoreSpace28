using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool isAlive = true;

    HitFlashScript hitFlashScript;
    [SerializeField] SliderScript hpSlider;
    [SerializeField] float maxPlayerHP = 100f;
    float currentPlayerHP;

    PlayerScore playerScore;

    [SerializeField] GameObject deathMenu;

    //AUDIO
    public GameObject audioManager;

    private void Awake()
    {
        if(deathMenu.activeInHierarchy)
        {
            deathMenu.SetActive(false);
        }
    }

    private void Start()
    {
        currentPlayerHP = maxPlayerHP;
        hpSlider.UpdateSlider(currentPlayerHP, maxPlayerHP);
        playerScore = GetComponent<PlayerScore>();
        hitFlashScript = GetComponent<HitFlashScript>();
    }

    public void GetDamage(float damage)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerHurt");
        currentPlayerHP -= damage;
        hpSlider.UpdateSlider(currentPlayerHP, maxPlayerHP);
        hitFlashScript.HitFlash();

        if (currentPlayerHP <= 0)
        {
            OnPlayerDeath();
            Debug.Log("Player is dead now!");
        }
    }

    void OnPlayerDeath()
    {
        StartCoroutine(playerScore.SendScore());
        Destroy(gameObject);
        Destroy(audioManager);
        Time.timeScale = 0f;
        deathMenu.SetActive(true);
    }
}
