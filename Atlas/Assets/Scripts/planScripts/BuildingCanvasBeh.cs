using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BuildingCanvasBeh : MonoBehaviour
{

    public  Text            InfoText        ;
    public  Image           InfoImage       ;
    public  Vector3         Position        ;
    public  Vector3         Rotation        ;
    public  Vector3         StartPosition   ;

    private bool            stateOfCanvas   ;
    private Canvas          currentCanvas   ;
    private SphereBeh       playerObj { get; set; }
    private RectTransform   ceryCanvas      ;

    void Awake()
    {
        currentCanvas   =   GetComponent<Canvas>()          ;
        ceryCanvas      =   GetComponent<RectTransform>()   ;
        hideCanvas()                                        ;
    }
    void Start()
    {
        playerObj       =   SphereBeh.Instance              ;
        var CompositControl = Observable.EveryLateUpdate()
            .Subscribe(s=> {
                ScaleImage();
            })
            .AddTo(this);
    }
    private void ScaleImage() {
        var sizeOfCanvas    =   ceryCanvas.sizeDelta        ;
        var xSizeForImage   =   (sizeOfCanvas.x*0.62f)      ;
        var ySizeForImage   =   (sizeOfCanvas.y*0.62f)      ;
        
        //print(playerObj);
        
        transform.rotation  =   Quaternion.LookRotation(transform.position - playerObj.camera.transform.position, Vector3.up);

        //InfoImage.rectTransform.sizeDelta   = new Vector2(xSizeForImage, ySizeForImage)                 ;
        //InfoText.rectTransform.sizeDelta    = new Vector2(xSizeForImage, sizeOfCanvas.y-ySizeForImage)  ;
    }
    public void showCanvas() {
        currentCanvas.enabled = true;
    }
    public void hideCanvas()
    {
        currentCanvas.enabled = false;
    }
    public bool getState() {
        return currentCanvas.enabled;
    }
}
