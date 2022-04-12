using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shared by notes and markers, this component positions
// the GameObject at the appropriate position in the workspace.
public class SelfPositionerInEditor : MonoBehaviour
{
    private static int pulsesPerScan => Pattern.pulsesPerBeat *
            EditorContext.Pattern.patternMetadata.bps;

    private void OnEnable()
    {
        PatternPanel.RepositionNeeded += Reposition;
    }

    private void OnDisable()
    {
        PatternPanel.RepositionNeeded -= Reposition;
    }

    public void Reposition()
    {
        Marker marker = GetComponent<Marker>();
        ScanlineInEditor scanline = GetComponent<ScanlineInEditor>();
        NoteObject noteObject = GetComponent<NoteObject>();

        double pulse;
        if (marker != null)
        {
            pulse = marker.pulse;
        }
        else if (scanline != null)
        {
            pulse = scanline.doublePulse;
        }
        else
        {
            pulse = noteObject.note.pulse;
        }
        double x = PulseToX(pulse);

        double y;
        if (marker != null)
        {
            // Don't change y.
            y = GetComponent<RectTransform>().anchoredPosition.y;
        }
        else if (scanline != null)
        {
            y = 0f;
        }
        else
        {
            y = LaneToY(noteObject.note.lane);
        }

        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2((float) x, (float) y);
        if (noteObject != null)
        {
            rect.anchoredPosition = new Vector2((float) x, (float) y);
            rect.sizeDelta = new Vector2(
                PatternPanel.LaneHeight, PatternPanel.LaneHeight);
        }
    }

    public static Vector2 PositionOf(Note n)
    {
        return new Vector2((float) PulseToX(n.pulse), LaneToY(n.lane));
    }

    private static double PulseToX(double pulse)
    {
        double scan = pulse / pulsesPerScan;
        return PatternPanel.ScanWidth * scan;
    }

    private static float LaneToY(int lane)
    {
        return -PatternPanel.LaneHeight * (lane + 0.5f);
    }
}
