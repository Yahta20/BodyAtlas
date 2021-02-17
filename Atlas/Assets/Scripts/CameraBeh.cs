using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CameraBeh : MonoBehaviour
{
    private Vector2 screenBounds;
    private Camera camera;

    
    void Start()
    {
        camera = GetComponent<Camera>();
    }

}
