using UnityEngine;
using UnityEngine.UI;
using UniRx;

[RequireComponent(typeof(HorizontalLayoutGroup))]

public class DesigionBeh : MonoBehaviour
{

    public HorizontalLayoutGroup HLP;

    [Space]
    public RectTransform rtMain;
    public RectTransform rtNumber;
    public RectTransform rtWrong;
    public RectTransform rtRght;

    [Space]
    public Text tNumber;
    public Text tWrong;
    public Text tRight;


    public Image backgroundC;
    public int numberInRaw;
    public string sWrong;
    public string sRight;



    private void Awake()
    {
        if (HLP==null) { 
            HLP = GetComponent<HorizontalLayoutGroup>();
        }
    }

    void Start()
    {
        
    }

    void UIUpdate(Vector2 size,Vector2 delta) {

        rtMain.sizeDelta    = new Vector2(size.x-2*delta.x,size.y*0.2f);

        HLP.padding.left = (int) (size.y * 0.08f);
        HLP.padding.top  = (int) (size.y * 0.08f);
        HLP.spacing      = size.y * 0.08f;

        var dl = HLP.padding.left*2 + HLP.spacing*2; 
        rtNumber.sizeDelta  = new Vector2(rtMain.sizeDelta.y * 0.84f,rtMain.sizeDelta.y *0.84f);

        var xl = rtMain.sizeDelta.x-(rtNumber.sizeDelta.x + dl);

        rtWrong.sizeDelta   = new Vector2(xl/2,      rtMain.sizeDelta.y *0.84f);
        rtRght.sizeDelta    = new Vector2(xl/2,      rtMain.sizeDelta.y *0.84f);

    }

    void UITextUpdate()
    {
        tNumber.text = numberInRaw.ToString();
        tWrong.text = sWrong;
        tRight.text = sRight;
    }

    public void setDesigion(string[] args) {
        tNumber.text=args[0];
        tWrong .text=args[1];
        tRight .text=args[2];
        
        if (args[1] == args[2])
        {
            backgroundC.color = Color.green;
        }
        else { 
            backgroundC.color = Color.red;
        }

    }
}
