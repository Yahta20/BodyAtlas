using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class topPanel : MonoBehaviour
{
    public static topPanel Instance;
    
    public RectTransform TopPanel;
    public Image scroll;
    public Image exit;
    public Image lang;
    public Text typolang;
    public Text nlang;

    private bool state;
    private bool stateCR;
    public float SpeedOfScrole;
    private Vector2 screenSize;

    //public IObservable <Vector2> screenSize { get; private set; }
    public IObservable <bool> changeScrean { get; private set; }
    
    void Awake() {
        GameEnviroment.Singelton.setLanguage(0);
        Instance = this;
        TopPanel = GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height);
        //print(screenSize);
        state = false;
        SpeedOfScrole = SpeedOfScrole == 0 ? 0.1f : SpeedOfScrole;
        changeScrean = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                if (screenSize.x!=Screen.width| screenSize.y != Screen.height) {
                    screenSize = new Vector2(Screen.width, Screen.height);
                    //print("Allo");
                    return true;
                }
                return false;
            });


    }

    // Start is called before the first frame update
    void Start()
    {
        updateFoo();

        var movi = Moving.Instance;

        changeScrean.
            //Where(w => w != false).
            Subscribe(s => {
                if (s==true) { 
                    updateFoo();
                }
                //print(s);
            } ).
            AddTo(this);

        movi.mainPanel.
            Where(w => w != false).
            Subscribe(s => { changeState(); }).
            AddTo(this);

        scroll.OnPointerDownAsObservable().
            Subscribe(s=> {
                state = state == false ? true : false;
            }).
            AddTo(this);

       exit.OnPointerDownAsObservable().
            Subscribe(s => {
                Application.Quit();
            }).
            AddTo(this);
        lang.OnPointerDownAsObservable().
             Subscribe(s => {
                 
                 switch (GameEnviroment.Singelton.languageInfo)
                 {
                     case (Lang.lat):
                         GameEnviroment.Singelton.setLanguage(1); break;
                     case (Lang.ua):
                         GameEnviroment.Singelton.setLanguage(2); break;
                     case (Lang.ru):
                         GameEnviroment.Singelton.setLanguage(3); break;
                     case (Lang.en):
                         GameEnviroment.Singelton.setLanguage(0); break;
                 }
                 
             }).
             AddTo(this);

        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                //updateFoo();
                if (state)
                {
                    var y = Mathf.Lerp(TopPanel.anchoredPosition.y, 0, SpeedOfScrole);
                    TopPanel.anchoredPosition = new Vector2(TopPanel.anchoredPosition.x, y);
                    
                }
                else {
                    var y = Mathf.Lerp(TopPanel.anchoredPosition.y, TopPanel.sizeDelta.y, SpeedOfScrole);
                    TopPanel.anchoredPosition = new Vector2(TopPanel.anchoredPosition.x, y);
                }

            })
            .AddTo(this);
    }


    void changeState()
    {
        state = state == false ? true : false;
    }


    void updateFoo( ) {
        //print(screenSize);
        //razmer okna
        TopPanel.sizeDelta          = new Vector2(screenSize.x*0.62f, screenSize.y*0.15f);
        TopPanel.anchoredPosition   = new Vector2(screenSize.x*0.01f, TopPanel.sizeDelta.y);

        //razmer kartinok
        //-vihod
        var rtExit = exit.rectTransform;
        rtExit.sizeDelta          = new Vector2(TopPanel.sizeDelta.y * 0.9f, TopPanel.sizeDelta.y * 0.9f);
        rtExit.anchoredPosition   = new Vector2(TopPanel.sizeDelta.y * 0.05f,0);
        
        //-scroll
        var rtScroll = scroll.rectTransform;
        rtScroll.sizeDelta          = new Vector2(TopPanel.sizeDelta.x * 0.08f, TopPanel.sizeDelta.x * 0.08f);
        rtScroll.anchoredPosition   = new Vector2(0, 0);
        
        //-change
        var rtlang = lang.rectTransform;
        rtlang.sizeDelta          = new Vector2(TopPanel.sizeDelta.y * 0.9f, TopPanel.sizeDelta.y * 0.9f);
        rtlang.anchoredPosition   = new Vector2(-TopPanel.sizeDelta.y * 0.05f, 0);
        
        //-tip yazika
        var rttypolang = typolang.rectTransform;
        rttypolang.sizeDelta = new Vector2(TopPanel.sizeDelta.x * 0.36f, TopPanel.sizeDelta.y * 0.45f);
        rttypolang.anchoredPosition = new Vector2(0, 0);

        //nazvanie
        var rtnlang = nlang.rectTransform;
        rtnlang.sizeDelta = new Vector2(TopPanel.sizeDelta.x * 0.36f, TopPanel.sizeDelta.y * 0.45f);
        rtnlang.anchoredPosition = new Vector2(0, 0);

    }







}
