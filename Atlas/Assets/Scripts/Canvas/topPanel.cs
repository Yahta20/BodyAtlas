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
    private bool state;
    private bool stateCR;
    public float SpeedOfScrole;
    private Vector2 screenSize;

    void Awake() {

        Instance = this;
        TopPanel = GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height);
        state = false;
        SpeedOfScrole = SpeedOfScrole == 0 ? 0.1f : SpeedOfScrole;

    }

    // Start is called before the first frame update
    void Start()
    {
        updateFoo();

        var movi = Moving.Instance;

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
        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
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


    void updateFoo() {
        TopPanel.sizeDelta          = new Vector2(screenSize.x*0.62f, screenSize.y*0.15f);
        TopPanel.anchoredPosition   = new Vector2(screenSize.x*0.01f, TopPanel.sizeDelta.y);
    }




    


}
