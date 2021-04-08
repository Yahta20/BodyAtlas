using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class TestMenuBeh : MonoBehaviour
{
    public static TestMenuBeh Instance;

    public RectTransform rtPanel;
    public RectTransform FirstStep;
    //public RectTransform rtResult;
    [Space]

    public Text MainText;
    public Text Description;

    public Image randImg;
    public Image boneImg;
    public Image pontImg;


    public enum StateOfMenu { 
        start=0,
        choseBone=1,
        exit=2,
        result=3
    }

    public StateOfMenu currentState;

    void Awake()
    {
        Instance = this; 
        rtPanel = GetComponent<RectTransform>();
        currentState=StateOfMenu.start;
    }


    private void Start()
    {
        
        MainText.text       = TextUI.Singelton.getLabel("Chose type of test");
        Description.text    = TextUI.Singelton.getLabel("Description of the selected test");


        var screan = UIManager.Instance.screenSize.
            Where(w => w != Vector2.zero).
            //DistinctUntilChanged().
            Subscribe(s => {
                    updateFoo(s);
            }).
            AddTo(this);

        //navedenie
        randImg.OnPointerEnterAsObservable().
            Subscribe(s => {
                Description.text = TextUI.Singelton.getLabel("Test of random questions\nbones and dots on them"); //"Тест из случайных вопросов\nкостей и точек на них";
            }).
            AddTo(this);


        boneImg.OnPointerEnterAsObservable().
            Subscribe(s => {
                Description.text = TextUI.Singelton.getLabel("Test of questions about bones");//"Тест из вопросов о костях";
            }).
            AddTo(this);

        pontImg.OnPointerEnterAsObservable().
            Subscribe(s => {
                Description.text = TextUI.Singelton.getLabel("Test of questions about points on bones");//= "Тест из вопросов о точках на костях";
            }).
            AddTo(this);

        // nagatie OnPointerDownAsObservable().
        randImg.OnPointerDownAsObservable().
            Subscribe(s => {
                currentState = StateOfMenu.exit;
                TestManager.Instance.setState(TestManager.TypeOfTest.Random);
            }).
            AddTo(this);


        boneImg.OnPointerDownAsObservable().
            Subscribe(s => {
                currentState = StateOfMenu.exit;
                TestManager.Instance.setState(TestManager.TypeOfTest.Bones);
            }).
            AddTo(this);

        pontImg.OnPointerDownAsObservable().
            Subscribe(s => {
                currentState = StateOfMenu.exit;
                TestManager.Instance.setState(TestManager.TypeOfTest.Points);
            }).
            AddTo(this);

    }

    private void updateFoo(Vector2 size)
    {
        var rtMainText      = MainText.rectTransform;
        var rtDescription   = Description.rectTransform;
        var rtrandImg       = randImg.rectTransform;
        var rtboneImg       = boneImg.rectTransform;
        var rtpontImg       = pontImg.rectTransform;

        rtPanel.sizeDelta   = new Vector2(size.x * 0.62f, size.y * 0.62f);

        FirstStep.sizeDelta         = new Vector2(rtPanel.sizeDelta.x, rtPanel.sizeDelta.y * 0.75f);
        FirstStep.anchoredPosition  = Vector2.zero;

        //PointStep.sizeDelta         = new Vector2(rtPanel.sizeDelta.x, rtPanel.sizeDelta.y * 0.75f);
        //PointStep.anchoredPosition  = Vector2.zero;

        rtMainText.sizeDelta        = new Vector2(rtPanel.sizeDelta.x, rtPanel.sizeDelta.y * 0.15f);
        rtDescription.sizeDelta     = new Vector2(rtPanel.sizeDelta.x, rtPanel.sizeDelta.y * 0.15f);

        var BlockSize = rtPanel.sizeDelta.x < rtPanel.sizeDelta.y ? rtPanel.sizeDelta.x : rtPanel.sizeDelta.y;
        rtrandImg.sizeDelta         = new Vector2(BlockSize * 0.25f, BlockSize * 0.25f);
        rtboneImg.sizeDelta         = new Vector2(BlockSize * 0.25f, BlockSize * 0.25f);
        rtpontImg.sizeDelta         = new Vector2(BlockSize * 0.25f, BlockSize * 0.25f);

        rtrandImg.anchoredPosition  = new Vector2(-FirstStep.sizeDelta.x * 0.32f, FirstStep.sizeDelta.y * 0.16f);
        rtboneImg.anchoredPosition  = new Vector2(0, FirstStep.sizeDelta.y * 0.16f);
        rtpontImg.anchoredPosition  = new Vector2(FirstStep.sizeDelta.x * 0.32f, FirstStep.sizeDelta.y * 0.16f);

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
            case StateOfMenu.result:
                exitState();
                break;
            default:
                break;
        }





    }

    private void startState()
    {
        this.gameObject.SetActive(true);
        FirstStep.gameObject.SetActive(true);
        //PointStep.gameObject.SetActive(false);
    }
        
    private void choseBoneState()
    {
        this.gameObject.SetActive(true);
        FirstStep.gameObject.SetActive(false);
        //PointStep.gameObject.SetActive(true);
    }

    private void exitState()
    {
                this.gameObject.SetActive(false);
        
    }

    private void resultState()
    {
        this.gameObject.SetActive(false);
        //rtResult.gameObject.SetActive(true);
    }




}
