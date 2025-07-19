using UnityEngine;

public class HatColorRainbow : MonoBehaviour
{
    public Renderer hatRenderer; //
    public float speed = 1f; 

    private Material hatMaterial;
    private float hue = 0f;

    void Start()
    {
        if (hatRenderer == null)
        {
            Debug.LogError("Hat Renderer не визначено!");
            return;
        }

       
        hatMaterial = hatRenderer.material;
        StartCoroutine(Rainbow());
    }

    System.Collections.IEnumerator Rainbow()
    {
        while (true)
        {
            Color c = Color.HSVToRGB(hue, 1f, 1f);
            hatMaterial.color = c;

            hue += Time.deltaTime * speed;
            if (hue > 1f) hue = 0f;

            yield return null;
        }
    }
}