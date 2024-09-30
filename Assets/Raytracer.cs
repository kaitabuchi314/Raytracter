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
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            Color color = new Color(0,0,0,1);

            if (Physics.Raycast(ray, out hit))
            {
                RaytracingMaterial rmat =  hit.collider.gameObject.GetComponent<RaytracingMaterial>();
                if (rmat != null)
                {
                    color = rmat.color;
                }
            }
            
            for (int y = 0; y < screenTexture.height; y++)
            {
                for (int x = 0; x < screenTexture.width; x++)
                {
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
