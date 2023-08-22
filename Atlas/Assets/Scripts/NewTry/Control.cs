using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Control : MonoBehaviour
{
    public static Control Instance { get; private set; }
    public GameObject Indicator;
    public GameObject Preparat;
    public GameObject Postparat { get; private set; }
    [Space]
    public string nameOfFile;
    public Material trasperent;
    
    [SerializeField]
    public List<Bone> bones = new List<Bone>();    
    //List<MeshRenderer> bones = new List<MeshRenderer>();
    public event Action<GameObject> OnChangePoint;
    //public event Action<Transform> OnMarkPoint;
    // public CinemachineFreeLook camera;



    private void Awake()
    {
        Instance = this;
        Postparat = Preparat;
        HideIndicator();
    }
    private void HideIndicator()
    {
        Indicator.transform.position = new Vector3(1007, 1070, 1700);
    }
    void Start()
    {
        MeshListUpdate();
    }


    private void MeshListUpdate()
    {
        var list = ObjectTree(Preparat);
        
        var path = Path.Combine(Application.dataPath, nameOfFile);
        
        var alb = "";
        nameOfFile += ".txt";

        var idot = new List<string>();
        
        var rend = new MeshRenderer();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].TryGetComponent<MeshRenderer>(out rend))
            {
                var d = list[i].AddComponent<Bone>();
                d.Setmaterial(trasperent);
                bones.Add(
                    d);
            }


            if (
                (list[i].gameObject.name.StartsWith("R_") |
                list[i].gameObject.name.StartsWith("L_"))
                )
            {
                if (!idot.Exists(p =>p== $"{list[i].gameObject.name.Substring(2)}\n")) {
                    idot.Add($"{list[i].gameObject.name.Substring(2)}\n");
                }
            }
            else {
                if (!idot.Exists(p => p == $"{list[i].gameObject.name}\n"))
                {
                    idot.Add($"{list[i].gameObject.name}\n");
                }
                //alb += $"{list[i].gameObject.name} \n";
            }
            /*
             */
                    
                   // print($"{list[i].gameObject.name.Substring(2)} \n");
                 //   print($"{list[i].gameObject.name.Substring(2)} \n");
                //alb += $"{list[i].gameObject.name} \n" ;

        }

        //idot.Sort();
         
        /*
        for (int i = 0; i < idot.Count; i++)
        {
            alb += idot[i];
        }

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream fileStream = new FileStream(path,
                                       FileMode.OpenOrCreate,
                                       FileAccess.ReadWrite,
                                       FileShare.None);
            if (fileStream.CanWrite)
            {
                byte[] arr = System.Text.Encoding.Default.GetBytes(alb);
                fileStream.Write(arr, 0, arr.Length);
            }
            fileStream.Close();
            print("fin");
        }
        catch (System.Exception e)
        {
            print($"Pizda togo sho {e.ToString()}");
        }
         */
    }



    public void ChangePoint(Bone obj)
    {
        Postparat=obj.gameObject;
        OnChangePoint?.Invoke(Postparat);
        HideIndicator();
    }



    public void ChangePoint(string name) {

        for (int i = 0; i < Postparat.transform.childCount; i++)
        {
            if (
                Postparat.transform.GetChild(i).gameObject.name == name 
                )
            {
                if (
                Postparat.transform.GetChild(i).childCount != 0 
                    )
                {
                    Postparat = Postparat.transform.GetChild(i).gameObject;
                    HideIndicator();
                    OnChangePoint?.Invoke(Postparat);
                }
                else
                {
                    Indicator.transform.position = Postparat.transform.GetChild(i).position;
                }
                //  true
                    //print("sa");
                    //print($"ss{Postparat.transform.position}");
                    //OnMarkPoint?.Invoke(Postparat.transform);
                    

                //camera.LookAt = Postparat.transform;
            }
        }
    }

    public void UpperHierarchy() {
        if (Postparat == Preparat) return;
        else
        {
            Postparat = Postparat.transform.parent.gameObject;
        }
        HideIndicator();
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





