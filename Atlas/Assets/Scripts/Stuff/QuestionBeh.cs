using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class QuestionBeh : MonoBehaviour
{
    public Image back;
    public Text name;
    private Dictionary<Lang, string> dicName;

    void Start()
    {
        
        var testManager = TestManager.Instance;

        var chosen = Observable.EveryFixedUpdate()
            .Subscribe(_ => langUpdate()).AddTo(this);

        back.OnPointerDownAsObservable().
            Subscribe(s => {
                testManager.setAnswer(dicName[Lang.lat]);//gameObject.name
            }).
            AddTo(this);
    }

    public void setName(string s) {
        name.text = s;
    }

    public void setGOName(string nam) {
        gameObject.name = nam;
        dicName = LangEnv.Singelton.currentBone.getBoneDic(nam);
        //print(dicName[GameManager.Singelton.currentLang]);
    }

    public void SetDicName(Dictionary<Lang, string> d) {
        dicName = d;
        //gameObject.name = dicName[Lang.lat];
    }

    private void langUpdate()
    {
        if (dicName!=null)
        {
            setName(dicName[GameManager.Singelton.currentLang]);
        }
    }

}



