using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Canvas mainCanvas;
    private CanvasScaler cs4work;

    public IObservable <Vector2> screenSize { get; private set; }


    // Start is called before the first frame update
    void Awake() {

        Instance = this;
        mainCanvas = GetComponent<Canvas>();
        cs4work = mainCanvas.GetComponent<CanvasScaler>();

        //TopPanel = topPanel.Instance.rtPanel;
        //RightPanel = rightPanel.Instance.rtPanel; 

        screenSize = this.LateUpdateAsObservable()
        .Select(w =>
        {
            if (mainCanvas == null)
            {
                mainCanvas = (Canvas)FindObjectOfType(typeof(Canvas));
            }
            else
            {
                return CanvasSize();
            }
            return Vector2.zero;
        });


    }


    public Vector2 CanvasSize() { 
        return mainCanvas.pixelRect.size;
    }
    void Start()
    {
        //screenSize.
        //    //o=> { var v = o;
        //    // updateFoo();}
        //    Where(w => w != Vector2.zero).
        //    DistinctUntilChanged().
        //    //Buffer(2).
        //    //Where(w=>w[1]!=w[0]).
        //    Subscribe(s => {
        //        //print(s);
        //        updateFoo(s);
        //    } )
        //    .AddTo(this);

        var chekObj = Observable.EveryFixedUpdate()
            .Subscribe(
            s => {
               //cs4work.referenceResolution = new Vector2(Screen.width,Screen.height);
            })
            .AddTo(this);
    }

    
}
