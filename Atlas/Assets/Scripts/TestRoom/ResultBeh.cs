using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class ResultBeh : MonoBehaviour
{
    public RectTransform rtMain;
    public RectTransform rtListOfFail;


    [Space]
    public Text  ResulTxt;
    public Text  retryTxt;
    public Text  exitTxt;

    [Space]
    public Image exitImg;
    public Image retryImg;

        
    void Start()
    {
        var TMB = TestMenuBeh.Instance;

        var screan = UIManager.Instance.screenSize.
             Where(w => w != Vector2.zero).
             //DistinctUntilChanged().
             Subscribe(s => {
                 if (TMB.currentState == TestMenuBeh.StateOfMenu.result)
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
        exitImg .rectTransform.sizeDelta = new Vector2(rtMain.sizeDelta.x*0.12f, rtMain.sizeDelta.x * 0.12f);
        retryImg.rectTransform.sizeDelta = new Vector2(rtMain.sizeDelta.x*0.12f, rtMain.sizeDelta.x * 0.12f);


        //        (rtMain.sizeDelta.x - rtListOfFail.sizeDelta.x - exitImg .rectTransform.sizeDelta.x)/2
        retryTxt.rectTransform.anchoredPosition = new Vector2(
            -(rtMain.sizeDelta.x - rtListOfFail.sizeDelta.x - exitImg.rectTransform.sizeDelta.x) / 2,
            (rtMain.sizeDelta.x - rtListOfFail.sizeDelta.x - exitImg.rectTransform.sizeDelta.x) / 2
            );
        exitTxt.rectTransform.anchoredPosition = new Vector2(
            (rtMain.sizeDelta.x - rtListOfFail.sizeDelta.x - exitImg.rectTransform.sizeDelta.x) / 2
            , exitTxt.rectTransform.sizeDelta.y + exitTxt.rectTransform.sizeDelta.y + retryTxt.rectTransform.anchoredPosition.x
            );






        //retryTxt.sizeDelta = new Vector2(size.x * 0.62f, size.y * 0.62f);
        //exitTxt .sizeDelta = new Vector2(size.x * 0.62f, size.y * 0.62f);

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
