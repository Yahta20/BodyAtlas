using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ResultBeh : MonoBehaviour
{
    public static ResultBeh Instance;

    public RectTransform rtMain;
    public RectTransform rtListOfFail;
    [Space]
    public Text  ResulTxt;
    public Text  retryTxt;
    public Text  exitTxt;
    [Space]
    public Image exitImg;
    public Image retryImg;
    [Space]
    public GameObject RPref;
    


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResulTxt.text = TextUI.Singelton.getLabel("Retry");
        retryTxt.text = TextUI.Singelton.getLabel("Retry");
        exitTxt .text = TextUI.Singelton.getLabel("Exit");

        var TMB = TestManager.Instance;

        var screan = UIManager.Instance.screenSize.
             Where(w => w != Vector2.zero).
             //DistinctUntilChanged().
             Subscribe(s => {
                 //print("w");
                 if (TMB.currentState == TestManager.TypeOfTest.Finish)
                 {
                     this.gameObject.SetActive(true);
                     updateFoo(s);
                 }
                 else {
                     this.gameObject.SetActive(false);
                 }
             }).
             AddTo(this);



        exitImg.OnPointerDownAsObservable().
            Subscribe(s => {
                startScene();
            }).
            AddTo(this);
        retryImg.OnPointerDownAsObservable().
            Subscribe(s => {
                rebootScene();
            }).
            AddTo(this);


        //var boneUpdate = Observable.EveryLateUpdate()
        //    .Subscribe(
        //    s => {
        //    
        //        
        //
        //    })
        //    .AddTo(this);
    //retryTxt.sizeDelta = new Vector2(size.x * 0.62f, size.y * 0.62f);
    //exitTxt .sizeDelta = new Vector2(size.x * 0.62f, size.y * 0.62f);
    }

    private void updateFoo(Vector2 size)
    {
        rtMain.sizeDelta = new Vector2(size.x * 0.62f, size.y * 0.62f);

        ResulTxt.rectTransform.sizeDelta        = new Vector2(rtMain.sizeDelta.x, rtMain.sizeDelta.y * 0.25f);
        ResulTxt.rectTransform.anchoredPosition = Vector2.zero;

        rtListOfFail.sizeDelta          = new Vector2(rtMain.sizeDelta.x*0.84f, rtMain.sizeDelta.y * 0.75f);
        rtListOfFail.anchoredPosition   = Vector2.zero;

        retryTxt.rectTransform.sizeDelta = new Vector2(rtMain.sizeDelta.x*0.12f, rtMain.sizeDelta.y * 0.07f);
        exitTxt .rectTransform.sizeDelta = new Vector2(rtMain.sizeDelta.x*0.12f, rtMain.sizeDelta.y * 0.07f);
        retryTxt.rectTransform.anchoredPosition = Vector2.zero;
        exitTxt.rectTransform. anchoredPosition = Vector2.zero;

        exitImg .rectTransform.sizeDelta = new Vector2(rtMain.sizeDelta.x*0.12f, rtMain.sizeDelta.x * 0.12f);
        retryImg.rectTransform.sizeDelta = new Vector2(rtMain.sizeDelta.x*0.12f, rtMain.sizeDelta.x * 0.12f);


        //        (rtMain.sizeDelta.x - rtListOfFail.sizeDelta.x - exitImg .rectTransform.sizeDelta.x)/2
        retryImg.rectTransform.anchoredPosition = new Vector2(
            -(rtMain.sizeDelta.x - rtListOfFail.sizeDelta.x - exitImg.rectTransform.sizeDelta.x) / 2,
            (rtMain.sizeDelta.x - rtListOfFail.sizeDelta.x - exitImg.rectTransform.sizeDelta.x) / 2
            );
        exitImg.rectTransform.anchoredPosition = new Vector2(
            -(rtMain.sizeDelta.x - rtListOfFail.sizeDelta.x - exitImg.rectTransform.sizeDelta.x) / 2
            , exitTxt.rectTransform.sizeDelta.y + exitImg.rectTransform.sizeDelta.y + retryImg.rectTransform.anchoredPosition.y*2
            );

    }

    public void ClearListQuestions() {
        if (rtListOfFail.transform.childCount != 0)
        {
            foreach (Transform t in rtListOfFail.transform)
            {
                Destroy(t.gameObject);
            }
        }
    }

    public void CreatingResults(string[] ansv,List<string> cheker) {
        int Index = 0;
        if (ansv.Length==cheker.Count) {
            for (int i = 0; i < ansv.Length; i++)
            {
                var go = Instantiate(RPref);
                var sgo = go.GetComponent<DesigionBeh>();
                var info = new string[3]  {(i+1).ToString(),ansv[i],cheker[i] };
                sgo.setDesigion(info);
                if (sgo.backgroundC.color== Color.green) 
                {
                    Index++;
                }
                go.transform.SetParent(rtListOfFail);
                //tNumber.text=args[0];
                //tWrong.text = args[1];
                //tRight.text = args[2];
                ///sgo.
            }
        }
        int acc = (Index / cheker.Count * 100);

        string resik = $"{Index} / {cheker.Count} {TextUI.Singelton.getLabel("correct ansvers")}" ;

        ResulTxt.text = resik;// 
    }

    public void startScene()
    {
        SceneManager.LoadScene(0);
    }

    public void rebootScene()
    {
        SceneManager.LoadScene(2);
    }
}
