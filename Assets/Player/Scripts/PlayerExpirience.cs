using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpirience : MonoBehaviour
{
    [SerializeField] GunCharacteristicSerializable[] characteristicsData; // For storing images, titles and descriptions of parameters

    int playerExpirience = 0;
    [SerializeField] int expForLevelUP;
    [SerializeField] UpgradeMenuScript upgradeMenu;

    [SerializeField] SliderScript xpSlider;

    // Start is called before the first frame update
    void Start()
    {
        playerExpirience = 0;
        xpSlider.UpdateSlider(playerExpirience, expForLevelUP);
    }

    public void ApplyExpirience(int newExp)
    {
        playerExpirience += newExp;

        if (playerExpirience >= expForLevelUP)
        {
            playerExpirience = 0;
            expForLevelUP = Mathf.FloorToInt(Mathf.Pow(expForLevelUP, 1.15f));

            LevelUP();
        }

        xpSlider.UpdateSlider(playerExpirience, expForLevelUP);
    }

    public void ApplyBonusExpirience(int amount)
    {
        ApplyExpirience(amount);
    }


    private void LevelUP()
    {
        Time.timeScale = 0f;
        upgradeMenu.gameObject.SetActive(true);
        GunCharacteristicSerializable[] selectedElements = GetRandomElements(characteristicsData, 3);

        upgradeMenu.SetupUpgradeMenu(selectedElements);
    }

    GunCharacteristicSerializable[] GetRandomElements<GunCharacteristicSerializable>(GunCharacteristicSerializable[] array, int count)
    {
        List<GunCharacteristicSerializable> resultList = new List<GunCharacteristicSerializable>();
        List<GunCharacteristicSerializable> sourceList = new List<GunCharacteristicSerializable>(array);

        for (int i = 0; i < Mathf.Min(count, array.Length); i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, sourceList.Count);
            resultList.Add(sourceList[randomIndex]);
            sourceList.RemoveAt(randomIndex);
        }

        return resultList.ToArray();
    }
}
