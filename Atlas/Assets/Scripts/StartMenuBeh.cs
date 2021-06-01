using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuBeh : MonoBehaviour
{
    public static StartMenuBeh Instance;

    public RectTransform rtPanel;
    public RectTransform LangStep;
    public RectTransform RoomStep;
    public RectTransform PartStep;

    [Space]
    public TMP_Text LangText;
    public TMP_Text ModeText;
    public TMP_Text AtlasText;
    public TMP_Text ExamText;
    public TMP_Text PartText;

    //public TextMeshPro MainText;
    //public Text Description;

    [Space]
    public Image UaImg;
    public Image EngImg;
    public Image RuImg;
    public Image LatImg;

    [Space]
    public RectTransform Atlas;
    public RectTransform Exam  ;


    public enum StartMenuState {
        hide=0,
        langChose=1,
        roomChose=2,
        partChose=3,
    }


    public StartMenuState currentState;


    void Awake()
    {
        Instance = this;
        rtPanel = GetComponent<RectTransform>();
        currentState = StartMenuState.roomChose;

        LangText.text = TextUI.Singelton.getLabel("Choose language");
        ModeText.text = TextUI.Singelton.getLabel("Choose mode");
        AtlasText.text = TextUI.Singelton.getLabel("Atlas");
        ExamText .text = TextUI.Singelton.getLabel("Exam");

    }
       

    private void Start()
    {
        var screan = UIManager.Instance.screenSize.
            Where(w => w != Vector2.zero).
            //DistinctUntilChanged().
            Subscribe(s => {
                updateFoo(s);
            }).
            AddTo(this);

        //navedenie

        /*
        randImg.OnPointerEnterAsObservable().
            Subscribe(s => {
                Description.text = "Тест из случайных вопросов\nкостей и точек на них";
            }).
            AddTo(this);


        boneImg.OnPointerEnterAsObservable().
            Subscribe(s => {
                Description.text = "Тест из вопросов о костях";
            }).
            AddTo(this);

        pontImg.OnPointerEnterAsObservable().
            Subscribe(s => {
                Description.text = "Тест из вопросов о точках на костях";
            }).
            AddTo(this);

         */

        // nagatie OnPointerDownAsObservable().
        UaImg.OnPointerDownAsObservable().
            Subscribe(s => {
                //GameEnviroment.Singelton.setUILanguage(LangUI.ua);
                GameEnviroment.Singelton.setLanguage(1);
                currentState = StartMenuState.hide; 
                ExamScene();
            }).
            AddTo(this);


        EngImg.OnPointerDownAsObservable().
            Subscribe(s => {
                //GameEnviroment.Singelton.setUILanguage(LangUI.eng);
                GameEnviroment.Singelton.setLanguage(3);
                currentState = StartMenuState.hide;
                ExamScene();
            }).
            AddTo(this);

        RuImg.OnPointerDownAsObservable().
            Subscribe(s => {
                //GameEnviroment.Singelton.setUILanguage(LangUI.ru);
                GameEnviroment.Singelton.setLanguage(2);
                currentState = StartMenuState.hide;
                ExamScene();
            }).
            AddTo(this);

        LatImg.OnPointerDownAsObservable().
            Subscribe(s => {
                //GameEnviroment.Singelton.setUILanguage(LangUI.ru);
                GameEnviroment.Singelton.setLanguage(0);
                currentState = StartMenuState.hide;
                ExamScene();
            }).
            AddTo(this);
    }





    public void ClasroomScene() { 
        SceneManager.LoadScene(1);
    }
    
    public void ExamScene() {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void updateFoo(Vector2 size)
    {
        //var rtMainText = mtPanel.rectTransform;
        //var rtDescription = Description.rectTransform;
        var rtUAImg  = UaImg.rectTransform;
        var rtENGImg = EngImg.rectTransform;
        var rtRUImg  = RuImg.rectTransform;
        var rtLATImg = LatImg.rectTransform;

        rtPanel.sizeDelta = size;
        rtPanel.anchoredPosition = Vector2.zero;

        var side4Blok = size.x > size.y ? size.y : size.x;

        LangStep.sizeDelta = new Vector2(side4Blok*0.68f, side4Blok * 0.68f) ;
        RoomStep.sizeDelta = new Vector2(side4Blok * 0.68f, side4Blok * 0.68f);
        
        var mtPanel = LangText.rectTransform;
        mtPanel.sizeDelta = new Vector2(LangStep.sizeDelta.x, LangStep.sizeDelta.x * 0.16f); 
        mtPanel.anchoredPosition = Vector2.zero;

        var roomPanel = ModeText.rectTransform;
        roomPanel.sizeDelta = new Vector2(LangStep.sizeDelta.x, LangStep.sizeDelta.x * 0.16f);
        roomPanel.anchoredPosition = Vector2.zero;
        //LANG PANEL
        var yFlagSpace = (LangStep.sizeDelta.y - mtPanel.sizeDelta.y) / 3;
        var yFlag = yFlagSpace * 0.81f;
        
        rtRUImg.sizeDelta  = new Vector2(yFlag*1.5F, yFlag);
        rtRUImg.anchoredPosition = new Vector2(-yFlag * 1F,  yFlag * 0.5f);
        
        rtENGImg.sizeDelta = new Vector2(yFlag * 1.5F, yFlag);
        rtENGImg.anchoredPosition = new Vector2(yFlag * 1F,  yFlag * 0.5f);

        rtUAImg.sizeDelta  = new Vector2(yFlag * 1.5F, yFlag);
        rtUAImg.anchoredPosition = new Vector2(-yFlag * 1F,  yFlag*  1.9f);

        rtLATImg.sizeDelta = new Vector2(yFlag * 1.5F, yFlag);
        rtLATImg.anchoredPosition = new Vector2(yFlag * 1F,  yFlag*  1.9f);
        
        Atlas.sizeDelta = new Vector2(yFlag , yFlag*0.5f);
        Atlas.anchoredPosition = new Vector2(-roomPanel.sizeDelta.x * 0.25f, 0);

        Exam.sizeDelta = new Vector2(yFlag , yFlag * 0.5f);
        Exam.anchoredPosition = new Vector2(roomPanel.sizeDelta.x * 0.25f, 0);

        switch (currentState)
        {
            case StartMenuState.hide:
                exitState();
                break;
            case StartMenuState.langChose:
                startState();
                break;
            case StartMenuState.roomChose:
                roomChose();
                break;
            case StartMenuState.partChose:
                partChose();
                break;
            default:
                break;
        }
    }

    private void partChose()
    {
        this.gameObject.SetActive(true);
        LangStep.gameObject.SetActive(false);
        RoomStep.gameObject.SetActive(false);
        PartStep.gameObject.SetActive(true);
    }

    public void langChose() {
        currentState = StartMenuState.langChose;
    }

    public void PartChose()
    {
        currentState = StartMenuState.partChose;
    }

    private void startState()
    {
        this.gameObject.SetActive(true);
        LangStep.gameObject.SetActive(true);
        RoomStep.gameObject.SetActive(false);
        PartStep.gameObject.SetActive(false);
    }

    private void roomChose()
    {
        this.gameObject.SetActive(true);
        LangStep.gameObject.SetActive(false);
        RoomStep.gameObject.SetActive(true);
        PartStep.gameObject.SetActive(false);
    }

    private void exitState()
    {
        this.gameObject.SetActive(false);
    }


}
