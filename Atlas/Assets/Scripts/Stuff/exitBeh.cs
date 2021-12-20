using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class exitBeh : MonoBehaviour
{

    public Image Symbol;

    
    
    void Start()
    {
        Symbol = GetComponent<Image>();
        var img = Observable.EveryFixedUpdate()
           .Subscribe(
           s => {
               updateFoo();
           })
           .AddTo(this);

        Symbol.OnPointerDownAsObservable().
            Subscribe(s => {
                GameManager.Instance.resetData();
                SceneManager.LoadScene(0);
            }).
            AddTo(this);

    }

    private void updateFoo()
    {
        var rtPanel = UIManager.Instance.CanvasSize();
        var rtlang = Symbol.rectTransform;
        rtlang.sizeDelta = new Vector2(rtPanel.y * 0.10f, rtPanel.y * 0.10f);
        rtlang.anchoredPosition = new Vector2(0, 0);
    }


}
