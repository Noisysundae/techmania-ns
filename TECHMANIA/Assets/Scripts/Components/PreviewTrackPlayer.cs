using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class PreviewTrackPlayer : MonoBehaviour
{
    public MessageDialog messageDialog;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(string trackFolder,
        TrackMetadata trackMetadata,
        bool loop)
    {
        if (trackMetadata.previewTrack == "" ||
            trackMetadata.previewTrack == null)
        {
            return;
        }
        if (trackMetadata.previewStartTime >
            trackMetadata.previewEndTime)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(InnerPlay(trackFolder,
            trackMetadata.previewTrack,
            trackMetadata.previewStartTime,
            trackMetadata.previewEndTime,
            trackMetadata.gain,
            loop));
    }

    private IEnumerator InnerPlay(string trackFolder,
        string previewTrackFilename,
        double startTime, double endTime,
        double gain,
        bool loop)
    {
        // We could use ResourceLoader.LoadAudio, but this creates
        // problems when the user stops preview track playback
        // before the loading completes.
        string filename = Path.Combine(trackFolder, 
            previewTrackFilename);
        UnityWebRequest request = 
            UnityWebRequestMultimedia.GetAudioClip(
            Paths.FullPathToUri(filename), AudioType.UNKNOWN);
        yield return request.SendWebRequest();

        AudioClip clip;
        string error;
        ResourceLoader.GetAudioClipFromWebRequest(
            request, out clip, out error);
        if (clip == null)
        {
            // When called from SelectPatternDialog, messageDialog
            // is intentionally set to null because we don't support
            // showing 2 dialogs at the same time.
            messageDialog?.Show(error);
            yield break;
        }

        if (startTime < 0d) startTime = 0d;
        if (endTime > AudioSourceManager.getDoubleLength(clip))
        {
            endTime = AudioSourceManager.getDoubleLength(clip);
        }
        if (endTime == 0d) endTime = AudioSourceManager.getDoubleLength(clip);
        double previewLength = endTime - startTime;

        source.clip = clip;
        source.loop = false;
        source.volume = 0f;
        double fadeLength = 1d;
        if (fadeLength > previewLength * 0.5d)
        {
            fadeLength = previewLength * 0.5d;
        }

        float maxAmp = Options.instance.usePerTrackGain ?
            (float) AudioSourceManager.DecibelToAmp(gain)
            : 1f;

        int numLoops = loop ? int.MaxValue : 1;
        for (int i = 0; i < numLoops; i++)
        {
            float floatFadeLength = (float) fadeLength;
            source.time = (float)startTime;
            source.Play();
            
            for (float time = 0f; time < floatFadeLength; time += Time.deltaTime)
            {
                float progress = time / floatFadeLength * maxAmp;
                source.volume = progress;
                yield return null;
            }
            source.volume = maxAmp;
            yield return new WaitForSeconds(
                (float) (previewLength - fadeLength * 2));
            for (float time = 0f; time < floatFadeLength; time += Time.deltaTime)
            {
                float progress = time / floatFadeLength;
                source.volume = (1f - progress) * maxAmp;
                yield return null;
            }
            source.volume = 0f;
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
        StartCoroutine(InnerStop());
    }

    private IEnumerator InnerStop()
    {
        if (source.volume == 0f) yield break;

        for (; source.volume > 0f; source.volume -= Time.deltaTime * 5f)
        {
            yield return null;
        }
        source.volume = 0f;
        source.Stop();
    }
}
