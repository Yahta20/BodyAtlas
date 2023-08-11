using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    RectTransform mainRect;
    public RectTransform toppanel;
    public RectTransform scrollRect;
    public RectTransform lanbtn;
    public RectTransform upbtn;
    public RectTransform extbtn;
    public RectTransform text;
    public RectTransform icon;


    [Space]
    public Image BtnImage;
    public Sprite[] button;

    [SerializeField]
    float xkof;


    bool visible =true;
    // Start is called before the first frame update
    void Start()
    {
        mainRect = GetComponent<RectTransform>();
        SetSizes();
    }

    private void SetSizes()
    {
        var sc = CanvasBehavior.Instance.getSize();


        mainRect.sizeDelta = new Vector2(sc.x*xkof,sc.y);
        mainRect.anchoredPosition = Vector2.zero;

        scrollRect.sizeDelta = new Vector2(sc.x * xkof, mainRect.sizeDelta.y * 0.80f);
        toppanel.sizeDelta = new Vector2(sc.x * xkof, mainRect.sizeDelta.y * 0.10f);
        text.sizeDelta = toppanel.sizeDelta;
        lanbtn  .sizeDelta = new Vector2(text.sizeDelta.y, text.sizeDelta.y);
        upbtn   .sizeDelta = new Vector2(text.sizeDelta.y,text.sizeDelta.y);
        extbtn  .sizeDelta = new Vector2(text.sizeDelta.y,text.sizeDelta.y);
        lanbtn  .anchoredPosition   =   Vector2.zero; 
        extbtn  .anchoredPosition = Vector2.zero; 
        upbtn   .anchoredPosition   =new Vector2(extbtn.sizeDelta.y*2,0);

        icon    .sizeDelta = new Vector2(text.sizeDelta.y, text.sizeDelta.y);
        icon    .anchoredPosition = new Vector2(-extbtn.sizeDelta.y * 2, 0);

    }

    // Update is called once per frame
    void Update()
    {
        ChekVisible();
    }

    private void ChekVisible()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeView();
        }
        if (visible)
        {
            mainRect.anchoredPosition = new Vector2(Mathf.Lerp(mainRect.anchoredPosition.x, 0, Time.deltaTime*3), 0);
        }
        else {
            mainRect.anchoredPosition = new Vector2(Mathf.Lerp(mainRect.anchoredPosition.x, mainRect.sizeDelta.x, Time.deltaTime*3), 0);
        }

    }

    public void ChangeLang() { 
    
    
    }

    public void Upstarse() {
        Control.Instance.UpperHierarchy();
    }

    public void Exit() {
    
    }

    public void ChangeView() =>visible = !visible;
    

}
