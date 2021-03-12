using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class StartMenuBeh : MonoBehaviour
{
    public static StartMenuBeh Instance;

    public RectTransform rtPanel;
    public RectTransform LangStep;
    public RectTransform RoomStep;

    public Text MainText;
    public Text Description;

    public Image UaImg;
    public Image EngImg;
    public Image RuImg;

    public enum StartMenuState {
        hide=0,
        langChose=1,
        roomChose=2,
    }

    public StartMenuState curentstate;

    void Awake()
    {
        Instance = this;
        rtPanel = GetComponent<RectTransform>();
        curentstate = StartMenuState.langChose;
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
        // nagatie OnPointerDownAsObservable().
        randImg.OnPointerDownAsObservable().
        Subscribe(s => {
            currentState = StateOfMenu.exit;
        }).
        AddTo(this);


        boneImg.OnPointerDownAsObservable().
            Subscribe(s => {
                currentState = StateOfMenu.exit;
            }).
            AddTo(this);

        pontImg.OnPointerDownAsObservable().
            Subscribe(s => {
                currentState = StateOfMenu.exit;
            }).
            AddTo(this);
         */



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
        /*
        var rtMainText = MainText.rectTransform;
        var rtDescription = Description.rectTransform;
        var rtrandImg = randImg.rectTransform;
        var rtboneImg = boneImg.rectTransform;
        var rtpontImg = pontImg.rectTransform;

        rtPanel.sizeDelta = new Vector2(size.x * 0.62f, size.y * 0.62f);

        FirstStep.sizeDelta = new Vector2(rtPanel.sizeDelta.x, rtPanel.sizeDelta.y * 0.75f);
        FirstStep.anchoredPosition = Vector2.zero;

        //PointStep.sizeDelta         = new Vector2(rtPanel.sizeDelta.x, rtPanel.sizeDelta.y * 0.75f);
        //PointStep.anchoredPosition  = Vector2.zero;

        rtMainText.sizeDelta = new Vector2(rtPanel.sizeDelta.x, rtPanel.sizeDelta.y * 0.15f);
        rtDescription.sizeDelta = new Vector2(rtPanel.sizeDelta.x, rtPanel.sizeDelta.y * 0.15f);

        var BlockSize = rtPanel.sizeDelta.x < rtPanel.sizeDelta.y ? rtPanel.sizeDelta.x : rtPanel.sizeDelta.y;
        rtrandImg.sizeDelta = new Vector2(BlockSize * 0.25f, BlockSize * 0.25f);
        rtboneImg.sizeDelta = new Vector2(BlockSize * 0.25f, BlockSize * 0.25f);
        rtpontImg.sizeDelta = new Vector2(BlockSize * 0.25f, BlockSize * 0.25f);

        rtrandImg.anchoredPosition = new Vector2(-FirstStep.sizeDelta.x * 0.32f, FirstStep.sizeDelta.y * 0.16f);
        rtboneImg.anchoredPosition = new Vector2(0, FirstStep.sizeDelta.y * 0.16f);
        rtpontImg.anchoredPosition = new Vector2(FirstStep.sizeDelta.x * 0.32f, FirstStep.sizeDelta.y * 0.16f);
        switch (currentState)
        {
            case StateOfMenu.start:
                startState();

                break;
            case StateOfMenu.choseBone:
                choseBoneState();

                break;
            case StateOfMenu.exit:
                exitState();

                break;
            default:
                break;
        }
         */





    }

    private void startState()
    {
        this.gameObject.SetActive(true);
        //FirstStep.gameObject.SetActive(true);
        //PointStep.gameObject.SetActive(false);
    }

    private void choseBoneState()
    {
        this.gameObject.SetActive(true);
        //FirstStep.gameObject.SetActive(false);
        //PointStep.gameObject.SetActive(true);
    }

    private void exitState()
    {
        this.gameObject.SetActive(false);

    }

}
