using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BuildBeh : MonoBehaviour
{
    public BuildingCanvasBeh canvasDriver;
    void Start()
    {

        this.OnMouseDownAsObservable()
            //.SelectMany(_ => this.UpdateAsObservable())
            //.TakeUntil(this.OnMouseUpAsObservable())
            //.Select(_ => Input.mousePosition)
            .Subscribe(x => {
                if (canvasDriver.getState())
                {
                    canvasDriver.hideCanvas();
                }
                else { 
                
                    canvasDriver.showCanvas();
                }
            });
    }




}
