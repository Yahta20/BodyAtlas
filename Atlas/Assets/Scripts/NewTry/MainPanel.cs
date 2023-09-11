using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    RectTransform mainRect;
    public RectTransform toppanel;
    public RectTransform scrollRect;
    public RectTransform lanbtn;
    public RectTransform upbtn;
    public RectTransform extbtn;
    public RectTransform text;
    public RectTransform icon;


    [Space]
    public Image BtnImage;
    public Sprite[] button;

    [SerializeField]
    int lang;
    [SerializeField]
    float xkof;


    bool visible = true;
    // Start is called before the first frame update
    void Start()
    {
        mainRect = GetComponent<RectTransform>();
        SetSizes();
        //ContentLoc.Instance.OnChangeLang += ChangeLang;
    }

    private void SetSizes()
    {
        var sc = CanvasBehavior.Instance.getSize();
        var aspekt = sc.x / sc.y;

        mainRect.sizeDelta = new Vector2(sc.x * xkof, sc.y);
        mainRect.anchoredPosition = Vector2.zero;

        scrollRect.sizeDelta = new Vector2(sc.x * xkof, mainRect.sizeDelta.y * 0.80f);
        toppanel.sizeDelta = new Vector2(sc.x * xkof, mainRect.sizeDelta.y * 0.10f);
        text.sizeDelta = toppanel.sizeDelta;



        lanbtn.sizeDelta = new Vector2(text.sizeDelta.x*0.25f, text.sizeDelta.x * 0.25f);
        upbtn.sizeDelta = new Vector2(text.sizeDelta.x * 0.25f, text.sizeDelta.x * 0.25f);
        extbtn.sizeDelta = new Vector2(text.sizeDelta.x * 0.25f, text.sizeDelta.x * 0.25f);
        icon.sizeDelta = new Vector2(text.sizeDelta.x * 0.25f, text.sizeDelta.x * 0.25f);
        
        lanbtn.anchoredPosition = Vector2.zero;
        extbtn.anchoredPosition = Vector2.zero;
        upbtn.anchoredPosition = new Vector2(extbtn.sizeDelta.y * 2, 0);
        icon.anchoredPosition = new Vector2(-extbtn.sizeDelta.y * 2, 0);


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

    public void ChangeLang()
    {
        switch (ContentLoc.Instance.language)
        {
            case SystemLanguage.Afrikaans:
                break;
            case SystemLanguage.Arabic:
                break;
            case SystemLanguage.Basque:
                break;
            case SystemLanguage.Belarusian:
                break;
            case SystemLanguage.Bulgarian:
                break;
            case SystemLanguage.Catalan:
                break;
            case SystemLanguage.Chinese:
                break;
            case SystemLanguage.Czech:
                break;
            case SystemLanguage.Danish:
                break;
            case SystemLanguage.Dutch:
                break;
            case SystemLanguage.English:
                ContentLoc.Instance.changeLang(SystemLanguage.Unknown);
                BtnImage.sprite = button[2];
                break;
            case SystemLanguage.Estonian:
                break;
            case SystemLanguage.Faroese:
                break;
            case SystemLanguage.Finnish:
                break;
            case SystemLanguage.French:
                break;
            case SystemLanguage.German:
                break;
            case SystemLanguage.Greek:
                break;
            case SystemLanguage.Hebrew:
                break;
            case SystemLanguage.Icelandic:
                break;
            case SystemLanguage.Indonesian:
                break;
            case SystemLanguage.Italian:
                break;
            case SystemLanguage.Japanese:
                break;
            case SystemLanguage.Korean:
                break;
            case SystemLanguage.Latvian:
                break;
            case SystemLanguage.Lithuanian:
                break;
            case SystemLanguage.Norwegian:
                break;
            case SystemLanguage.Polish:
                break;
            case SystemLanguage.Portuguese:
                break;
            case SystemLanguage.Romanian:
                break;
            case SystemLanguage.Russian:
                break;
            case SystemLanguage.SerboCroatian:
                break;
            case SystemLanguage.Slovak:
                break;
            case SystemLanguage.Slovenian:
                break;
            case SystemLanguage.Spanish:
                break;
            case SystemLanguage.Swedish:
                break;
            case SystemLanguage.Thai:
                break;
            case SystemLanguage.Turkish:
                break;
            case SystemLanguage.Ukrainian:
                ContentLoc.Instance.changeLang(SystemLanguage.English);
                BtnImage.sprite = button[1];
                break;
            case SystemLanguage.Vietnamese:
                break;
            case SystemLanguage.ChineseSimplified:
                break;
            case SystemLanguage.ChineseTraditional:
                break;
            case SystemLanguage.Unknown:
                ContentLoc.Instance.changeLang(SystemLanguage.Ukrainian);
                BtnImage.sprite = button[0];
                break;
            case SystemLanguage.Hungarian:
                break;
            default:
                break;
        }

    }
        //ContentLoc.Instance.OnChangeLang();


    public void Upstarse()
    {

        Control.Instance.UpperHierarchy();
    }

    public void Exit()
    {

    }

    public void ChangeView() {
        visible = !visible;
        SetSizes();
    }


}
