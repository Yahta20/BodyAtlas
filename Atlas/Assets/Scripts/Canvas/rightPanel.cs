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

    public Image ScrollArea;
    public Image slide;
    //public 
    public Image scroller;

    public Text Main; 


    public float SpeedOfScrole;
    public bool init = false;
    public bool state { get; private set; }

    private bool stateCR;
    private Vector2 screenSize;
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
    void Start()
    {
        //updateFoo();
        /*
        правильную регистрацию изменения экрана
         
         */
        var movi = Moving.Instance;
        var croom = ClassroomBeh.Instance;
        //print(s+" "+ stateCR);
        chosenObj = croom.chosenObj.name;

        topPanel.Instance.changeScrean.
            Where(x => x == true).
            Subscribe(_ => { updateFoo();}).
            AddTo(this);

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

        slide.OnPointerDownAsObservable().
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
                        chosenObj = croom.chosenObj.name;
                    }
                }
                    
                /*
                 */

            })
            .AddTo(this);
                    

    }


    private void updateFoo()
    {
        //main panel
        TopPanel.sizeDelta          = new Vector2(screenSize.x * 0.37f, screenSize.y);
        TopPanel.anchoredPosition   = new Vector2(0, 0);

        
        //text of bone
        var rtMain = Main.rectTransform;
        rtMain.sizeDelta = new Vector2(TopPanel.sizeDelta.x * 0.9f, TopPanel.sizeDelta.y * 0.15f);
        rtMain.anchoredPosition = new Vector2(0, -TopPanel.sizeDelta.y * 0.01f);

        //background
        var rtScrAr = ScrollArea.rectTransform;
        rtScrAr.sizeDelta = new Vector2(TopPanel.sizeDelta.x * 0.95f, TopPanel.sizeDelta.y * 0.85f);
        rtScrAr.anchoredPosition = new Vector2(0, 0);

        var rtScroll = slide.rectTransform;
        rtScroll.sizeDelta = new Vector2(TopPanel.sizeDelta.x * 0.131f, TopPanel.sizeDelta.x * 0.131f);
        rtScroll.anchoredPosition = new Vector2(0, 0);

        var rtScroller = scroller.rectTransform;
        rtScroller.sizeDelta = new Vector2(TopPanel.sizeDelta.x-TopPanel.sizeDelta.x * 0.95f, TopPanel.sizeDelta.y * 0.85f);
        rtScroller.anchoredPosition = new Vector2(0, 0);

        Image content = Content.GetComponent<Image>();

        var rtcont = content.rectTransform;
        rtcont.anchoredPosition = new Vector2(0, 0);
        //print(Content.transform.childCount);
        if (Content.transform.childCount != 0) {
            var vlg = Content.GetComponent<VerticalLayoutGroup>();
            vlg.padding.top = (int)(TopPanel.sizeDelta.x * 0.05f);
            vlg.spacing = vlg.padding.top;
            //rtcont.sizeDelta = new Vector2(rtScrAr.sizeDelta.x, Content.transform.childCount*30);
           //
           //foreach (var item in Content.transform)
           //{
           //    var img = 
           //}
        }


    }

    


    void changeState() {
        
        state = state == false ? true : false;
    }
    void changeStateCR()
    {
        stateCR = stateCR == false ? true : false;
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
        PublishList(listOname,true);
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
        PublishList(listOname,false);
    }

    private void PublishList(List<string> srtList,bool bone) {
        int numer = 1;
        if (bone) { 
            
        }
        foreach (var str in srtList)
        {
            GameObject go = Instantiate(Prefab);
            var t= go.GetComponent<BonePointInfo>();
            t.setName(str);
            t.setGoName(str);
            t.setNumber(numer.ToString());
            go.transform.SetParent(Content.transform);
            numer++;
        }
        updateFoo();

    }
    private void clearContent() {
        if (Content.transform.childCount!=0) {
            foreach (Transform t in Content.transform)
            {
                Destroy(t.gameObject);
            }
        }
    }





    /*
    public void addQuestion(question q, int i, AudioClip ac)
    {
        go.GetComponent<questionScript>().setAudio(ac);
        questions[i] = go;
    }
    */


}

