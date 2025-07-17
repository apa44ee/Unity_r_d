using UnityEngine;

public class MaterialColorChanger : MonoBehaviour
{
    public Renderer targetRenderer;
    public Color color1 = Color.red;
    public Color color2 = Color.blue;
    public float speed = 1f;

    private Material materialInstance;

    void Start()
    {
        materialInstance = targetRenderer.material;
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        materialInstance.color = Color.Lerp(color1, color2, t);
    }
}