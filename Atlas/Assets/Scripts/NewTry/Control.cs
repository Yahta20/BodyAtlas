using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Control : MonoBehaviour
{
    public static Control Instance { get; private set; }
    public GameObject Preparat;
    public GameObject Postparat { get; private set; }
    [Space]
    public string nameOfFile;
    public Material trasperent;
    
    [SerializeField]
    List<Bone> bones = new List<Bone>();    
    //List<MeshRenderer> bones = new List<MeshRenderer>();
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
        var path = Path.Combine(Application.dataPath, nameOfFile);
        nameOfFile += ".txt";
        var alb = "";
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

        }
        /*
            if (
                (list[i].gameObject.name.StartsWith("R_") |
                list[i].gameObject.name.StartsWith("L_"))
                )
            {
                alb += $"{list[i].gameObject.name.Substring(2)} \n";

            }
            else {
                alb += $"{list[i].gameObject.name} \n" ;
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

<<<<<<< HEAD
=======
>>>>>>> parent of 7c93133 (Chek for Webgl build)
=======
<<<<<<< HEAD
>>>>>>> parent of 7c93133 (Chek for Webgl build)




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
         */

    }

    public void ChangePoint(Bone obj)
    {
        Postparat=obj.gameObject;
        OnChangePoint?.Invoke(Postparat);
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





