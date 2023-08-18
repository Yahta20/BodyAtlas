using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class scrollBeh : MonoBehaviour
{

    public GameObject prefab;
    ScrollRect scroll;
    public Text Label;

    // Start is called before the first frame update

    private void Awake()
    {
        scroll = GetComponent<ScrollRect>();
    }
        
    void Start()
    {
        UpdateContent(Control.Instance.Postparat);

        Control.Instance.OnChangePoint += UpdateContent;
        ContentLoc.Instance.OnChangeLang += UpdateLang;
    }

    private void UpdateLang()
    {
        UpdateContent(Control.Instance.Postparat);
    }

    private void UpdateContent(GameObject @object)
    {
        for (int i = 0; i < scroll.content.transform.childCount; i++)
        {
            Destroy(scroll.content.transform.GetChild(i).gameObject);
        }
        scroll.content.sizeDelta = Vector2.zero;

        var content = Control.Instance.getContent();
        //print(content.Length);
        for (int i = 0; i < content.Length; i++)
        {
            var butun = Instantiate(prefab, scroll.content);
            butun.GetComponent<RectTransform>().sizeDelta
                = new Vector2(
                        butun.GetComponent<RectTransform>().sizeDelta.x,
                        CanvasBehavior.Instance.getSize().y * 0.15f);
            butun.name = content[i];
            butun.GetComponentInChildren<Text>().text =ContentLoc.Instance.GetLocalText(content[i]) ;//
            var a = content[i].ToString();

            butun.GetComponent<Button>().onClick.AddListener(() => {
                Control.Instance.ChangePoint(a);
            });
            

            //butun.GetComponent<Button>().On
            scroll.content.sizeDelta += new Vector2(0
                , CanvasBehavior.Instance.getSize().y * 0.15f + 25);
        }
        Label.text = ContentLoc.Instance.GetLocalText(@object.name);// ;

    }



    private void OnDisable()
    {
        Control.Instance.OnChangePoint -= UpdateContent;
    }



}
