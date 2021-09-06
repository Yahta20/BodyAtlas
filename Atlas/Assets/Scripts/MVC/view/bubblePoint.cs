using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;


public class bubblePoint : MonoBehaviour
{

    public Text UItext;
    private RectTransform rtPanel;
    public Transform pointToShow;
    private LineRenderer lr;
    private Image Knob;


    void Awake()
    {
        rtPanel = GetComponent<RectTransform>();
        lr = GetComponent<LineRenderer>();
        Knob = GetComponent<Image>();
       // UItext = GetComponent<Text>(); 
    }

    void Start()
    {
        var screan = UIManager.Instance.screenSize.
            Where(w => w != Vector2.zero).
            Subscribe(s => {
                updateFoo(s);
            }).
            AddTo(this);

        Knob.OnDragAsObservable()
            .Subscribe(s=> {
                updateMove(s.delta);
            })
            .AddTo(this);
    }

    public void updateMove(Vector2 size) {
        rtPanel.anchoredPosition += size;
    }
        
    public void updateFoo(Vector2 size)
    {
        if (GameManager.Instance.getState()[3]=="")
        {
            gameObject.SetActive(false);
        }
        if (GameManager.Instance.getState()[3] != "")
        {
            gameObject.SetActive(true);

        var sp = rightPanel.Instance.rtPanel;
        var x = (rtPanel.anchoredPosition.x + rtPanel.sizeDelta.x) >
                (UIManager.Instance.CanvasSize().x - sp.sizeDelta.x + sp.anchoredPosition.x) ?
                UIManager.Instance.CanvasSize().x - sp.sizeDelta.x + sp.anchoredPosition.x - rtPanel.sizeDelta.x :
                rtPanel.anchoredPosition.x;
        var y = -(rtPanel.anchoredPosition.y - rtPanel.sizeDelta.y) >
                (UIManager.Instance.CanvasSize().y) ?
                (rtPanel.sizeDelta.y - UIManager.Instance.CanvasSize().y) :
                rtPanel.anchoredPosition.y;
        
        x = x < 0 ? 0 : x;
        y = y > 0 ? 0 : y;
        
            rtPanel.anchoredPosition = new Vector2(x, y);
        lr.positionCount = 2;
        
            float xposRT = x+rtPanel.sizeDelta.x/2;
        float yposRT = y - rtPanel.sizeDelta.y/2+UIManager.Instance.CanvasSize().y;
        
        Vector3 start = SphereBeh.Instance.camera.ScreenToWorldPoint(
            new Vector3(xposRT, yposRT, SphereBeh.Instance.camera.nearClipPlane));

        Vector3 point = ClassroomBeh.Instance.chosenObj.
            GetComponent<BoneBih>().getPoint(GameManager.Instance.getState()[3]);//Vector3.zero;

        lr.endWidth = 0.002f;
        lr.startWidth = 0.003f;
        lr.SetPosition(1, start);
        lr.SetPosition(0, point);

            UItext.text =(
            rightPanel.Instance.getPointNumber(GameManager.Instance.getState()[3])+1).ToString();
            //print($"s = {s}");
        }
    }
}

