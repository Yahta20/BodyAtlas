using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehavior : MonoBehaviour
{
    public static CanvasBehavior Instance;
    private Canvas field;
    void Awake()
    {
        Instance = this;
       field = GetComponent<Canvas>();
    }

    public Vector2 getSize() => field.pixelRect.size;
}
