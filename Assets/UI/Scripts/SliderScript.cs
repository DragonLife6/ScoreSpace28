using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void UpdateSlider(float value, float maxValue)
    {
        slider.value = value / maxValue;
    }
}
