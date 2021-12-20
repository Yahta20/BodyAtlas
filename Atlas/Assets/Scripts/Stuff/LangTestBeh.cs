using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class LangTestBeh : MonoBehaviour
{
    public Image Symbol;
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
                updateFoo();
            })
            .AddTo(this);

        Flag.OnPointerDownAsObservable().
        Subscribe(s => {
            switch (GameManager.Singelton.currentLang)
            {
                case (Lang.lat):
                    GameManager.Singelton.setLanguage(1);
                            //typolang.text = "Українська";
                            //nlang.text = "Українська Мова";
                    break;
                case (Lang.ua):
                    GameManager.Singelton.setLanguage(2);
                            //typolang.text = "Русский";
                            //nlang.text = "Русский Язык";
                    break;
                case (Lang.ru):
                    GameManager.Singelton.setLanguage(3);
                            //typolang.text = "English";
                            //nlang.text = "English Language";
                    break;
                case (Lang.en):
                    GameManager.Singelton.setLanguage(0);
                            //typolang.text = "Latina";
                            //nlang.text = "Lingua Latina";
                    break;
            }
        }).
        AddTo(this);

    }

    private void updateFoo()
    {
        var rtPanel = UIManager.Instance.CanvasSize();

        var rtlang = Symbol.rectTransform;
        rtlang.sizeDelta = new Vector2(rtPanel.y * 0.10f, rtPanel.y * 0.10f);

        var rtflag = Flag.rectTransform;
        rtflag.sizeDelta = new Vector2(rtlang.sizeDelta.y * 0.91f, rtlang.sizeDelta.y * 0.55f);
        rtlang.anchoredPosition = new Vector2(-(rtlang.sizeDelta.x- rtflag.sizeDelta.x)/2, 0);
        rtflag.anchoredPosition = new Vector2((rtlang.sizeDelta.x - rtflag.sizeDelta.x) / 2, -(rtlang.sizeDelta.y - rtflag.sizeDelta.y) / 2);
    
        
    }

    void Update()
    {
        
    }
}
