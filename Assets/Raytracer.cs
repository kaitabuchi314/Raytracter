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
                    screenTexture.SetPixel(x, y, CastSingleRay(x, y));
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

    Color CastSingleRay(int px, int py)
    {
        float u = (float)px / textureWidth;
        float v = (float)py / textureHeight;

        Vector3 screenPos = new Vector3(u * Screen.width, v * Screen.height, 0);

        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        RaycastHit hit;
        Color color = new Color(0, 0, 0, 1);

        if (Physics.Raycast(ray, out hit))
        {
            RaytracingMaterial rmat = hit.collider.gameObject.GetComponent<RaytracingMaterial>();
            if (rmat != null)
            {
                color = rmat.color;
            }
        }

        return color;
    }
}
