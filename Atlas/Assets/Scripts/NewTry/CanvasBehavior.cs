using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehavior : MonoBehaviour
{
    public static CanvasBehavior Instance;
    private Canvas field;
    public  event Action<Vector2> OnSizeChanged;
    Vector2 HScreensize = Vector2.zero;
    void Awake()
    {
        Instance = this;
       field = GetComponent<Canvas>();
        HScreensize = getSize();
    }
    private void Update()
    {

        if (HScreensize!=getSize()) { 
            HScreensize=getSize();
            OnSizeChanged?.Invoke(HScreensize);
        }

        
    }



    public Vector2 getSize() => field.pixelRect.size;
}
