using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModifierSidesheet : MonoBehaviour
{
    [Header("Modifiers")]
    public TMP_Dropdown noteOpacity;
    public TMP_Dropdown scanlineOpacity;
    public TMP_Dropdown scanDirection;
    public TMP_Dropdown notePosition;
    public TMP_Dropdown scanPosition;
    public TMP_Dropdown fever;
    public TMP_Dropdown keysound;
    public TMP_Dropdown assistTick;

    [Header("Appearance")]
    public Toggle showJudgementTally;
    public GameObject forceDefaultBackgroundSettingsIndicator;
    public TMP_Dropdown backgroundDisplay;

    [Header("Special modifiers")]
    public TMP_Dropdown mode;
    public TMP_Dropdown controlOverride;
    public TMP_Dropdown scrollSpeed;

    public static event UnityAction ModifierChanged;

    private void OnEnable()
    {
        MemoryToUI();
    }

    private void InitializeDropdowns()
    {
        UIUtils.InitializeDropdownWithLocalizedOptions(
            noteOpacity,
            Modifiers.noteOpacityDisplayKeys);
        UIUtils.InitializeDropdownWithLocalizedOptions(
            scanlineOpacity,
            Modifiers.scanlineOpacityDisplayKeys);
        UIUtils.InitializeDropdownWithLocalizedOptions(
            scanDirection,
            Modifiers.scanDirectionDisplayKeys);
        UIUtils.InitializeDropdownWithLocalizedOptions(
            notePosition,
            Modifiers.notePositionDisplayKeys);
        UIUtils.InitializeDropdownWithLocalizedOptions(
            scanPosition,
            Modifiers.scanPositionDisplayKeys);
        UIUtils.InitializeDropdownWithLocalizedOptions(
            fever,
            Modifiers.feverDisplayKeys);
        UIUtils.InitializeDropdownWithLocalizedOptions(
            keysound,
            Modifiers.keysoundDisplayKeys);
        UIUtils.InitializeDropdownWithLocalizedOptions(
            assistTick,
            Modifiers.assistTickDisplayKeys);
        UIUtils.InitializeDropdownWithLocalizedOptions(
            mode,
            Modifiers.modeDisplayKeys);
        UIUtils.InitializeDropdownWithLocalizedOptions(
            controlOverride,
            Modifiers.controlOverrideDisplayKeys);
        UIUtils.InitializeDropdownWithLocalizedOptions(
            scrollSpeed,
            Modifiers.scrollSpeedDisplayKeys);

        // Appearance

        UIUtils.InitializeDropdownWithLocalizedOptions(
            backgroundDisplay,
            PerTrackOptions.backgroundDisplayKeys);
    }

    public void MemoryToUI()
    {
        InitializeDropdowns();

        // Modifiers
        
        noteOpacity.SetValueWithoutNotify(
            (int)Modifiers.instance.noteOpacity);
        noteOpacity.RefreshShownValue();
        
        scanlineOpacity.SetValueWithoutNotify(
            (int)Modifiers.instance.scanlineOpacity);
        scanlineOpacity.RefreshShownValue();
        
        scanDirection.SetValueWithoutNotify(
            (int)Modifiers.instance.scanDirection);
        scanDirection.RefreshShownValue();
        
        notePosition.SetValueWithoutNotify(
            (int)Modifiers.instance.notePosition);
        notePosition.RefreshShownValue();
        
        scanPosition.SetValueWithoutNotify(
             (int)Modifiers.instance.scanPosition);
        scanPosition.RefreshShownValue();
        
        fever.SetValueWithoutNotify(
            (int)Modifiers.instance.fever);
        fever.RefreshShownValue();
        
        keysound.SetValueWithoutNotify(
            (int)Modifiers.instance.keysound);
        keysound.RefreshShownValue();

        assistTick.SetValueWithoutNotify(
            (int)Modifiers.instance.assistTick);
        assistTick.RefreshShownValue();

        // Appearance

        showJudgementTally.SetIsOnWithoutNotify(
            Options.instance.showJudgementTally);
        if (Options.instance.forceDefaultBackgroundSettings)
        {
            forceDefaultBackgroundSettingsIndicator.SetActive(true);
            backgroundDisplay.SetValueWithoutNotify(
                (int)Options.instance.defaultBackgroundDisplay);
        }
        else {
            backgroundDisplay.SetValueWithoutNotify(
                (int)GameSetup.trackOptions.backgroundDisplay);
        }
        backgroundDisplay.RefreshShownValue();

        // Special modifiers

        mode.SetValueWithoutNotify(
            (int)Modifiers.instance.mode);
        mode.RefreshShownValue();
        
        controlOverride.SetValueWithoutNotify(
            (int)Modifiers.instance.controlOverride);
        controlOverride.RefreshShownValue();
        
        scrollSpeed.SetValueWithoutNotify(
            (int)Modifiers.instance.scrollSpeed);
        scrollSpeed.RefreshShownValue();
    }

    public void UIToMemory()
    {
        // Modifiers

        Modifiers.instance.noteOpacity =
            (Modifiers.NoteOpacity)noteOpacity.value;
        Modifiers.instance.scanlineOpacity = 
            (Modifiers.ScanlineOpacity)scanlineOpacity.value;
        Modifiers.instance.scanDirection =
            (Modifiers.ScanDirection)scanDirection.value;
        Modifiers.instance.notePosition =
            (Modifiers.NotePosition)notePosition.value;
        Modifiers.instance.scanPosition =
            (Modifiers.ScanPosition)scanPosition.value;
        Modifiers.instance.fever =
            (Modifiers.Fever)fever.value;
        Modifiers.instance.keysound =
            (Modifiers.Keysound)keysound.value;
        Modifiers.instance.assistTick =
            (Modifiers.AssistTick)assistTick.value;

        // Appearance

        Options.instance.showJudgementTally =
            showJudgementTally.isOn;
        if (Options.instance.forceDefaultBackgroundSettings)
        {
            Options.instance.defaultBackgroundDisplay =
                (PerTrackOptions.BackgroundDisplay)backgroundDisplay.value;
        }
        else
        {
            GameSetup.trackOptions.backgroundDisplay =
                (PerTrackOptions.BackgroundDisplay)backgroundDisplay.value;
        }

        // Special modifiers

        Modifiers.instance.mode =
            (Modifiers.Mode)mode.value;
        Modifiers.instance.controlOverride =
            (Modifiers.ControlOverride)controlOverride.value;
        Modifiers.instance.scrollSpeed =
            (Modifiers.ScrollSpeed)scrollSpeed.value;

        ModifierChanged?.Invoke();
    }

    public static string GetDisplayString(
        PerTrackOptions.BackgroundDisplay bgDisplay,
        Color specialModifierColor)
    {
        List<string> regularSegments = new List<string>();
        List<string> specialSegments = new List<string>();
        Modifiers.instance.ToDisplaySegments(
            regularSegments, specialSegments);
        switch (bgDisplay)
        {
            case PerTrackOptions.BackgroundDisplay.PatternBga:
                regularSegments.Add(Locale.GetString(
                    "modifier_bg_pattern_bga"));
                break;
            case PerTrackOptions.BackgroundDisplay.BaseBga: 
                regularSegments.Add(Locale.GetString(
                    "modifier_bg_base_bga"));
                break;
            case PerTrackOptions.BackgroundDisplay.PatternImage: 
                regularSegments.Add(Locale.GetString(
                    "modifier_bg_pattern_image"));
                break;
        }

        List<string> allSegments = new List<string>();
        if (regularSegments.Count + specialSegments.Count == 0)
        {
            allSegments.Add(Locale.GetString(
                "select_pattern_modifier_none"));
        }
        for (int i = 0; i < regularSegments.Count; i++)
        {
            allSegments.Add(regularSegments[i]);
        }
        for (int i = 0; i < specialSegments.Count; i++)
        {
            allSegments.Add(
                $"<color=#{ColorUtility.ToHtmlStringRGB(specialModifierColor)}>{specialSegments[i]}</color>");
        }

        return string.Join(" / ", allSegments);
    }
}
