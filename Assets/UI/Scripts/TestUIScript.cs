using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestUIScript : MonoBehaviour
{
    [SerializeField] TMP_Text[] textFields;
    [SerializeField] PlayerGunController gun;

    private void Update()
    {
        List<float> data = gun.GetGunData();

        for (int i = 0; i< data.Count; i++)
        {
            textFields[i].text = data[i].ToString();
        }
    }
}
