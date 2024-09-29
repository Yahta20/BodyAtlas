using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Control : MonoBehaviour
{
    public static Control Instance { get; private set; }
    public GameObject Indicator;
    public GameObject Preparat;
    public GameObject SubParat { get; private set; }
    [Space]
    public string nameOfFile;
    public Material trasperent;

    [SerializeField]
    List<Bone> bones = new List<Bone>();
    //List<MeshRenderer> bones = new List<MeshRenderer>();
    public event Action<GameObject> OnChangePoint;
    //public event Action<Transform> OnMarkPoint;
    // public CinemachineFreeLook camera;



    private void Awake()
    {
        Instance = this;
        SubParat = Preparat;
        HideIndicator();
        //VisibilityOfPreparat(false);
        MeshListUpdate();
    }
    public void HideIndicator()
    {
        Indicator.transform.position = new Vector3(1007, 1070, 1700);
        Indicator.SetActive(false);
    }
    private void MeshListUpdate()
    {
        var list = ObjectTree(Preparat);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].TryGetComponent<MeshRenderer>(out var rend))
            {
                var d = list[i].AddComponent<Bone>();
                d.Setmaterial(trasperent);
                bones.Add(
                    d);
            }
        }
    }
     
    public void VisibilityOfPreparat(bool v)
    {
        Preparat.SetActive(v);
    }
    public void HomePosition()
    {
        SubParat = Preparat;
        OnChangePoint?.Invoke(SubParat);
        HideIndicator();
    }

    public void ChangePoint(Bone obj)
    {
        SubParat = obj.gameObject;
        OnChangePoint?.Invoke(SubParat);
        HideIndicator();
    }

    public Bone[] GetBoneArray(int l) {
        var rand = new System.Random();
        List<Bone> list = new List<Bone>();
        do
        {
            var ansv = bones[rand.Next(0,bones.Count-1)];
            if (ansv.gameObject.name.StartsWith("R_") |
                ansv.gameObject.name.StartsWith("L_")
                )
            {
                if (!list.Exists(p => p == ansv) |
                    !list.Exists(p => p.name == $"{ansv.gameObject.name.Substring(2)}\n"))
                {
                    list.Add(ansv);
                }
            }
        } while (list.Count < l);
        return list.ToArray();
    }

    public Bone[] GetAddBoneArray(Bone obj,int l)
    {
        var rand = new System.Random();
        List<Bone> list = new List<Bone>();
        list.Add(obj);

        do
        {
            var ansv = bones[rand.Next(bones.Count-1)];
            if (ansv.gameObject.name.StartsWith("R_") |
                ansv.gameObject.name.StartsWith("L_")
                )
            {
                if (!list.Exists(p => p == ansv) |
                    !list.Exists(p => p.name == $"{ansv.gameObject.name.Substring(2)}\n"))
                {
                    list.Add(ansv);
                }
            }
        } while (list.Count < l);
        list.Sort();
        return list.ToArray();
    }

    public void ChangePoint(string name) {

        for (int i = 0; i < SubParat.transform.childCount; i++)
        {
            if (
                SubParat.transform.GetChild(i).gameObject.name == name 
                )
            {
                if (
                SubParat.transform.GetChild(i).childCount != 0 
                    )
                {
                    SubParat = SubParat.transform.GetChild(i).gameObject;
                    HideIndicator();
                    OnChangePoint?.Invoke(SubParat);
                }
                else
                {
                    Indicator.SetActive(true);
                    Indicator.transform.position = SubParat.transform.GetChild(i).position;
                }
                //  true
                    //print("sa");
                    //print($"ss{SubParat.transform.position}");
                    //OnMarkPoint?.Invoke(SubParat.transform);
                    

                //camera.LookAt = SubParat.transform;
            }
        }
    }

    public void UpperHierarchy() {
        if (SubParat == Preparat) return;
        else
        {
            SubParat = SubParat.transform.parent.gameObject;
        }
        HideIndicator();
        OnChangePoint?.Invoke(SubParat);
    }


    //creatin answering list
    public string[] getContent() {
        List<string> content = new List<string>();


        //need compare special case


        if (SubParat.TryGetComponent<Bone>(out var bon))
        {

        }

        if (SubParat.TryGetComponent<MeshRenderer>(out var mr)) {
            for (int i = 0; i < mr.materials.Length; i++)
            {
                var s = $"{mr.materials[i].name}";
                if (mr.materials[i].name != SubParat.name
                    & mr.materials[i].name.IndexOf('_')!=-1
                    )
                { 
                    content.Add(
                        mr.materials[i].name.Substring(
                              mr.materials[i].name.IndexOf('_') + 1,
                              mr.materials[i].name.IndexOf('(') - mr.materials[i].name.IndexOf('_')-2
                        )
                    );
                }
            }
        }

        for (int i = 0; i < SubParat.transform.childCount; i++) {
            content.Add(
            SubParat.transform.GetChild(i).gameObject.name
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






        /*
        var path = Path.Combine(Application.dataPath, nameOfFile);
        
        var alb = "";
        nameOfFile += ".txt";

        var idot = new List<string>();
         */

            /*
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
             */
        // print($"{list[i].gameObject.name.Substring(2)} \n");
        //   print($"{list[i].gameObject.name.Substring(2)} \n");
        //alb += $"{list[i].gameObject.name} \n" ;
        //idot.Sort();

        /*
        
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