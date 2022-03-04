﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif
        Input.simulateMouseWithTouches = false;
        Paths.PrepareFolders();
        OptionsPanel.ApplyOptionsOnStartUp();
        Paths.ApplyCustomDataLocation();
        SpriteSheet.PrepareEmptySpriteSheet();
        Records.RefreshInstance();
        BetterStreamingAssets.Initialize();
        GetComponent<GlobalResourceLoader>().StartLoading();
        DiscordController.Start();
        DiscordController.SetActivity(DiscordActivityType.MainMenu);
    }
}
