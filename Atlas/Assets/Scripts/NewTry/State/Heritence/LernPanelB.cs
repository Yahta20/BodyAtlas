using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LernPanelB : MonoBehaviour
{
    public GameObject prefab;
    [Space]
    RectTransform mainRect;
    public ScrollRect scroll;
    public Button lanbtn;
    public Button upbtn;
    public Button extbtn;
    public Button hide;
    public Text text;
    public Image flag;


    public Sprite[] flags;

    bool visible = true;

    Dictionary<SystemLanguage, Sprite> flagD = new();
    private void fillDic()
    {
        flagD.Add(SystemLanguage.Unknown, flags[0]);
        flagD.Add(SystemLanguage.Ukrainian, flags[1]);
        flagD.Add(SystemLanguage.English, flags[2]);
    }

    private void Awake()
    {
        fillDic();
    }

    private void OnEnable()
    {
        //print("SASA");
    }


    void Start()
    {
        mainRect = GetComponent<RectTransform>();
        UpdateFunctional();
        SetSizes(CanvasBehavior.Instance.getSize());
        Control.Instance.OnChangePoint += UpdateContent;
        ContentLoc.Instance.OnChangeLang += UpdateLang;
        Control.Instance.HomePosition();
    }

    private void UpdateContent(GameObject @object)
    {
        text.text = ContentLoc.Instance.GetLocalText(@object.name);// ;
        for (int i = 0; i < scroll.content.transform.childCount; i++)
        {
            Destroy(scroll.content.transform.GetChild(i).gameObject);
        }
        scroll.content.sizeDelta = Vector2.zero;

        var content = Control.Instance.getContent();
        //print(content.Length);
        for (int i = 0; i < content.Length; i++)
        {
            var butun = Instantiate(prefab, scroll.content);
            butun.GetComponent<RectTransform>().sizeDelta
                = new Vector2(
                        butun.GetComponent<RectTransform>().sizeDelta.x,
                        CanvasBehavior.Instance.getSize().y * 0.15f);
            butun.name = content[i];
            butun.GetComponentInChildren<Text>().text = ContentLoc.Instance.GetLocalText(content[i]);//
            var a = content[i].ToString();

            butun.GetComponent<Button>().onClick.AddListener(() => {
                Control.Instance.ChangePoint(a);
            });
            scroll.content.sizeDelta += new Vector2(0
                , CanvasBehavior.Instance.getSize().y * 0.15f + 25);
        }

    }

    private void UpdateLang()
    {
        flag.sprite = flagD[ContentLoc.Instance.language];
        UpdateContent(Control.Instance.Postparat);
    }

    private void UpdateFunctional()
    {
        lanbtn  .onClick.RemoveAllListeners();
        upbtn   .onClick.RemoveAllListeners();
        hide    .onClick.RemoveAllListeners();
        lanbtn  .onClick.AddListener(() => { ContentLoc.Instance.changeLang(); });
        upbtn   .onClick.AddListener(() => { Control.Instance.UpperHierarchy(); });
        hide    .onClick.AddListener(() => { visible = !visible; });
    }
     
    public void SetExtImplemets(UnityAction a) { 
        extbtn  .onClick.RemoveAllListeners();
        extbtn  .onClick.AddListener(a);
    }
    


    private void SetSizes(Vector2 sc)
    {
        //var sc = CanvasBehavior.Instance.getSize();
        //var aspekt = sc.x / sc.y;

        mainRect.sizeDelta = new Vector2(sc.x * 0.25f, sc.y);
        mainRect.anchoredPosition = Vector2.zero;

        scroll.GetComponent<RectTransform>().sizeDelta = new Vector2(sc.x * 0.25f, mainRect.sizeDelta.y * 0.80f);
        text.GetComponent<RectTransform>().sizeDelta = new Vector2(sc.x * 0.25f, mainRect.sizeDelta.y * 0.10f);
        lanbtn.GetComponent<RectTransform>().sizeDelta = new Vector2    (mainRect.sizeDelta.y * 0.10f, mainRect.sizeDelta.y * 0.10f);
        upbtn.GetComponent<RectTransform>().sizeDelta = new Vector2     (mainRect.sizeDelta.y * 0.10f,   mainRect.sizeDelta.y * 0.10f);
        extbtn.GetComponent<RectTransform>().sizeDelta = new Vector2    (mainRect.sizeDelta.y * 0.10f,  mainRect.sizeDelta.y * 0.10f);
        hide.GetComponent<RectTransform>().sizeDelta = new Vector2      (mainRect.sizeDelta.y * 0.10f,    mainRect.sizeDelta.y * 0.10f);

        text.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -mainRect.sizeDelta.y * 0.10f);
        lanbtn.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        extbtn.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        upbtn.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        hide.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        /*
        icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-extbtn.sizeDelta.y * 2, 0);
        toppanel.sizeDelta = new Vector2(sc.x * 0.25f, mainRect.sizeDelta.y * 0.10f);




         */

    }

    // Update is called once per frame
    void Update()
    {
        ChekVisible();
    }
    private void ChekVisible()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ChangeView();
        //}
        if (visible)
        {
            mainRect.anchoredPosition = new Vector2(Mathf.Lerp(mainRect.anchoredPosition.x, 0, Time.deltaTime * 3), 0);

        }
        else
        {
            mainRect.anchoredPosition = new Vector2(Mathf.Lerp(mainRect.anchoredPosition.x, mainRect.sizeDelta.x, Time.deltaTime * 3), 0);
        }

    }
}
