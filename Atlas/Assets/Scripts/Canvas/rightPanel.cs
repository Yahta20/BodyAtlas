using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class rightPanel : MonoBehaviour
{
    public static rightPanel Instance;

    public RectTransform rtPanel;
    public GameObject Prefab;
    public GameObject Content;

    public Image ScrollArea;
    public Image slide;
    
    public Image scroller;

    public Text Main;

    public Canvas mainCanvas;
    public float SpeedOfScrole;
    public bool init = false;
    public bool state { get; private set; }

    private bool stateCR;
    private Vector2 screenSize;
    private string chosenObj;

    public Dictionary<Lang, string> BoneDic { get; private set; }
    public Dictionary<Lang, string> BoneNameDic { get; private set; }
    public IObservable<Vector2> changeScrean { get; private set; }



    void Awake()
    {
        Instance = this;
        rtPanel = GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height);
        state = false;
        stateCR = false;
        SpeedOfScrole = SpeedOfScrole == 0 ? 0.1f : SpeedOfScrole;
        init = false;
        changeScrean = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                if (mainCanvas!=null)
                {
                    return mainCanvas.pixelRect.size;
                }

                return new Vector2(1024,768);
            });


    }


    
    
    void Start()
    {
        //updateFoo();
        /*
        правильную регистрацию изменения экрана
         
         */
        BoneDic = LangManage.instance.FindBoneDic("osseus");

        var movi = Moving.Instance;
        var croom = ClassroomBeh.Instance;
        //print(s+" "+ stateCR);
        chosenObj = croom.chosenObj.name;

        //topPanel.Instance.changeScrean.
        //    Where(x => x == true).
        //    Subscribe(_ => { updateFoo();}).
        //   AddTo(this);
        var screan = UIManager.Instance.screenSize.
            Where(w => w != Vector2.zero).
            DistinctUntilChanged().
            Subscribe(s => {
                updateFoo(s);
            }).
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
                if (croom.chosenObj.name == "empty")
                {
                    //Main.text = "Sceleton";
                    Main.text = BoneDic[GameEnviroment.Singelton.languageInfo];
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
                    BoneNameDic = LangManage.instance.FindBoneDic(croom.chosenObj.name);
                    Main.text = BoneNameDic[GameEnviroment.Singelton.languageInfo];

                    if (!init)
                    {
                        clearContent();
                        MakelistOfPoints();
                        init = true;
                        chosenObj = croom.chosenObj.name;
                    }
                }
                if (state)
                {
                    var x = Mathf.Lerp(rtPanel.anchoredPosition.x, 0, SpeedOfScrole);
                    rtPanel.anchoredPosition = new Vector2(x, rtPanel.anchoredPosition.y);
                }
                else
                {
                    var x = Mathf.Lerp(rtPanel.anchoredPosition.x, rtPanel.sizeDelta.x, SpeedOfScrole);
                    rtPanel.anchoredPosition = new Vector2(x, rtPanel.anchoredPosition.y);
                }

            })
            .AddTo(this);
                    

    }


    private void updateFoo(Vector2 screenSize)
    {
        rtPanel.sizeDelta          = new Vector2(screenSize.x * 0.37f, screenSize.y );
        //main panel
        //rtPanel.anchoredPosition   = new Vector2(0, 0);

        //text of bone
        var rtMain = Main.rectTransform;
        rtMain.sizeDelta = new Vector2(rtPanel.sizeDelta.x, rtPanel.sizeDelta.y * 0.08f);
        rtMain.anchoredPosition = new Vector2(0, rtPanel.sizeDelta.y * 0.01f);

        //background
        var rtScrAr = ScrollArea.rectTransform;
        rtScrAr.sizeDelta = new Vector2(rtPanel.sizeDelta.x * 0.95f, rtPanel.sizeDelta.y * 0.85f);
        rtScrAr.anchoredPosition = new Vector2(0, 0);

        var rtScroll = slide.rectTransform;
        rtScroll.sizeDelta = new Vector2(rtPanel.sizeDelta.x * 0.131f, rtPanel.sizeDelta.y * 0.35f);
        rtScroll.anchoredPosition = new Vector2(0, 0);

        var rtScroller = scroller.rectTransform;
        rtScroller.sizeDelta = new Vector2(rtPanel.sizeDelta.x-rtPanel.sizeDelta.x * 0.95f, rtPanel.sizeDelta.y * 0.85f);
        rtScroller.anchoredPosition = new Vector2(0, 0);

        Image content = Content.GetComponent<Image>();

        var rtcont = content.rectTransform;
        rtcont.anchoredPosition = new Vector2(0, 0);
        //print(Content.transform.childCount);
        if (Content.transform.childCount != 0) {
            var vlg = Content.GetComponent<VerticalLayoutGroup>();
            vlg.padding.top = (int)(rtPanel.sizeDelta.x * 0.05f);
            vlg.spacing = vlg.padding.top;
        }
            //rtcont.sizeDelta = new Vector2(rtScrAr.sizeDelta.x, Content.transform.childCount*30);
           //
           //foreach (var item in Content.transform)
           //{
           //    var img = 
           //}


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
            if (name != "osseus")
            {
                listOname.Add(name);
            }
        }
        PublishList(listOname,true);
    }


    private void MakelistOfPoints()
    {
        var listOname = new List<string>();
        foreach (var item in ClassroomBeh.Instance.chosenObj.GetComponent<BoneBih>().specPlases)
        {
            listOname.Add(item.name);
        }
        PublishList(listOname,false);
    }



    private void PublishList(List<string> srtList,bool bone) {
        int numer = 1;
        
            
        foreach (var str in srtList)
        {
            GameObject go = Instantiate(Prefab);
            var t = go.GetComponent<BonePointInfo>();
            t.setGoName(str);
            t.setNumber(numer.ToString());
            if (bone)
            {
                t.setTranslate(LangManage.instance.FindBoneDic(str));
            }
            else {
                t.setTranslate(LangManage.instance.FindPointDic(str));
            }
            go.transform.SetParent(Content.transform);
            numer++;
        }
        boobleMaker.Instance.CreateUI(srtList);
    }

            
    
    private void clearContent() {
        if (Content.transform.childCount!=0) {
            foreach (Transform t in Content.transform)
            {
                Destroy(t.gameObject);
            }
        }
    }





        //updateFoo();
        //var chosen = ;
        //print("List of bones");
    /*
    public void addQuestion(question q, int i, AudioClip ac)
    {
        go.GetComponent<questionScript>().setAudio(ac);
        questions[i] = go;
    }
    */
        //if (LangManage.instance.bones!=null) {
        //   //print(LangManage.instance.bones.Count);
        //}
        //t.setName(LangManage.instance.FindBone(str));
        //t.setName(LangManage.instance.FindPoint(str));


}

