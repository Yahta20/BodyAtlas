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


        Label.text = ContentLoc.Instance.GetLocalText(@object.name);// ;
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
            scroll.content.sizeDelta += new Vector2(0
                , CanvasBehavior.Instance.getSize().y * 0.15f + 25);
        }

    }
    public void UpdateTesting() {

        List<Bone> l2v = new List<Bone>();  
        var a =Control.Instance.bones[
            UnityEngine.Random.Range(0, Control.Instance.bones.Count)];
        Control.Instance.ChangePoint(
        a    
            );
        l2v.Add(a);
        while (l2v.Count <6) {
            var t = Control.Instance.bones[
            UnityEngine.Random.Range(0, Control.Instance.bones.Count)];
            if (
                !l2v.Contains(t)
                )
            {
                l2v.Add(t);
            }

        }
        l2v.Sort();



        //Random.Range(0, Control.Instance.bones.Count)
        Label.text = ContentLoc.Instance.GetLocalText("Quod os hoc est?");// ;
        for (int i = 0; i < scroll.content.transform.childCount; i++)
        {
            Destroy(scroll.content.transform.GetChild(i).gameObject);
        }
        scroll.content.sizeDelta = Vector2.zero;
       
        for (int i = 0; i < l2v.Count; i++) {
            var butun = Instantiate(prefab, scroll.content);
            butun.GetComponent<RectTransform>().sizeDelta
                = new Vector2(
                        butun.GetComponent<RectTransform>().sizeDelta.x,
                        CanvasBehavior.Instance.getSize().y * 0.15f);

            butun.name = l2v[i].name;
            butun.GetComponentInChildren<Text>().text = ContentLoc.Instance.GetLocalText(l2v[i].name);//
            //var a = l2v[i].name.ToString();

            butun.GetComponent<Button>().onClick.AddListener(() => {
                //Control.Instance.ChangePoint(a);
            });

        }
    }
    private void OnDisable()
    {
        Control.Instance.OnChangePoint -= UpdateContent;
    }
    


}
