using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class BonePointInfo : MonoBehaviour
{
    public Text Number;
    public Text Name;
    public Dictionary<Lang, string> TransLang {get; private set;}
    public RectTransform rtPref;
    public rightPanel rp;

    public void setNumber(string s) {
        Number.text= s;
    }

    public void setName(string s) { 
        Name.text = s;
        //gameObject.name = s;
    }
    public void setGoName(string s)
    {
        //Name.text = s;
        gameObject.name = s;
    }
    public void setTranslate(Dictionary<Lang, string> d) {
        TransLang = d;
    }
    private void Awake()
    {
        
        TransLang = new Dictionary<Lang, string>();
        rtPref = GetComponent<RectTransform>();

    }
    private void Start()
    {
        var uiMng = UIManager.Instance;
        rp = rightPanel.Instance;

        uiMng.screenSize.
            Where(w => w != Vector2.zero).
            DistinctUntilChanged().
            Subscribe(s => {
                updateFoo();//s
            } )
            .AddTo(this);

        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(_ =>
            {
                if (TransLang!=null & TransLang.Count == 4) {
                    setName(TransLang[GameEnviroment.Singelton.languageInfo]);
                }


            }).AddTo(this);
    }

    public void updateFoo(
        //Vector2 Screen
        ) {
        //print(rp.scroller.rectTransform.sizeDelta);
        rtPref.sizeDelta = new Vector2(rp.ScrollArea.rectTransform.sizeDelta.x*0.95f, rp.ScrollArea.rectTransform.sizeDelta.y*0.10f);
        rtPref.anchoredPosition = new Vector2(0, 0);

        var rtNumber   = Number.rectTransform;
        var rtName     = Name.rectTransform;

        rtNumber.sizeDelta = new Vector2(rtPref.sizeDelta.x*0.1f, rtPref.sizeDelta.y*0.9f);
        rtNumber.anchoredPosition = new Vector2(0, 0);

        rtName.sizeDelta = new Vector2(rtPref.sizeDelta.x*0.89f, rtPref.sizeDelta.y*0.9f);
        rtName.anchoredPosition = new Vector2(rtPref.sizeDelta.x * 0.06f, 0);

    }





    //setName(TransLang[GameEnviroment.Singelton.languageInfo]);


}
