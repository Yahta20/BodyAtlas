using UnityEngine;
using UnityEngine.UI;

public class ResultPanelB: MonoBehaviour
{
    public GameObject prefab;
    [Space]
    public Text Qlabel;
    public ScrollRect AnsverPool;
    RectTransform baseRect;

    private void Start()
    {
        baseRect = GetComponent<RectTransform>();
        PrintPanel(CanvasBehavior.Instance.getSize());
        CanvasBehavior.Instance.OnSizeChanged += PrintPanel;
    }

    public void PrintPanel(Vector2 screen)
    {
        baseRect.sizeDelta = screen * 0.78f;
        baseRect.anchoredPosition = Vector2.zero;
        Qlabel.rectTransform.sizeDelta = new Vector2(baseRect.sizeDelta.x, baseRect.sizeDelta.y * 0.10f);
        AnsverPool.GetComponent<RectTransform>().sizeDelta = new Vector2(baseRect.sizeDelta.x*0.75f, baseRect.sizeDelta.y * 0.85f);
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
            //print($"{car[i] == uar[i]}= {car[i] }/{uar[i]}");

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

            //if (car[i].Equals(uar[i])) butun.GetComponent<Image>().color = Color.green;
            //else
            //
            //var a = l2v[i].name.ToString();
            //var str = car[i].name;
            //butun.GetComponent<Button>().onClick.AddListener(() => {
            //    fAudi.MarkAnsver(str);
            //});
        }
        Qlabel.text = $"{ContentLoc.Instance.GetLocalText("Quod os hoc est?")} {ra}/{car.Length}";//  ContentLoc.Instance.GetLocalText("Quod os hoc est?");// ;


    }


}