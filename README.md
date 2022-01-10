**Disclaimer:** This fork is intended for personal use. These commits are published in case anyone is interested, and I might not be actively maintaining this (at least I will merge changes from the original repository from time to time).

## Changes

* Add default background settings and the "Always Use Default BG Settings" toggle, all in *Options*.
  * If the new toggle is on, background settings in the Modifier Side Sheet and Pause Menu also apply to defaults.
* **New Feature:** Base BGAs
  * In the Modifier Side Sheet, the "No Video" toggle has been changed into "Background Source" dropdown.
    * Possible Options...
      * Pattern BGA (No Video OFF)
      * Pattern Image (No Video ON)
      * Base BGA (One of the BGAs stored in the *BGAs* folder)
    * A few playback modes are provided, explained and can be set in *Options*.
  * Put video files with a supported format into the *BGAs* folder.
  * **IMPORTANT:** This will remove all track's "No Video" values inside per-track settings.
    * All other settings are still compatible with the official build.
* Add ability to use supported files inside track subfolder(s)
  * Those files are displayed, or stored into track.tech as a relative path separated by "/" (e.g. *Notes/synth_001.ogg*).
* Disable empty hits for long notes (Hold, Drag, Repeat Hold)
  * Reference: DJMax Respect V behavior.
* With "Auto Keysound" on, reroute keysound audio sources to the keysound mixer track
  * This keeps keysounds from using the music volume (from settings) instead of keysound volume.
* (dirty change) Some default values which are unavailable in settings
  * Default BGA brightness (from 10 to 6)
* (dirty change) Convert gameplay note collections from lists to dictionaries
  * For optimization, especially on rapid scan jumps in practice mode
* Prevent screen settings applying at launch, so some command line arguments are usable (e.g. custom screen resolution)
  * You can still apply them in settings.
* Optimize Unity physics and shading
  * Since TECHMANIA does not actually utilize 3D-space shading, some settings are tweaked for unlit kind of shading.
  * Other minor game scene adjustments, such as 2D culling mask and clipping distance
