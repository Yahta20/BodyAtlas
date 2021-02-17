using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UIManager : MonoBehaviour
{
    private Canvas mainCanvas;
    private CanvasScaler cs4work;
    public RectTransform TopPanel;
    public RectTransform RightPanel;
    // Start is called before the first frame update
    void Awake() {
        mainCanvas = GetComponent<Canvas>();
        cs4work = mainCanvas.GetComponent<CanvasScaler>();
    }
    
    void Start()
    {
        var chekObj = Observable.EveryFixedUpdate()
            .Subscribe(
            s => {
               //cs4work.referenceResolution = new Vector2(Screen.width,Screen.height);
            })
            .AddTo(this);
    }

    
}
