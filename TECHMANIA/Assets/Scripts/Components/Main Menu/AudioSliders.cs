using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSliders : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public TextMeshProUGUI masterVolumeDisplay;
    public Slider musicVolumeSlider;
    public TextMeshProUGUI musicVolumeDisplay;
    public Slider keysoundVolumeSlider;
    public TextMeshProUGUI keysoundVolumeDisplay;
    public Slider sfxVolumeSlider;
    public TextMeshProUGUI sfxVolumeDisplay;
    public AudioMixer audioMixer;

    private void OnEnable()
    {
        MemoryToUI();
    }

    private void MemoryToUI()
    {
        masterVolumeSlider.SetValueWithoutNotify(
            Options.instance.masterVolumePercent);
        musicVolumeSlider.SetValueWithoutNotify(
            Options.instance.musicVolumePercent);
        keysoundVolumeSlider.SetValueWithoutNotify(
            Options.instance.keysoundVolumePercent);
        sfxVolumeSlider.SetValueWithoutNotify(
            Options.instance.sfxVolumePercent);
        UpdateVolumeDisplay();
    }

    private void UpdateVolumeDisplay()
    {
        masterVolumeDisplay.text =
            ((int) Options.instance.masterVolumePercent).ToString();
        musicVolumeDisplay.text = 
            ((int) Options.instance.musicVolumePercent).ToString();
        keysoundVolumeDisplay.text = 
            ((int) Options.instance.keysoundVolumePercent).ToString();
        sfxVolumeDisplay.text = 
            ((int) Options.instance.sfxVolumePercent).ToString();
    }

    public void ApplyVolume()
    {
        audioMixer.SetFloat("MasterVolume",
            (float) AudioSourceManager.AmpToDecibel(
                Options.instance.masterVolumePercent / 100d));
        audioMixer.SetFloat("MusicVolume",
            (float) AudioSourceManager.AmpToDecibel(
                Options.instance.musicVolumePercent / 100d));
        audioMixer.SetFloat("KeysoundVolume",
            (float) AudioSourceManager.AmpToDecibel(
                Options.instance.keysoundVolumePercent / 100d));
        audioMixer.SetFloat("SfxVolume",
            (float)(
                AudioSourceManager.AmpToDecibel(
                    Options.instance.sfxVolumePercent / 100d)
                + AudioSourceManager.kBaseSfxGain));
    }

    public void OnVolumeChanged()
    {
        Options.instance.masterVolumePercent =masterVolumeSlider.value;
        Options.instance.musicVolumePercent = musicVolumeSlider.value;
        Options.instance.keysoundVolumePercent = keysoundVolumeSlider.value;
        Options.instance.sfxVolumePercent = sfxVolumeSlider.value;

        UpdateVolumeDisplay();
        ApplyVolume();
    }
}
