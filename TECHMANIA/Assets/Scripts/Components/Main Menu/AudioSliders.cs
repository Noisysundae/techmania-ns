using System;
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

    private float AmpToScaledDecibel(float amp)
    {
        return (float) AudioSourceManager
            .AmpToDecibel(
                Math.Pow(
                    amp / 100d,
                    2));
    }

    private string VolumeToVolumeText(float percent)
    {
        return ((int) percent).ToString() + "%\n<size=16>"
            + (float) Math.Round(
                AmpToScaledDecibel(percent),
                1,
                MidpointRounding.AwayFromZero)
            + " dB</size>";
    }

    private void UpdateVolumeDisplay()
    {
        masterVolumeDisplay.text =
            VolumeToVolumeText(Options.instance.masterVolumePercent);
        musicVolumeDisplay.text = 
            VolumeToVolumeText(Options.instance.musicVolumePercent);
        keysoundVolumeDisplay.text = 
            VolumeToVolumeText(Options.instance.keysoundVolumePercent);
        sfxVolumeDisplay.text = 
            VolumeToVolumeText(Options.instance.sfxVolumePercent);
    }

    public void ApplyVolume()
    {
        audioMixer.SetFloat("MasterVolume",
            AmpToScaledDecibel(
                Options.instance.masterVolumePercent));
        audioMixer.SetFloat("MusicVolume",
            AmpToScaledDecibel(
                Options.instance.musicVolumePercent));
        audioMixer.SetFloat("KeysoundVolume",
            AmpToScaledDecibel(
                Options.instance.keysoundVolumePercent));
        audioMixer.SetFloat("SfxVolume",
            (float)(
                AmpToScaledDecibel(
                    Options.instance.sfxVolumePercent)
                + (Options.instance.usePerTrackGain ?
                    AudioSourceManager.kBaseSfxGain
                    : 0)));
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
