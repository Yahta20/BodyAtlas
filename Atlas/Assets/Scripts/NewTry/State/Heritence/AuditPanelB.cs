using System;
using UnityEngine;
using UnityEngine.UI;


public class AuditPanelB : MonoBehaviour
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
        baseRect.sizeDelta = new Vector2(screen.x*0.25f, screen.y);
        baseRect.anchoredPosition = Vector2.zero;
        Qlabel.rectTransform.sizeDelta =  new Vector2(baseRect.sizeDelta.x, baseRect.sizeDelta.y * 0.10f);
        AnsverPool.GetComponent<RectTransform>().sizeDelta = new Vector2(baseRect.sizeDelta.x, baseRect.sizeDelta.y * 0.90f);
    }

    public void CreateStep(Bone b, FAudit fAudi) {


        var a = Control.Instance.GetAddBoneArray(b, 5);
        Qlabel.text = ContentLoc.Instance.GetLocalText("Quod os hoc est?");// ;
        for (int i = 0; i < AnsverPool.content.transform.childCount; i++)
        {
            Destroy(AnsverPool.content.transform.GetChild(i).gameObject);
        }
        AnsverPool.content.sizeDelta = Vector2.zero;

        for (int i = 0; i < a.Length; i++)
        {
            var butun = Instantiate(prefab, AnsverPool.content);
            butun.GetComponent<RectTransform>().sizeDelta
                = new Vector2(
                        butun.GetComponent<RectTransform>().sizeDelta.x,
                        CanvasBehavior.Instance.getSize().y * 0.15f);

            butun.name = a[i].name;
            butun.GetComponentInChildren<Text>().text = ContentLoc.Instance.GetLocalText(a[i].name);//
            //var a = l2v[i].name.ToString();
            //var str = a[i].name;
            
            var str = a[i].name.StartsWith("R_") |
                a[i].name.StartsWith("L_")? a[i].name.Substring(2) : a[i].name;
            butun.GetComponent<Button>().onClick.AddListener(() => {
                fAudi.MarkAnsver(str);
            });
        }

    }

}



