using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpirience : MonoBehaviour
{
    int playerExpirience = 0;
    [SerializeField] int expForLevelUP;
    PlayerGunController gunController;

    [SerializeField] SliderScript xpSlider;

    // Start is called before the first frame update
    void Start()
    {
        gunController = GetComponent<PlayerGunController>();
        playerExpirience = 0;
        xpSlider.UpdateSlider(playerExpirience, expForLevelUP);
    }

    public void ApplyExpirience(int newExp)
    {
        playerExpirience += newExp;

        if (playerExpirience >= expForLevelUP)
        {
            playerExpirience = 0;
            expForLevelUP = Mathf.FloorToInt(expForLevelUP * 1.1f);

            LevelUP();
        }

        xpSlider.UpdateSlider(playerExpirience, expForLevelUP);
    }

    private void LevelUP()
    {
        gunController.UpgradeRandomParameter();
    }
}
