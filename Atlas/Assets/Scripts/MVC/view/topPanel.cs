using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class topPanel : MonoBehaviour
{
    public static topPanel Instance;

    public Canvas mainCanvas;

    public RectTransform rtPanel;
    [Space]
    public Image scroll;
    public Image exit;
    public Image lang;
    public Image flag;
    public Image back;
    [Space]
    public Text typolang;
    public Text nlang;

    private bool state;
    private bool stateCR;
    public float SpeedOfScrole;
    private Vector2 scrSize;

    public IObservable <Vector2> screenSize { get; private set; }
    public IObservable <bool> changeScrean { get; private set; }
    
    void Awake() {
        
        Instance = this;
        rtPanel = GetComponent<RectTransform>();
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
        nlang.text = "Lingua Latina";
    }
    void Start()
    {
        //updateFoo(mainCanvas.pixelRect.size);
        //o=> { var v = o;
        // updateFoo();}
        //Buffer(2).
        //Where(w=>w[1]!=w[0]).
        //print(s);
        //TimeInterval().
        //DistinctUntilChanged().
        // 出力
        //dClick1.Subscribe(b => Debug.Log(b ? "シングルクリック" : "ダブルクリック");
        // ↑ 3項演算子によるシングル、ダブルクリック出力
                //Application.Quit();





        var movi = Moving.Instance;
        var uiMng = UIManager.Instance;
        var screen = uiMng.screenSize.
            Where(w => w != Vector2.zero).
            Subscribe(s => {
                updateFoo(s);
            } )
            .AddTo(this);

        movi.mainPanel.
            Where(w => w != false).
            Subscribe(s => { changeState(); }).
            AddTo(this);

        exit.OnPointerDownAsObservable().
            Subscribe(s => {
                StartScene();
            }).
            AddTo(this);
        lang.OnPointerDownAsObservable().
             Subscribe(s => {           
                 switch (GameManager.Singelton.currentLang)
                 {
                     case (Lang.lat):
                         GameManager.Singelton.setLanguage(1);
                         //typolang.text = "Українська";
                         nlang.text = "Українська Мова";
                         break;
                     case (Lang.ua):
                         GameManager.Singelton.setLanguage(2);
                         //typolang.text = "Русский";
                         nlang.text = "Русский Язык";
                         break;
                     case (Lang.ru):
                         GameManager.Singelton.setLanguage(3);
                         //typolang.text = "English";
                         nlang.text = "English Language";
                         break;
                     case (Lang.en):
                         GameManager.Singelton.setLanguage(0);
                         //typolang.text = "Latina";
                         nlang.text = "Lingua Latina";
                         break;
                 }            
             }).
             AddTo(this);
        
        back.OnPointerDownAsObservable().
             Subscribe(s => {
                 upHierarchy();                 
             }).
             AddTo(this);


    }


    public void upHierarchy()
    {
        switch (GameManager.Instance.currentChose)
        {
            case stateOfChose.Partition:
                back.color = new Color(255, 255, 255, 255);
                //GameManager.Instance.setState(stateOfChose.Partition);
                //GameManager.Instance.currentSubpartitions = "";
                //rightPanel.Instance.init = false;
                break;
            case stateOfChose.Subpartitions:
                back.color = new Color(255, 255, 255, 255);
                GameManager.Instance.setState(stateOfChose.Partition);
                GameManager.Instance.currentPartition = "";
                GameManager.Instance.GOID = 0;
                rightPanel.Instance.init = false;
                break;
            case stateOfChose.Item:
                back.color = new Color(255, 255, 255, 255);
                GameManager.Instance.setState(stateOfChose.Subpartitions);
                GameManager.Instance.currentSubpartitions = "";
                GameManager.Instance.currentItem = "";
                GameManager.Instance.GOID = 0;
                GameManager.Instance.currentItemPoints = "";
                rightPanel.Instance.init = false;
                break;
            case stateOfChose.ItemPoints:
                back.color = new Color(255, 255, 255, 255);
                GameManager.Instance.setState(stateOfChose.Item);
                GameManager.Instance.currentItem = "";
                GameManager.Instance.GOID = 0;
                GameManager.Instance.currentItemPoints = "";
                ClassroomBeh.Instance.setEmpty();
                rightPanel.Instance.init = false;
                break;
        }
    }
    void changeState()
    {
        state = state == false ? true : false;
    }
    void updateFoo(Vector2 screenSize) {
        //print(screenSize);
        //razmer okna
        var rightPanelSize          = rightPanel.Instance.rtPanel.sizeDelta;
        var rightPanelPos           = rightPanel.Instance.rtPanel.anchoredPosition;

        rtPanel.sizeDelta           = new Vector2(rightPanelSize.x, rightPanelSize.y*0.07f);
        rtPanel.anchoredPosition    = new Vector2(0, 0);

        //razmer kartinok
        //-vihod
        var rtExit = exit.rectTransform;
        rtExit.sizeDelta            = new Vector2(rtPanel.sizeDelta.y * 0.9f, rtPanel.sizeDelta.y * 0.9f);
        rtExit.anchoredPosition     = new Vector2(-rtPanel.sizeDelta.y * 0.05f,0);

        //.sizeDelta            = new Vector2(rtPanel.sizeDelta.y * 0.9f, rtPanel.sizeDelta.y * 0.9f);
        //.anchoredPosition     = new Vector2(rtPanel.sizeDelta.y * 0.05f,0);
        //-scroll
        var rtScroll = scroll.rectTransform;
        rtScroll.sizeDelta          = new Vector2(rtPanel.sizeDelta.x * 0.08f, rtPanel.sizeDelta.x * 0.08f);
        rtScroll.anchoredPosition   = new Vector2(0, 0);
        
        //-change
        var rtlang = lang.rectTransform;
        rtlang.sizeDelta            = new Vector2(rtPanel.sizeDelta.y * 0.9f, rtPanel.sizeDelta.y * 0.9f);
        rtlang.anchoredPosition     = new Vector2(0, -rtPanel.sizeDelta.y * 0.05f);

        //-fang
        var rtflag = flag.rectTransform;
        rtflag.sizeDelta            = new Vector2(rtlang.sizeDelta.y * 0.91f, rtlang.sizeDelta.y * 0.55f);
        rtflag.anchoredPosition     = new Vector2(rtPanel.sizeDelta.y * 0.05f, 0);

        var rtback = back.rectTransform;
        rtback.sizeDelta            = rtlang.sizeDelta;
        rtback.anchoredPosition     = new Vector2(rtlang.sizeDelta.x, 0);

        //nazvanie
        var rtnlang = nlang.rectTransform;
        var leng = rtlang.sizeDelta.x + rtback.sizeDelta.x + rtExit.sizeDelta.x + (-rtExit.anchoredPosition.x);
        leng = rtPanel.sizeDelta.x-leng; 
        leng *= 0.98f;
        var xpas = rtlang.sizeDelta.x + rtback.sizeDelta.x;
        rtnlang.sizeDelta           = new Vector2(leng, rtPanel.sizeDelta.y);
        rtnlang.anchoredPosition    = new Vector2(xpas*1.02f, 0);
    }

    
    public void StartScene()
    {
        GameManager.Instance.resetData();
        SceneManager.LoadScene(0);
    }

}