using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class rightPanel : MonoBehaviour
{

    public static rightPanel Instance;
    public RectTransform TopPanel;
    public Image image;

    public float SpeedOfScrole;

    private bool state;
    private Vector2 screenSize;



    void Awake()
    {
        Instance = this;
        TopPanel = GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height);
        state = true;
        SpeedOfScrole = SpeedOfScrole == 0 ? 0.1f : SpeedOfScrole;
    }



    void Start()
    {
        image.OnPointerDownAsObservable().
            Subscribe(s => {
                state = state == false ? true : false;
            }).
            AddTo(this);
        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                //updateFoo();
                if (state)
                {
                    var x = Mathf.Lerp(TopPanel.anchoredPosition.x, 0, SpeedOfScrole);
                    TopPanel.anchoredPosition = new Vector2(x, TopPanel.anchoredPosition.y);
                }
                else
                {
                    var x = Mathf.Lerp(TopPanel.anchoredPosition.x, TopPanel.sizeDelta.x, SpeedOfScrole);
                    TopPanel.anchoredPosition = new Vector2(x, TopPanel.anchoredPosition.y);
                }
            })
            .AddTo(this);
                    

    }
}

