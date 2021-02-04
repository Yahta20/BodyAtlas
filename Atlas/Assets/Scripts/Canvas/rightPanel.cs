using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class rightPanel : MonoBehaviour
{

    public static rightPanel Instance;
    public Image image;
    public Text Main;
    public float SpeedOfScrole;
    public GameObject Pref;
    public GameObject Content;


    private RectTransform TopPanel;
    private bool state;
    private bool stateCR;
    private Vector2 screenSize;



    void Awake()
    {
        Instance = this;

        TopPanel = GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height);
        state = false;
        stateCR = false;
        SpeedOfScrole = SpeedOfScrole == 0 ? 0.1f : SpeedOfScrole;

    }


    void changeState() {
        
        state = state == false ? true : false;
    }

    void changeStateCR()
    {
        stateCR = stateCR == false ? true : false;
    }

    void Start()
    {

        var movi  = Moving.Instance;
        var croom = ClassroomBeh.Instance;
        var lang  = LangManage.Instance;

        croom.isChosenObject.
            Where(w => w !=stateCR).
            Subscribe(s => {
               //print(s+" "+ stateCR);
                if (s) {
                    state = s;
                }
               stateCR=s;
            }).
            AddTo(this);

        movi.supportPanel.
            Where(w => w != false).
            Subscribe(s=> {changeState();}).
            AddTo(this);



        image.OnPointerDownAsObservable().
            Subscribe(s => {
                changeState();
            }).
            AddTo(this);
        
        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                //updateFoo();
                if (state)
                {
                    var x = Mathf.Lerp(TopPanel.anchoredPosition.x, 0, SpeedOfScrole);
                    TopPanel.anchoredPosition = new Vector2(x, TopPanel.anchoredPosition.y);

                }
                else
                {
                    var x = Mathf.Lerp(TopPanel.anchoredPosition.x, TopPanel.sizeDelta.x, SpeedOfScrole);
                    TopPanel.anchoredPosition = new Vector2(x, TopPanel.anchoredPosition.y);
                }
                if (croom.chosenObj.name != "empty")
                {
                    Main.text = croom.chosenObj.name;

                }
                else {
                    Main.text = "Sceleton";
                }
            })
            .AddTo(this);
                    

    }
}

