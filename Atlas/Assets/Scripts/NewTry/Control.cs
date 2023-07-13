using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public static Control Instance { get; private set; }
    public GameObject Preparat;
    public GameObject Postparat { get; private set; }

    public event Action OnChangePoint;


    private void Awake()
    {
        Instance = this;
        Postparat = Preparat;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ChangePoint(string name) {
        
        for (int i = 0; i < Postparat.transform.childCount; i++)
        {
            if (Postparat.transform.GetChild(i).gameObject.name==name&&
                Postparat.transform.GetChild(i).childCount!=0
                )
            {
                Postparat = Postparat.transform.GetChild(i).gameObject;
            }
        }
        OnChangePoint?.Invoke();
    }

    public void UpperHierarchy() {
        if (Postparat == Preparat) return;
        else
        {
            Postparat = Postparat.transform.parent.gameObject;
        }
        OnChangePoint?.Invoke();
    }
    public string[] getContent() {
        List<string> content = new List<string>();
        for (int i = 0; i < Postparat.transform.childCount; i++) { 
            content.Add(
            Postparat.transform.GetChild(i).gameObject.name
                );
        }
        return content.ToArray();
    }


    public string getObjName() { 
        return Postparat.transform.name;
        
    }

}





