using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class bubblePointTest : MonoBehaviour
{

    public Text UItext;
    private RectTransform rtPanel;
    public Transform pointToShow;
    private LineRenderer lr;
    private Image Knob;
    public RectTransform sp;
    public Camera camera; 

    void Awake()
    {
        rtPanel = GetComponent<RectTransform>();
        lr = GetComponent<LineRenderer>();
        Knob = GetComponent<Image>();
        // UItext = GetComponent<Text>(); 
        //Activation(false);
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
            .Subscribe(s => {
                updateMove(s.delta);
            })
            .AddTo(this);
    }

    public void updateMove(Vector2 size)
    {
        rtPanel.anchoredPosition += size;
    }

    public void updateFoo(Vector2 size)
    {
        //var sp = rightPanel.Instance.rtPanel;
        var x = (rtPanel.anchoredPosition.x + rtPanel.sizeDelta.x) >
                (UIManager.Instance.CanvasSize().x - sp.sizeDelta.x + sp.anchoredPosition.x) ?
                UIManager.Instance.CanvasSize().x - sp.sizeDelta.x + sp.anchoredPosition.x - rtPanel.sizeDelta.x :
                rtPanel.anchoredPosition.x;
        var y = (rtPanel.anchoredPosition.y + rtPanel.sizeDelta.y) >
                (UIManager.Instance.CanvasSize().y) ?
                (UIManager.Instance.CanvasSize().y-rtPanel.sizeDelta.y) :
                rtPanel.anchoredPosition.y;

        x = x < 0 ? 0 : x;
        y = y < 0 ? 0 : y;

        rtPanel.anchoredPosition = new Vector2(x, y);
        lr.positionCount = 2;

        float xposRT = x + rtPanel.sizeDelta.x / 2;
        float yposRT = y + rtPanel.sizeDelta.y / 2;// + UIManager.Instance.CanvasSize().y;
        
        Vector3 start = camera.ScreenToWorldPoint(
            new Vector3(xposRT, yposRT, camera.nearClipPlane));
       // Vector3 point = camera.ScreenToWorldPoint(TestManager.Instance.getBonepoint());
        Vector3 point = TestManager.Instance.getBonepoint();
        // Vector3 point = ClassroomBeh.Instance.chosenObj.
        //     GetComponent<BoneBih>().getPoint(GameManager.Instance.getState()[3]);//Vector3.zero;

        lr.endWidth = 0.0025f;
        lr.startWidth = 0.003f;
        lr.SetPosition(1, start);
        lr.SetPosition(0, point);

        //UItext.text = (
        //rightPanel.Instance.getPointNumber(GameManager.Instance.getState()[3]) + 1).ToString();
        /*
         */
        //print($"s = {s}");
    }

    public void Activation(bool a) {
        gameObject.SetActive(a);
    }
}
