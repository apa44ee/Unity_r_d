using UnityEngine;
using UnityEngine.UI;

public class ImageColorChanger : MonoBehaviour
{
    private Image image;
    public float speed = 1f;

    private Color colorStart = Color.blue;
    private Color colorEnd = Color.red;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f; // коливання от 0 до 1
        image.color = Color.Lerp(colorStart, colorEnd, t);
    }
}