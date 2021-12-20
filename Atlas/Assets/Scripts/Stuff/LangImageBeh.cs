using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;
using UniRx.Triggers;

public class LangImageBeh : MonoBehaviour
{
    public Image Flag;
    public Sprite ruFlag;
    public Sprite uaFlag;
    public Sprite latFlag;
    public Sprite engFlag;




    private void Awake()
    {
        GameManager.Singelton.setLanguage(0);
    }




    void Start()
    {
        var geLang = GameManager.Instance.currentLang;
        var img = Observable.EveryFixedUpdate()
            .Subscribe(
            s => {
                switch (GameManager.Singelton.getLanguage())
                {
                    case (0):
                        Flag.sprite = latFlag;
                        break;
                    case (1):
                        Flag.sprite = uaFlag;
                        break;
                    case (2):
                        Flag.sprite = ruFlag;
                        break;
                    case (3):
                        Flag.sprite = engFlag;
                        break;
                    default:
                        Flag.sprite = latFlag;
                        break;
                }
            })
            .AddTo(this);
    }


    
}
