using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Control : MonoBehaviour
{
    public static Control Instance { get; private set; }
    public GameObject Preparat;
    public GameObject Postparat { get; private set; }
    public Material trasperent;
    [SerializeField]
    List<MeshRenderer> bones = new List<MeshRenderer>();
    public event Action<GameObject> OnChangePoint;
    // public CinemachineFreeLook camera;

    private void Awake()
    {
        Instance = this;
        Postparat = Preparat;
    }

    void Start()
    {
        MeshListUpdate();
    }


    private void MeshListUpdate()
    {
        var list = ObjectTree(Preparat);
        var rend = new MeshRenderer();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].TryGetComponent<MeshRenderer>(out rend))
            {
                bones.Add(rend);


                //var e = new Regex()
                if (
                    !(list[i].gameObject.name.StartsWith("R_")|
                    list[i].gameObject.name.StartsWith("L_"))
                    ) {
                    //print(list[i].gameObject.name);
                }
            }
        }

        //print(list.Count);
        //print(bones.Count);
    }

    public void ChangePoint(string name) {

        for (int i = 0; i < Postparat.transform.childCount; i++)
        {
            if (Postparat.transform.GetChild(i).gameObject.name == name &&
                Postparat.transform.GetChild(i).childCount != 0
                )
            {
                Postparat = Postparat.transform.GetChild(i).gameObject;
                //camera.LookAt = Postparat.transform;
            }
        }
        OnChangePoint?.Invoke(Postparat);
    }

    public void UpperHierarchy() {
        if (Postparat == Preparat) return;
        else
        {
            Postparat = Postparat.transform.parent.gameObject;

        }
        OnChangePoint?.Invoke(Postparat);
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

    public void CloseAllObjects() {
        List<GameObject> objects = new List<GameObject>();



    }

    List<GameObject> ObjectTree(GameObject root){
        List<GameObject> objects = new List<GameObject>();
        //objects.Add(root);
        var got = root.transform;
        for (int i = 0; i < got.childCount; i++)
        {
            objects.Add(got.GetChild(i).gameObject);

            if (got.GetChild(i).childCount!=0)
            {
                objects.AddRange(
                    ObjectTree(got.GetChild(i).gameObject)
                    );
            }

        }

        return objects; 
    }




}





