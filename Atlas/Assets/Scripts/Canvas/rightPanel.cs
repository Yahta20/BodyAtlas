using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class rightPanel : MonoBehaviour
{

    public static rightPanel Instance;
    public RectTransform TopPanel;
    public GameObject Prefab;
    public GameObject Content;



    public Image image;
    public Text Main; 

    public float SpeedOfScrole;

    private bool state;
    private bool stateCR;
    private Vector2 screenSize;

    private bool init = false;



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
        var movi = Moving.Instance;
        var croom = ClassroomBeh.Instance;
        
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
                else
                {
                    Main.text = "Sceleton";
                    
                }

            })
            .AddTo(this);
                    

    }
    private void MakelistOfBones() {
        var list = ClassroomBeh.Instance.objOnScene;
        var listOname = new List<string>();
        foreach (var item in list)
        {
            var name = item.gameObject.name;
            if (name != "Skeleton")
            {
                listOname.Add(name);
            }
        }

    }
    private void MakelistOfPoints()
    {
        //var chosen = ;
        var listOname = new List<string>();

        foreach (var item in ClassroomBeh.Instance.chosenObj.GetComponent<BoneBih>().specPlases)
        {
            listOname.Add(item.name);
        }

    }

    private void PublishList() { 
        



    }

}

