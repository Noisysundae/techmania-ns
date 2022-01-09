using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultBgBrightnessSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI display;

    private void OnEnable()
    {
        MemoryToUI();
    }

    private void MemoryToUI()
    {
        slider.SetValueWithoutNotify(
            Options.instance.defaultBackgroundBrightness);
        RefreshBrightnessDisplay();
    }

    private void RefreshBrightnessDisplay()
    {
        display.text = Options.instance
            .defaultBackgroundBrightness.ToString();
    }

    public void UIToMemory()
    {
        Options.instance.defaultBackgroundBrightness =
            Mathf.FloorToInt(slider.value);
        RefreshBrightnessDisplay();
    }
}
