using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BgBrightnessSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI display;

    private void OnEnable()
    {
        MemoryToUI();
    }

    private void MemoryToUI()
    {
        if (Options.instance.forceDefaultBackgroundSettings)
        {
            slider.SetValueWithoutNotify(
                Options.instance.defaultBackgroundBrightness);
        }
        else
        {
            slider.SetValueWithoutNotify(
                GameSetup.trackOptions.backgroundBrightness);
        }
        RefreshBrightnessDisplay();
    }

    private void RefreshBrightnessDisplay()
    {
        if (Options.instance.forceDefaultBackgroundSettings)
        {
            display.text = Options.instance
                .defaultBackgroundBrightness.ToString();
        }
        else
        {
            display.text = GameSetup.trackOptions
                .backgroundBrightness.ToString();
        }
    }

    public void UIToMemory()
    {
        if (Options.instance.forceDefaultBackgroundSettings)
        {
            Options.instance.defaultBackgroundBrightness =
                Mathf.FloorToInt(slider.value);
        }
        else
        {
            GameSetup.trackOptions.backgroundBrightness =
                Mathf.FloorToInt(slider.value);
        }
        RefreshBrightnessDisplay();
    }
}
