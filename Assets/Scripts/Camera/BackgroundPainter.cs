using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class BackgroundPainter : MonoBehaviour
{
    [SerializeField] private Color[] backgroundColors;

    private new Camera camera;
    
    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Start()
    {
        if (backgroundColors.Length > 0)
        {
            camera.backgroundColor = backgroundColors[Random.Range(0, backgroundColors.Length)];
        }
    }
}
