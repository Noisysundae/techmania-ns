using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = System.Random;

// This would be called VFXRenderer, but apparently
// "Script 'VFXRenderer' has the same name as built-in Unity component."
//
// What the heck.
public class VFXDrawer : MonoBehaviour
{
    public Material additiveMaterial;

    private SpriteSheet spriteSheet;
    private bool loop;

    private RectTransform rect;
    private Image image;
    private float startTime;

    // For random orientation seed reference
    private static int baseRandomSeed;

    private void ModifyTransform (Transform transform,
        SpriteSheet spriteSheet,
        Vector3 position)
    {
        if (spriteSheet.randomOrientationSeed >= 0)
        {
            Random rand = new Random(
                baseRandomSeed
                + spriteSheet.randomOrientationSeed
                + (int) position.x * 1000
                + (int) position.y * 1000);
            transform.Rotate(0, 0, (float) rand.Next());

            float flipX = rand.Next() % 2 == 0 ? -1f: 1f;
            float flipY = rand.Next() % 2 == 0 ? -1f: 1f;
            transform.localScale.Scale(new Vector3(flipX, flipY, 1f));
        }
        transform.position = position;
    }

    public void Initialize(Vector3 position,
        SpriteSheet spriteSheet, bool loop)
    {
        ModifyTransform(transform, spriteSheet, position);
        this.spriteSheet = spriteSheet;
        this.loop = loop;
        if (spriteSheet.additiveShader)
        {
            GetComponent<Image>().material = additiveMaterial;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        startTime = Game.Time;
        baseRandomSeed = (int) System.DateTime.Now.Ticks;

        if (spriteSheet.sprites == null ||
            spriteSheet.sprites.Count == 0)
        {
            Destroy(gameObject);
            return;
        }

        image.sprite = spriteSheet.sprites[0];
        float height = Scan.laneHeight * spriteSheet.scale;
        float width = spriteSheet.sprites[0].rect.width /
            spriteSheet.sprites[0].rect.height * height;
        rect.sizeDelta = new Vector2(width, height);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Game.Time - startTime;
        Sprite sprite = spriteSheet.GetSpriteForTime(time, loop);
        if (sprite == null)
        {
            Destroy(gameObject);
        }
        else
        {
            image.sprite = sprite;
        }
    }
}
