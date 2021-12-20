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

    public bool init = false;
    public bool state { get; private set; }
    
    public float SpeedOfScrole;
    
    public Text Main;
   

    [Space]
    public Image ScrollArea;
    public Image slide;
    public Image scroller;
    public Image imSlide;
    [Space]

    public Canvas mainCanvas;
    public RectTransform rtPanel;

    public GameObject Prefab;
    public GameObject Content;
    //public GameObject InfoPoint;
    private BoxCollider2D box;

    //public Dictionary<Lang, string> BoneDic { get; private set; }
    public Dictionary<Lang, string> MainTextDic { get; private set; }
    public IObservable<Vector2> changeScrean { get; private set; }

    private bool croomStart = false;
    private bool stateCR;
    private Vector2 screenSize;  
    private string chosenObj;

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
        box = GetComponent<BoxCollider2D>();
    }
        

    void Start()
    {
        //BoneDic = LangManage.instance.FindBoneDic("osseus");
        

        var movi = Moving.Instance;
        //var croom = ClassroomBeh.Instance;
        //chosenObj = croom.chosenObj.name;

        var screan = UIManager.Instance.screenSize.
            Where(w => w != Vector2.zero).
            DistinctUntilChanged().
            Subscribe(s => {
                updateFoo(s);
            }).
            AddTo(this);

        //croom.isChosenObject.
        //    
        //    Where(w => w !=stateCR).
        //    Subscribe(s => {
        //        if (s) {
        //            state = s;
        //        }
        //       stateCR=s;
        //    }).
        //    AddTo(this);

        //dvijgenia paneli
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
                if (ClassroomBeh.Instance==null)
                {
                    return;
                }
                else
                {
                    if (ClassroomBeh.Instance.Compleeted && ClassroomBeh.Instance == null)
                    {
                        createChosenObg();
                    }
                }


                stateJob(GameManager.Instance.getState());
                if (MainTextDic != null & MainTextDic.Count == 4)
                {
                    setName(MainTextDic[GameManager.Singelton.currentLang]);
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

    private void createChosenObg()
    {
        var croom = ClassroomBeh.Instance;
        
        croom.isChosenObject
             .Where(w => w != stateCR)
             .Subscribe(s => {
                  if (s)
                  {
                      state = s;
                  }
                  stateCR = s;
              })
             .AddTo(this);
    }


    public int getPointNumber(string s)
    {
        int numb = 0;
        foreach (Transform item in Content.transform)
        {
            if (item.gameObject.name == s)
            {
                return numb;
            }
            numb++;
        }
        return -1;
    }
    public void setName(string s)
    {
        Main.text = s;
    }
    private void stateJob(string[] vs)
    {

        if (!init)
        {
            if (vs[0] == "")
            {
                MainTextDic = GameManager.Instance.anatomy.TitleDic;
                //Main.text = "Anatomy";
                var list = GameManager.Instance.anatomy.GetAllPart();
                init = true;
                clearContent();
                PublishPanels(list);
                //InfoPoint.SetActive(false);
                return;
            }
            
            if (vs[1] == "")
            {
                MainTextDic = GameManager.Instance.anatomy.getSubgectDic(
                    GameManager.Instance.currentPartition
                    );
                //Main.text = GameManager.Instance.currentPartition;
                var list = GameManager.Instance.anatomy.
                    GetSubgect(GameManager.Instance.currentPartition).GetAllPart();
                init = true;
                clearContent();
                PublishPanels(list);
                //InfoPoint.SetActive(false);
                return;
            }
            if (vs[2] == "")
            {
                MainTextDic = GameManager.Instance.anatomy.getSubPartDic(
                    GameManager.Instance.currentSubpartitions
                    );
                //Main.text = GameManager.Instance.currentSubpartitions;
                var list = GameManager.Instance.anatomy.
                    GetSubgect(GameManager.Instance.currentPartition).
                    GetSubgect(GameManager.Instance.currentSubpartitions).
                    GetAllPart();
                init = true;
                clearContent();
                PublishPanels(list);
                //InfoPoint.SetActive(false);
                return;
            }
            if (vs[3] == "")
            {
                MainTextDic = LangEnv.Instance.currentBone.getBoneDic
                    (GameManager.Instance.currentItem);
                //Main.text = GameManager.Instance.currentItem;
                var list = LangEnv.Instance.currentBone.
                    getBone(GameManager.Instance.currentItem).getLatPoints();
                init = true;
                clearContent();
                PublishPanels(list);
                //InfoPoint.SetActive(false);
                return;
            }
            //if (vs[3] != "")
            //{
                //InfoPoint.SetActive(true);
                /*
                MainTextDic = LangEnv.Instance.currentBone.getBoneDic
                    (GameManager.Instance.currentItem);
                //Main.text = GameManager.Instance.currentItem;
                var list = LangEnv.Instance.currentBone.
                    getBone(GameManager.Instance.currentItem).getLatPoints();
                init = true;
                clearContent();
                PublishPanels(list);
                 */
            //    return;
            //}
        }
    }

    //razmeri panelki
    private void updateFoo(Vector2 screenSize)
    {
        rtPanel.sizeDelta          = new Vector2(screenSize.x * 0.37f, screenSize.y );
        var rtMain = Main.rectTransform;
        rtMain.sizeDelta = new Vector2(rtPanel.sizeDelta.x * 0.93f, rtPanel.sizeDelta.y * 0.08f);
        rtMain.anchoredPosition = new Vector2(0, rtPanel.sizeDelta.y * 0.01f);

        var rtScrAr = ScrollArea.rectTransform;
        rtScrAr.sizeDelta = new Vector2(rtPanel.sizeDelta.x * 0.95f, rtPanel.sizeDelta.y * 0.85f);
        rtScrAr.anchoredPosition = new Vector2(0, 0);

        var rtScroll = slide.rectTransform;
        rtScroll.sizeDelta = new Vector2(rtPanel.sizeDelta.x * 0.131f, rtPanel.sizeDelta.y * 0.35f);
        rtScroll.anchoredPosition = new Vector2(0, 0);

        var rtimSlide = imSlide.rectTransform;
        rtimSlide.sizeDelta = new Vector2(rtScroll.sizeDelta.x*0.75f, rtScroll.sizeDelta.x*0.75f);
        rtimSlide.anchoredPosition = new Vector2(0, 0);

        var rtScroller = scroller.rectTransform;
        rtScroller.sizeDelta = new Vector2(rtPanel.sizeDelta.x-rtPanel.sizeDelta.x * 0.95f, rtPanel.sizeDelta.y * 0.85f);
        rtScroller.anchoredPosition = new Vector2(0, 0);

        Image content = Content.GetComponent<Image>();

        var rtcont = content.rectTransform;
        rtcont.anchoredPosition = new Vector2(0, 0);
        if (Content.transform.childCount != 0) {
            var vlg = Content.GetComponent<VerticalLayoutGroup>();
            vlg.padding.top = (int)(rtPanel.sizeDelta.x * 0.05f);
            vlg.spacing = vlg.padding.top;
        }
        box.size = rtPanel.sizeDelta;
        box.offset = new Vector2(-(rtPanel.sizeDelta.x / 2),0) ;
    }
    void changeState() {
        state = state == false ? true : false;
    } 
    /*
    private void MakelistOfBones() {
        var list = ClassroomBeh.Instance.BoneOnScene;
        
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
        //boobleMaker.Instance.CreateUI(srtList);
    }
     */
    private void PublishPanels(string[] list) {
        int numer = 1;
        foreach (var str in list)
        {
            GameObject go = Instantiate(Prefab);
            var t = go.GetComponent<BonePointInfo>();
            t.setName(str);
            t.setGoName(str);
            t.setTranslate(getDic4BPI(str));
            t.setNumber(numer.ToString());
            go.transform.SetParent(Content.transform);
            numer++;
        }

    }

    private Dictionary<Lang, string> getDic4BPI(string s)
    {
        if (GameManager.Instance.anatomy.getSubgectDic(s)!=null)
        {
            return GameManager.Instance.anatomy.getSubgectDic(s);
        }
        if (GameManager.Instance.anatomy.getSubPartDic(s)!=null)
        {
            return GameManager.Instance.anatomy.getSubPartDic(s);
        }
        if (LangEnv.Instance.currentBone.getBoneDic(s)!=null)
        {
            return LangEnv.Instance.currentBone.getBoneDic(s);
        }
        if (LangEnv.Instance.currentBone.getPointDic(s) != null)
        {
            return LangEnv.Instance.currentBone.getPointDic(s);
        }
        return null;
    }

    private void clearContent() {
        if (Content.transform.childCount!=0) {
            foreach (Transform t in Content.transform)
            {
                Destroy(t.gameObject);
            }
        }
    }

}

