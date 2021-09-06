using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ClassroomBeh : MonoBehaviour
{
    public static ClassroomBeh Instance;

    public Material backgroundMat;
    public GameObject chosenObj;

    private GameObject emptyObj;
    [Space]
    public List<GameObject> chosenObjs;

    [Space]
    public List<GameObject> Partition = new List<GameObject>();

    public List<BoneBih> BoneOnScene;//{get; private set;}

    public IObservable <bool> isChosenObject    {get; private set;}
    public IObservable <string> nameOfChosen    {get; private set;}

    void Awake() {
        GameEnviroment.Singelton.setLanguage(0);
        Instance = this;
        emptyObj = new GameObject("empty");
        chosenObj = emptyObj;
        BoneOnScene = new List<BoneBih>();
        if (backgroundMat==null) {
            Addressables.LoadAssetAsync<Material>("BackgroundView.mat").Completed += OnLoadAsset;
        }

        if (gameObject.transform.childCount>0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var it = gameObject.transform.GetChild(i).gameObject;
                Partition.Add(it);    
            }
        }

        isChosenObject = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                if (chosenObj.name != "empty")
                {
                    return true;
                }
                return false;
            });
        nameOfChosen = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                return chosenObj.name;
            });
    }

    void OnLoadAsset(AsyncOperationHandle<Material> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            backgroundMat = handle.Result;
        }
        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogWarning("Spawn object faild");
        }
    }

    void Start()
    {
        var chekObj = Observable.EveryFixedUpdate()
            .Subscribe(

            s=> {
                if (CountAllBones() != BoneOnScene.Count) {
                    getBoneBihScrpt();
                }
                //print($"BOS {}");
                materialControl();
            })
            .AddTo(this);
    }
        

    private int CountAllBones()
    {
        int count = 0;
        for (int i = 0; i < Partition.Count; i++)
        {
            foreach (var transf in Partition[i].transform)
            {
                count++;
            }
        }
        return count;
    }

    private void getBoneBihScrpt()
    {
        for (int i = 0; i < Partition.Count; i++)
        {
            foreach (Transform obj in Partition[i].transform)
            {
                var t = obj.GetComponent<BoneBih>();
                if (t==null) 
                {
                    obj.gameObject.AddComponent<BoneBih>();
                }
                bool chek = false;
                foreach (Transform item in Partition[i].transform)
                {
                    if (item.name==obj.name) {
                        chek = true;
                    }
                }
                if (chek) { 
                    BoneOnScene.Add(obj.GetComponent<BoneBih>());
                }
            }
        }
    }

    private void materialControl()
    {
        foreach(BoneBih bh in BoneOnScene)
        {
            if (chosenObj.name == "empty" &GameManager.Instance.getState()[2] !="" )
            {
                if (bh.gameObject.name == GameManager.Instance.getState()[2])
                {
                    bh.setChek();
                }
            }      
            if (bh.chosen) {
                if (chosenObj.name != bh.gameObject.name & chosenObj.name != "empty") {
                    var pbh = chosenObj.GetComponent<BoneBih>();
                    pbh.unchek();
                    chosenObj = bh.gameObject;
                }
                chosenObj = bh.gameObject;
                bh.setChosenMaterial();
            }

            if (!bh.chosen)
            {
                if (chosenObj.name == "empty")
                {
                    bh.setChosenMaterial();
                    bh.setStartRot();
                }
                if (chosenObj.name != "empty") {
                    bh.changeMaterial(backgroundMat);
                    bh.setStartRot();
                }
                if (chosenObj.name == bh.gameObject.name) {
                    chosenObj = emptyObj;
                }
            }
        }
    }

    public void setEmpty() {
        if (chosenObj!= emptyObj)
        {
            chosenObj.GetComponent<BoneBih>().unchek();
        }
        chosenObj = emptyObj;
    }

    public List<GameObject> getChosenGroup(string[] args) {
        List<GameObject> retList = new List<GameObject>();
        for (int i = 0; i < Partition.Count; i++)
        {
            if (Partition[i].name == GameManager.Instance.currentPartition)
            {
                for (int j = 0; j < args.Length; j++)
                {
                    for (int k = 0; k < Partition[i].transform.childCount; k++)
                    {
                        if (args[j]== Partition[i].transform.GetChild(k).name)
                        {
                            retList.Add(Partition[i].transform.GetChild(k).gameObject);
                        }
                    }
                }
            }
            else { break; }
        }
        return retList;
    }


    public GameObject getSelectedObj(string arg) {
        for (int i = 0; i < Partition.Count; i++)
        {
            if (Partition[i].name == GameManager.Instance.currentPartition)
            {
                for (int k = 0; k < Partition[i].transform.childCount; k++)
                {
                    if (arg == Partition[i].transform.GetChild(k).name)
                    {
                        return Partition[i].transform.GetChild(k).gameObject;
                    }
                }
            }
            else { break; }
        }
        return null;
    }






        

}