using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResultPanelB: MonoBehaviour
{
    public GameObject prefab;
    [Space]
    public Text Qlabel;
    public ScrollRect AnsverPool;

    public Button retry;
    public Button lerning;
    Text txtretry;  
    Text txtlerning;

    RectTransform baseRect;

    private void Start()
    {

        baseRect = GetComponent<RectTransform>();
        PrintPanel(CanvasBehavior.Instance.getSize());
        CanvasBehavior.Instance.OnSizeChanged += PrintPanel;
        txtretry  =retry  .GetComponentInChildren<Text>();
        txtlerning = lerning.GetComponentInChildren<Text>();

    }

    public void PrintPanel(Vector2 screen)
    {
        baseRect.sizeDelta = screen * 0.78f;
        baseRect.anchoredPosition = Vector2.zero;
        Qlabel.rectTransform.sizeDelta = new Vector2(baseRect.sizeDelta.x, baseRect.sizeDelta.y * 0.10f);
        AnsverPool.GetComponent<RectTransform>().sizeDelta = new Vector2(baseRect.sizeDelta.x*0.75f, baseRect.sizeDelta.y * 0.85f);
        retry      .GetComponent<RectTransform>().sizeDelta = new Vector2 (baseRect.sizeDelta.x * 0.125f, baseRect.sizeDelta.y * 0.15f);
        lerning.GetComponent<RectTransform>().sizeDelta     = new Vector2 (baseRect.sizeDelta.x * 0.125f, baseRect.sizeDelta.y * 0.15f);
    }



    public void CreateResoult(string[] car, string[] uar) {
        for (int i = 0; i < AnsverPool.content.transform.childCount; i++)
        {
            Destroy(AnsverPool.content.transform.GetChild(i).gameObject);
        }
        AnsverPool.content.sizeDelta = Vector2.zero;
        int ra = 0;
        
        for (int i = 0; i < car.Length; i++)
        {
            var butun = Instantiate(prefab, AnsverPool.content);
            butun.GetComponent<RectTransform>().sizeDelta
                = new Vector2(
                        AnsverPool.GetComponent<RectTransform>().sizeDelta.x,
                        CanvasBehavior.Instance.getSize().y * 0.078f);

            butun.name = car[i];
            butun.GetComponentInChildren<Text>().text = $"{ContentLoc.Instance.GetLocalText(car[i])}/{ContentLoc.Instance.GetLocalText(uar[i])}";
            if (car[i] == uar[i])
            {
                butun.GetComponent<Image>().color = Color.green;
                ra++;
            }
            else {
                butun.GetComponent<Image>().color = Color.red;

            }
            AnsverPool.content.sizeDelta += new Vector2(
                0,
                butun.GetComponent<RectTransform>().sizeDelta.y+20
                );
        }
        Qlabel.text = $"{ContentLoc.Instance.GetLocalText("proventus")} {ra}/{car.Length}";//  ContentLoc.Instance.GetLocalText("Quod os hoc est?");// ;
    }
            //print($"{car[i] == uar[i]}= {car[i] }/{uar[i]}");





            //if (car[i].Equals(uar[i])) butun.GetComponent<Image>().color = Color.green;
            //else
            //
            //var a = l2v[i].name.ToString();
            //var str = car[i].name;
            //butun.GetComponent<Button>().onClick.AddListener(() => {
            //    fAudi.MarkAnsver(str);
            //});



    public void SetIplementing(UnityAction rtr, UnityAction lern) {
        retry.onClick.RemoveAllListeners();
        lerning.onClick.RemoveAllListeners();
        retry.onClick.AddListener(rtr);
        lerning.onClick.AddListener(lern);
    }

    public void FillText(string[] args)
    {
        retry.GetComponentInChildren<Text>().text   = args[0];
        lerning.GetComponentInChildren<Text>().text = args[1];
        

        print($"{retry.GetComponentInChildren<Text>()   .text}");
        print($"{lerning.GetComponentInChildren<Text>() .text}");
    }

}