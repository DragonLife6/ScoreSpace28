using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuScript : MonoBehaviour
{
    [SerializeField] TMP_Text[] variantsTitles;
    [SerializeField] TMP_Text[] variantsDescriptions;
    [SerializeField] Image[] variantsImages;

    [SerializeField] PlayerGunController playerGun;
    int[] variantsID = new int[3];

    public void SetupUpgradeMenu(GunCharacteristicSerializable[] variants)
    {
        for (int i = 0; i < 3; i++)
        {
            variantsID[i] = variants[i].GetId();
            variantsTitles[i].text = variants[i].GetName();
            variantsDescriptions[i].text = variants[i].GetDescription();
            variantsImages[i].sprite = variants[i].GetImage();
        }
    }

    public void OnVariantClicked(int buttonNum)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        playerGun.UpgradeParameter(variantsID[buttonNum]);
    }
}
