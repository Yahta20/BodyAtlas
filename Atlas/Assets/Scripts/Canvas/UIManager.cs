using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Canvas mainCanvas;
    public RectTransform TopPanel;
    public RectTransform RightPanel;
    // Start is called before the first frame update
    void Awake() {
        mainCanvas = GetComponent<Canvas>();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
