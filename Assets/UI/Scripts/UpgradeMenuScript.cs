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

    int selectedVariant;
    [SerializeField] Button confirmButton;

    private void Start()
    {
        confirmButton.interactable = false;
    }

    public void SetupUpgradeMenu(GunCharacteristicSerializable[] variants)
    {
        selectedVariant = -1;
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
        selectedVariant = buttonNum;
        confirmButton.interactable = true;
    }

    public void OnConfirm()
    {
        if(selectedVariant >= 0)
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            playerGun.UpgradeParameter(variantsID[selectedVariant]);
            confirmButton.interactable = false;
        }
    }
}
