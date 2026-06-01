using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ParallaxMousePosition : MonoBehaviour
{
    private Material material;
    private Camera cam;

    void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        material = sprite.material;
        cam = Camera.main;
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        float normalizedX = (mousePosition.x / Screen.width) - 0.5f;
        float normalizedY = (mousePosition.y / Screen.height) - 0.5f;

        Vector2 mouseOffset = new Vector2(normalizedX, normalizedY);

        material.SetVector("_MouseOffset", mouseOffset);
    }
}
