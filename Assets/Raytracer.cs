using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Raytracer : MonoBehaviour {
	public int textureWidth = 256;
    public int textureHeight = 256;
    public bool raytracingEnabled;

    private Texture2D screenTexture;
    private RawImage screenRenderer;

    void Start()
    {
        screenTexture = new Texture2D(textureWidth, textureHeight);
        screenRenderer = GetComponent<RawImage>();

        screenRenderer.texture = screenTexture;

        for (int y = 0; y < screenTexture.height; y++)
        {
            for (int x = 0; x < screenTexture.width; x++)
            {
                screenTexture.SetPixel(x, y, Color.white);
            }
        }

        screenTexture.Apply();
    }

    void Update()
    {
        if (raytracingEnabled)
        {
            for (int y = 0; y < screenTexture.height; y++)
            {
                for (int x = 0; x < screenTexture.width; x++)
                {
                    float darkness = (float)x / (screenTexture.width - 1);
                    Color color = new Color(1 - darkness, 1 - darkness, 1 - darkness, 1);
                    screenTexture.SetPixel(x, y, color);
                }
            }

            screenTexture.Apply();

            screenRenderer.enabled = true;
        }
        else
        {
            screenRenderer.enabled = false;
        }
    }
}