* UI text changes
  * [Tex Gyre Adventor](https://www.fontsquirrel.com/fonts/tex-gyre-adventor) as default font (TMP)
  * Readjusted text styling and spacing
  * Original fonts kept as fallbacks
* Bug fixes
  * Fix Scan Jump Playing Keysounds Right at the Start Twice
  * Pattern Editor: Fixed Keysound Off-By-One Errors


Below is the content from the original README.

---

# TECHMANIA
An open source rhythm game, written in Unity, playable with or without a touchscreen.

[Download for Windows](https://github.com/techmania-team/techmania/releases)

[Download for iOS/iPadOS](https://apps.apple.com/us/app/techmania/id1581524513)

[Trailer](https://www.youtube.com/watch?v=MtkxhEmCWwU)

[Discord](https://discord.gg/K4Nf7AnAZt)

[Official website](https://techmania-team.herokuapp.com/)

## Licensing
All code and assets are released under the [MIT License](LICENSE), with the following exceptions:
* Sound effects in [TECHMANIA/Assets/Sfx](TECHMANIA/Assets/Sfx) are acquired from external resources, which use different licenses. Refer to [TECHMANIA/Assets/Sfx/Attributions.md](TECHMANIA/Assets/Sfx/Attributions.md) for details. Please note that some licenses prohibit commercial use.
* Some included tracks in the releases are under separate licenses:
  * f for fun is released under the [CC BY-NC-ND 4.0 License](https://creativecommons.org/licenses/by-nc-nd/4.0/).
  * Yin-Yang Specialist (MUG ver) is released under the [CC BY-NC-NA 4.0 License](https://creativecommons.org/licenses/by-nc-sa/4.0/).
  * v (Game Mix) is released under the [CC BY-NC-SA 4.0 License](https://creativecommons.org/licenses/by-nc-sa/4.0/).

## Roadmap and progress
Refer to the [Kanban](https://github.com/techmania-team/techmania/projects/1).

## Manual and documentation
Refer to the [documentation website](https://techmania-team.github.io/techmania-docs/).

## Platform
The game is designed for Windows PCs, with the Touch control scheme requiring a touchscreen monitor. Patterns using other control schemes are playable with a mouse and keyboard.

We also provide technical support for iOS/iPadOS, in that we are able to respond to bug reports and some feature requests. However please be aware of the following:

- The game's UI is designed for PC and may be difficult to navigate on a phone;
- The game's difficulty is tuned for PC and may prove too difficult for mobile players;
- Some features in the included editor require mouse and keyboard, and therefore are unavailble to mobile users.

[Builds for other platforms](#platform-specific-forks) also exist, but are not officially supported at the moment.

## Content policy
Per the MIT license, you are free to produce any Content with TECHMANIA, including but not limited to screenshots, videos and livestreams. Attributions are appreciated, but not required. However, please keep the following in mind:
* If your Content features 3rd party music, it may be subject to copyright claims and/or takedowns. You may not hold TECHMANIA responsible for the resulting losses.
* If your Content is publicly available and features any unofficial [skin](https://github.com/techmania-team/techmania-docs/blob/main/English/Skins.md), you must clearly state so in the description of your Content, to avoid potential confusion.
* If your Content is commercial, additional limitations apply:
  * Your Content cannot feature the official tracks f for fun, Yin-Yang Specialist (MUG ver) and v (Game Mix).
  * Your Content cannot feature the [Fever sound effect](TECHMANIA/Assets/Sfx/Fever.wav). You can swap the sound with one that allows commercial use, make a custom build, and produce Content from that build.

## Feedback
For technical issues, read the [contribution guidelines](CONTRIBUTING.md), then submit them to [Issues](https://github.com/techmania-team/techmania/issues).

For general discussions, head to [Discord](https://discord.gg/K4Nf7AnAZt).

## Making your own builds
Follow the standard building process:
* Install Unity, making sure your Unity version matches this project's. Check the project's Unity version at [ProjectVersion.txt](TECHMANIA/ProjectSettings/ProjectVersion.txt).
* Clone this repo, then open it from Unity.
* File - Build Settings
* Choose your target platform, then build.

Please note that the default skins are not part of the project, so you'll need to copy the `Skins` folder from an official release into your build folder, in order for your build to be playable. Alternatively, set up [streaming assets](#on-streaming-assets) in your local clone.

If the build fails or produces a platform-specific bug, you can submit an issue, but we do not guarantee support.

## On streaming assets
In PC builds we release the base game and resources (official tracks and skins) separately so PC players don't need to redownload resources when updating. On mobile builds, however, it's more beneficial to include the resources in the release so the installation process is easier. To achieve this, we take advantage of [streaming assets](https://docs.unity3d.com/Manual/StreamingAssets.html).

In order to keep this repo focused, the streaming assets folder (TECHMANIA/Assets/StreamingAssets) is ignored in `.gitignore`. To set up streaming assets in your local clone:
* Create the following folders:
  * `TECHMANIA/Assets/StreamingAssets`
  * `TECHMANIA/Assets/StreamingAssets/Tracks`
  * `TECHMANIA/Assets/StreamingAssets/Tracks/Official Tracks`
  * `TECHMANIA/Assets/StreamingAssets/Skins`
* Download `Skins_and_Tracks.zip` from an official release.
* Copy everything:
  * from `Skins_and_Tracks.zip/Tracks` to `TECHMANIA/Assets/StreamingAssets/Tracks/Official Tracks`
  * from `Skins_and_Tracks.zip/Skins` to `TECHMANIA/Assets/StreamingAssets/Skins`

If done correctly, you should see official tracks and skins in the game even when they are not in the build folder.

Additionally, we perform the following optimizations / decorations for the mobile releases:
* In tracks, convert .wav to .ogg, and .wmv to .mp4, in order to decrease file size and increase compatibility. When doing so, make sure to also update filenames in `track.tech`.
* Copy `TECHMANIA/Assets/Sprites/Logo.png` to `TECHMANIA/Assets/StreamingAssets/Tracks/Official Tracks` so it shows up as eyecatch.

## Platform-specific forks
* rogeraabbccdd's iOS & Android builds: https://github.com/rogeraabbccdd/techmania/releases
* MoonLight's Android builds: https://github.com/yyj01004/techmania/releases
* fhalfkg's macOS builds: https://github.com/fhalfkg/techmania/releases
* samnyan's Android build on 0.2: https://github.com/samnyan/techmania/releases
