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

    public bool init = false;
    private string chosenObj;


    void Awake()
    {

        Instance = this;
        TopPanel = GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height);
        state = false;
        stateCR = false;
        SpeedOfScrole = SpeedOfScrole == 0 ? 0.1f : SpeedOfScrole;
        init = false;

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
        //print(s+" "+ stateCR);
        chosenObj = croom.chosenObj.name;

        croom.isChosenObject.
            Where(w => w !=stateCR).
            Subscribe(s => {
                if (s) {
                    state = s;
                }
               stateCR=s;
            }).
            AddTo(this);

        croom.isChangedObj.Where(s => s != chosenObj).Subscribe(
            s=> {
                s = chosenObj;
                init = false;
            }).AddTo(this);

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
                if (croom.chosenObj.name == "empty")
                {
                    Main.text = "Sceleton";
                    if (!init) {
                        if (croom.objOnScene.Count!=0) { 
                        
                        clearContent();
                        MakelistOfBones();
                        print("A");
                        init=true;
                        chosenObj = croom.chosenObj.name;
                        }
                    }
                }

                if (croom.chosenObj.name != "empty")
                {
                    Main.text = croom.chosenObj.name;
                    if (!init)
                    {
                        clearContent();
                        MakelistOfPoints();
                        init = true;
                        print("a");
                        chosenObj = croom.chosenObj.name;
                    }
                }
                    
                /*
                 */

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
        PublishList(listOname);
        //print("List of bones");
    }
    private void MakelistOfPoints()
    {
        //var chosen = ;
        var listOname = new List<string>();

        foreach (var item in ClassroomBeh.Instance.chosenObj.GetComponent<BoneBih>().specPlases)
        {
            listOname.Add(item.name);
        }
        PublishList(listOname);
    }

    private void PublishList(List<string> srtList) {
        int numer = 1;
        foreach (var str in srtList)
        {
            GameObject go = Instantiate(Prefab);
            var t= go.GetComponent<BonePointInfo>();
            t.setName(str);
            t.setNumber(numer.ToString());
            go.transform.SetParent(Content.transform);
            numer++;

        }

    }
    /*
    public void addQuestion(question q, int i, AudioClip ac)
    {
        go.GetComponent<questionScript>().setAudio(ac);
        questions[i] = go;
    }
    */

    private void clearContent() {
        if (Content.transform.childCount!=0) {
            foreach (Transform t in Content.transform)
            {
                Destroy(t.gameObject);
            }
        }
    }

}

