using UnityEngine;

public class MaterialColorOscillator : MonoBehaviour
{
    public Material material;
    public float speed = 1f;

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        material.SetFloat("T", t); 
    } 

} 