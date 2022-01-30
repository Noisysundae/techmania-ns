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
        if (Options.instance.alwaysUseDefaultBackgroundSettings)
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
        if (Options.instance.alwaysUseDefaultBackgroundSettings)
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
        if (Options.instance.alwaysUseDefaultBackgroundSettings)
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

    public void OptionsUIToMemory()
    {
        Options.instance.defaultBackgroundBrightness =
            Mathf.FloorToInt(slider.value);
        display.text = Options.instance
            .defaultBackgroundBrightness.ToString();
    }
}
