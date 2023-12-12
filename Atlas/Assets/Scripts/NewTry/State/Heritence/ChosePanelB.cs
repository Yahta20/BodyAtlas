using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChosePanelB : MonoBehaviour
{
    public RectTransform curPanel;

    public Button lernButt;
    public Button testButt;
    public Button langButt;

    public Text InfoText;
    public Text butonText;

    public Image langImage;

    public Sprite[] flags;

    Dictionary<SystemLanguage, Sprite> flagD = new();
    private void fillDic()
    {
        flagD.Add(SystemLanguage.Unknown, flags[0]);
        flagD.Add(SystemLanguage.Ukrainian, flags[1]);
        flagD.Add(SystemLanguage.English, flags[2]);
    }




    private void Start()
    {
        fillDic();
        curPanel = GetComponent<RectTransform>();
        PrintPanel(CanvasBehavior.Instance.getSize());
        CanvasBehavior.Instance.OnSizeChanged += PrintPanel;
    }

    public void PrintPanel(Vector2 screen)
    {
        //var ss = CanvasBehavior.Instance.getSize();
        curPanel.anchoredPosition = Vector2.zero;
        curPanel.sizeDelta = screen * 0.62f;

        InfoText.rectTransform.anchoredPosition = Vector2.zero;
        InfoText.rectTransform.sizeDelta =  new Vector2(0, curPanel.sizeDelta.y*0.62f);

        var lernbrt = lernButt.GetComponent<RectTransform>();
        var tstbrt = testButt.GetComponent<RectTransform>();
        var lanbrt = langButt.GetComponent<RectTransform>();

        lernbrt.sizeDelta   = new Vector2(curPanel.sizeDelta.x*0.25f,curPanel.sizeDelta.y*0.1f);
        tstbrt.sizeDelta    = new Vector2(curPanel.sizeDelta.x*0.25f,curPanel.sizeDelta.y*0.1f);
        lanbrt.sizeDelta = new Vector2(curPanel.sizeDelta.x * 0.15f, curPanel.sizeDelta.x * 0.15f);

        langButt.onClick.AddListener(() => {
            ContentLoc.Instance.changeLang();
            FillText(new string[] {
            ContentLoc.Instance.GetLocalText("grata verbum"),
            ContentLoc.Instance.GetLocalText("audit"),
            ContentLoc.Instance.GetLocalText("adsuescere")
            });
            langImage.sprite = flagD[ContentLoc.Instance.language];
        });
        langImage.sprite = flagD[ContentLoc.Instance.language];
    }

    public void FillText(string[] args) {
        InfoText.text = args[0];
        lernButt.GetComponentInChildren<Text>().text = args[1];
        testButt.GetComponentInChildren<Text>().text = args[2];
    }

    public void SetIplementing(UnityAction left,UnityAction right) { 
        lernButt.onClick.RemoveAllListeners();
        testButt.onClick.RemoveAllListeners();
        lernButt.onClick.AddListener(left);
        testButt.onClick.AddListener(right);
    }






}