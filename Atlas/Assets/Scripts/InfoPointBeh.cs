using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class InfoPointBeh : MonoBehaviour
{
    public Text UItext;
    private RectTransform rtPanel;
    public Transform pointToShow;
    private LineRenderer lr;
    public int index { get; set; }
    public SideOfScreen sos;
    public float scaleSize;

    void Awake()
    {
        rtPanel = GetComponent<RectTransform>();
        lr = GetComponent<LineRenderer>();
    }
        //rtPanel.sizeDelta = new Vector2(size.y * 0.1f, size.y * 0.1f);
        //rtPanel.anchoredPosition = new Vector2(0, 0);
        //print(rtPanel.sizeDelta);
    // Start is called before the first frame update


    void Start()
    {
        var screan = UIManager.Instance.screenSize.
            Where(w => w != Vector2.zero).
            //DistinctUntilChanged().
            Subscribe(s => {
                updateFoo(s);}).
            AddTo(this);
    }


    public void updateFoo(Vector2 size)  
    {
        var BooblertPanel = boobleMaker.Instance.rtPanel;
        var ScrollerPanel = rightPanel.Instance.rtPanel;


        var lowest = BooblertPanel.sizeDelta.x > BooblertPanel.sizeDelta.y ? BooblertPanel.sizeDelta.y : BooblertPanel.sizeDelta.x;
        setSize(new Vector2(lowest * scaleSize, lowest * scaleSize));
        setSetAncors(new Vector2(0, 0));
        var indexY = BooblertPanel.sizeDelta.y/(rtPanel.sizeDelta.x+ rtPanel.sizeDelta.x*scaleSize)-1;
        var indexX = BooblertPanel.sizeDelta.x/(rtPanel.sizeDelta.x+ rtPanel.sizeDelta.x*scaleSize)-1;

        var indexI = 0;
        var posx = (index+1) / indexX;
        var posy = (index+1) / indexY;
        var offset = (rtPanel.sizeDelta.x + rtPanel.sizeDelta.x * scaleSize);


        lr.positionCount = 2;

        //
        //

        float xposRT = 0;
        float yposRT = 0;

        switch (sos)
        {
            case SideOfScreen.top:
                indexI = posx >= 1 ? index - (int)indexX : index;
                setSetAncors(new Vector2( ((int)posx * offset) + offset * indexI, -(int)posx *offset));
                xposRT += rtPanel.sizeDelta.x/2;
                yposRT -= rtPanel.sizeDelta.y/2;

                break;
            case SideOfScreen.right:
                indexI = posy >= 1 ? index - (int)indexY : index;
                setSetAncors(new Vector2(
                    -(int)posy * offset,
                            -(((int)posy * offset) + offset * indexI)));
                var ymin = (size.y - ScrollerPanel.sizeDelta.y);
                var xmin = ScrollerPanel.sizeDelta.x;


                xposRT += rtPanel.sizeDelta.x / 2+ (ScrollerPanel.anchoredPosition.x - ScrollerPanel.sizeDelta.x);
                yposRT -= rtPanel.sizeDelta.y / 2;
                var str = $"{gameObject.name}:SP.AP:{ScrollerPanel.anchoredPosition- ScrollerPanel.sizeDelta}||x:{xposRT}||y:{yposRT}";
                print(str);
                setSetAncors(new Vector2(rtPanel.anchoredPosition.x, rtPanel.anchoredPosition.y));

                break;

            case SideOfScreen.bottop:
                indexI = posx >= 1 ? index - (int)indexX : index;
                setSetAncors(new Vector2(-(((int)posx * offset) + offset * indexI), (int)posx * offset));

                xposRT += rtPanel.sizeDelta.x / 2 + (ScrollerPanel.anchoredPosition.x  -  ScrollerPanel.sizeDelta.x);
                yposRT += rtPanel.sizeDelta.y / 2;
                
                

                break;
            case SideOfScreen.left:
                indexI = posy >= 1 ? index - (int)indexY : index;
                setSetAncors(new Vector2(
                    (int)posy * offset,
                            (((int)posy * offset) + offset * indexI))
                    );
                xposRT += rtPanel.sizeDelta.x / 2;
                yposRT += rtPanel.sizeDelta.y / 2;
                
                
                break;
            default:
                break;
        }
        //var str = $"index: {index}\t indexX: {indexX}\t indexY: {indexY}\n" +
        //          $"pos : {posx} \toffset: {offset}\t indexI: {indexI}\t rtPanel.sizeDelta.x: {rtPanel.sizeDelta.x}\n";
        //0, -(rtPanel.sizeDelta.x + rtPanel.sizeDelta.x * scaleSize) * index)
        //var str = $"xmin: {xmin}\t ymin: {ymin}";
        //print(str);
        //setSetAncors(new Vector2(-(rtPanel.sizeDelta.x + rtPanel.sizeDelta.x * scaleSize) * index, 0));
        //setSetAncors(new Vector2(0, (rtPanel.sizeDelta.x + rtPanel.sizeDelta.x * scaleSize) * index));
        //Debug.DrawLine(start, point, Color.green);
        xposRT += (rtPanel.anchorMax.x * size.x + rtPanel.anchorMin.x * size.x) / 2 + rtPanel.anchoredPosition.x;
        yposRT += (rtPanel.anchorMax.y * size.y + rtPanel.anchorMin.y * size.y) / 2 + rtPanel.anchoredPosition.y;


        Vector3 start = SphereBeh.Instance.camera.ScreenToWorldPoint(new Vector3(xposRT, yposRT, SphereBeh.Instance.camera.nearClipPlane));
        Vector3 point = Vector3.zero;




        try
        {
            ///////poisk po tochkam inpruv || 130 i 1337 linii oshibka nuzhno peredelaty vse norm rabotaet
            ///////
            if (
                //GameObject.Find("/Classroom/" + gameObject.name).transform != null && 
                ClassroomBeh.Instance.chosenObj.name == "empty") { 
                 point = GameObject.Find("/Classroom/"+gameObject.name).transform.position;
                //var str = $"{gameObject.name}-{point}";
                //print(str);
            }
            if (
                //GameObject.Find("/Classroom/" + gameObject.name).transform != null && 
                ClassroomBeh.Instance.chosenObj.name != "empty")
            {
                point = GameObject.Find("/Classroom/"+ ClassroomBeh.Instance.chosenObj.name+"/"+ gameObject.name).transform.TransformPoint(Vector3.zero);
            //    var str = $"{gameObject.name}-{point}";
            //    print(str);
                 
            }
        }
        catch (System.Exception e)
        {
            //print("йой"+e.ToString());
        }


            

        lr.SetPosition(1, start);
        lr.SetPosition(0, point);
        lr.startWidth = 0.001f;
        lr.endWidth = 0.001f;
        var txt = UItext.rectTransform;
        txt.sizeDelta = new Vector2(rtPanel.sizeDelta.x * 0.8f, rtPanel.sizeDelta.x * 0.8f);
        txt.anchoredPosition = new Vector2(0, 0);
    }

    public void setSize(Vector2 size){
        rtPanel.sizeDelta = size;
    }

    public void setSetAncors(Vector2 min, Vector2 max) {
        rtPanel.anchorMin = min;
        rtPanel.anchorMax = max;
    }

    public void setPivot(Vector2 pivot) {
        rtPanel.pivot = pivot;
    }

    public void setSetAncors(Vector2 pivot){
        rtPanel.anchoredPosition = pivot;
    }

    public void setSizeDelta(Vector2 pivot) {
        rtPanel.sizeDelta = pivot;
    }
}
