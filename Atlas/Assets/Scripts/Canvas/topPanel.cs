using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class topPanel : MonoBehaviour
{
    public static topPanel Instance;
    
    public RectTransform TopPanel;
    public Image image;
    private bool state;
    public float SpeedOfScrole;
    private Vector2 screenSize;

    void Awake() {

        Instance = this;
        TopPanel = GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height);
        state = true;
        SpeedOfScrole = SpeedOfScrole == 0 ? 0.1f : SpeedOfScrole;
    }

    // Start is called before the first frame update
    void Start()
    {

        image.OnPointerDownAsObservable().
            Subscribe(s=> {
                state = state == false ? true : false;
            }).
            AddTo(this);
        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                //updateFoo();
                if (state)
                {
                    var y = Mathf.Lerp(TopPanel.anchoredPosition.y, 0, SpeedOfScrole);
                    TopPanel.anchoredPosition = new Vector2(TopPanel.anchoredPosition.x, y);
                }
                else {
                    var y = Mathf.Lerp(TopPanel.anchoredPosition.y, TopPanel.sizeDelta.y, SpeedOfScrole);
                    TopPanel.anchoredPosition = new Vector2(TopPanel.anchoredPosition.x, y);
                }
            })
            .AddTo(this);

    }
}
