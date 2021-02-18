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
    }
    private void Start()
    {
        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(_ =>
            {
                if (TransLang!=null & TransLang.Count == 4) {
                    setName(TransLang[GameEnviroment.Singelton.languageInfo]);
                }
            }).AddTo(this);
                   

               //setName(TransLang[GameEnviroment.Singelton.languageInfo]);


    }
}
