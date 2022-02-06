using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is specific to hold notes, and does not handle trails.
public class HoldNoteAppearance : NoteAppearance
{
    protected override void TypeSpecificUpdateState()
    {
        switch (state)
        {
            case State.Inactive:
            case State.Resolved:
                SetNoteReceiveRaycast(false);
                SetNoteImageVisibility(Visibility.Hidden);
                SetFeverOverlayVisibility(Visibility.Hidden);
                break;
            case State.Prepare:
            case State.Active:
            case State.Ongoing:
                SetNoteImageVisibility(Visibility.Visible);
                SetFeverOverlayVisibility(Visibility.Visible);
                break;
        }
    }

    protected override void GetNoteImageScale(
        out float x, out float y)
    {
        x = GlobalResource.noteSkin.holdHead.scale;
        y = GlobalResource.noteSkin.holdHead.scale;
    }

    protected override void UpdateSprites()
    {
        if (GlobalResource.noteSkin.holdHead.directionTracking ==
            SpriteSheet.DirectionTracking.Mirror)
        {
            UIUtils.ConditionalMirror(
                noteImage.GetComponent<RectTransform>(),
                scanRef.direction == Scan.Direction.Left);
        }
        noteImage.sprite = GlobalResource.noteSkin.holdHead
            .GetSpriteAtFloatIndex(Game.FloatBeat);
    }
}
