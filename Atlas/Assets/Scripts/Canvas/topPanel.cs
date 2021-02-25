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

    public Canvas mainCanvas;

    public RectTransform TopPanel;
    public Image scroll;
    public Image exit;
    public Image lang;
    public Text typolang;
    public Text nlang;

    private bool state;
    private bool stateCR;
    public float SpeedOfScrole;
    private Vector2 scrSize;

    public IObservable <Vector2> screenSize { get; private set; }
    public IObservable <bool> changeScrean { get; private set; }
    
        //mainCanvas = GetComponent<Canvas>();
        //screenSize = new Vector2(Screen.width, Screen.height);
        //print(screenSize);
                //if (screenSize.x!=Screen.width| screenSize.y != Screen.height) {
                //    screenSize = new Vector2(Screen.width, Screen.height);
                //    //print("Allo");
                //    return true;
                //}
    void Awake() {
        
        Instance = this;
        TopPanel = GetComponent<RectTransform>();
        state = false;
        SpeedOfScrole = SpeedOfScrole == 0 ? 0.1f : SpeedOfScrole;
        changeScrean = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                return false;
            });
        screenSize = this.FixedUpdateAsObservable()
            .Select(w =>
            {
                if (mainCanvas == null)
                {
                    mainCanvas = (Canvas)FindObjectOfType(typeof(Canvas));
                } else {
                   
                    return mainCanvas.pixelRect.size;
                }
                
                return Vector2.zero;
            });

    }



    // Start is called before the first frame update
    void Start()
    {
        //updateFoo();

        var movi = Moving.Instance;
        var uiMng = UIManager.Instance;
            //o=> { var v = o;
            // updateFoo();}
            //Buffer(2).
            //Where(w=>w[1]!=w[0]).
                //print(s);

        uiMng.screenSize.
            Where(w => w != Vector2.zero).
            DistinctUntilChanged().
            Subscribe(s => {
                updateFoo(s);
            } )
            .AddTo(this);

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
                         GameEnviroment.Singelton.setLanguage(1);
                         //typolang.text = "Українська";
                         nlang.text = "Українська Мова";
                         break;
                     case (Lang.ua):
                         GameEnviroment.Singelton.setLanguage(2);
                         //typolang.text = "Русский";
                         nlang.text = "Русский Язык";
                         break;
                     case (Lang.ru):
                         GameEnviroment.Singelton.setLanguage(3);
                         //typolang.text = "English";
                         nlang.text = "English Language";
                         break;
                     case (Lang.en):
                         GameEnviroment.Singelton.setLanguage(0);
                         //typolang.text = "Latina";
                         nlang.text = "Lingua Latina";
                         break;
                 }
                 
             }).
             AddTo(this);

        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                //updateFoo();
                //if (state)
                //{
                //    var y = Mathf.Lerp(TopPanel.anchoredPosition.y, 0, SpeedOfScrole);
                //    TopPanel.anchoredPosition = new Vector2(TopPanel.anchoredPosition.x, y);
                //    
                //}
                //else {
                //    var y = Mathf.Lerp(TopPanel.anchoredPosition.y, TopPanel.sizeDelta.y, SpeedOfScrole);
                //    TopPanel.anchoredPosition = new Vector2(TopPanel.anchoredPosition.x, y);
                //}
                //
            })
            .AddTo(this);
    }


    void changeState()
    {
        state = state == false ? true : false;
    }


    void updateFoo(Vector2 screenSize) {
        //print(screenSize);
        //razmer okna
        var rightPanelSize =  rightPanel.Instance.TopPanel.sizeDelta;
        var rightPanelPos = rightPanel.Instance.TopPanel.anchoredPosition;


        TopPanel.sizeDelta          = new Vector2(rightPanelSize.x, rightPanelSize.y*0.07f);
        TopPanel.anchoredPosition   = new Vector2(0, 0);

        //razmer kartinok
        //-vihod
        var rtExit = exit.rectTransform;
        rtExit.sizeDelta            = new Vector2(TopPanel.sizeDelta.y * 0.9f, TopPanel.sizeDelta.y * 0.9f);
        rtExit.anchoredPosition     = new Vector2(-TopPanel.sizeDelta.y * 0.05f,0);

        //.sizeDelta            = new Vector2(TopPanel.sizeDelta.y * 0.9f, TopPanel.sizeDelta.y * 0.9f);
        //.anchoredPosition     = new Vector2(TopPanel.sizeDelta.y * 0.05f,0);
        //-scroll
        var rtScroll = scroll.rectTransform;
        rtScroll.sizeDelta          = new Vector2(TopPanel.sizeDelta.x * 0.08f, TopPanel.sizeDelta.x * 0.08f);
        rtScroll.anchoredPosition   = new Vector2(0, 0);
        
        //-change
        var rtlang = lang.rectTransform;
        rtlang.sizeDelta            = new Vector2(TopPanel.sizeDelta.y * 0.9f, TopPanel.sizeDelta.y * 0.9f);
        rtlang.anchoredPosition     = new Vector2(TopPanel.sizeDelta.y * 0.05f, 0);
        
        //-tip yazika
        //var rttypolang = typolang.rectTransform;
        //rttypolang.sizeDelta        = new Vector2(TopPanel.sizeDelta.x * 0.72f, TopPanel.sizeDelta.y);
        //rttypolang.anchoredPosition = new Vector2(0, 0);

        //nazvanie
        var rtnlang = nlang.rectTransform;
        rtnlang.sizeDelta           = new Vector2(TopPanel.sizeDelta.x * 0.70f, TopPanel.sizeDelta.y);
        rtnlang.anchoredPosition    = new Vector2(0, 0);

    }







}
