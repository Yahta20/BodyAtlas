using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class PreparationBeh : MonoBehaviour
{
    public TextAsset DataToLoad;
    public listOfData data4Load;
    private bool Loadet = false;
    public GameObject LoadPlace;


    private void Start()
    {
        if (LoadPlace==null)
        {
            LoadPlace = new GameObject("LoadPlace");
        }
        Loadet = false;
        data4Load = JsonUtility.FromJson<listOfData>(DataToLoad.text);
    }   
    void Spawn() {
        var main = data4Load.GetObjByPar("main");
        LoadPlace.transform.position = data4Load.GetPosition(main.name);
        LoadPlace.transform.rotation = data4Load.GetRotation(main.name);
        LoadPlace.transform.localScale = data4Load.GetScale(main.name);
        LoadPlace.name = main.name;
        foreach (var item in data4Load.saveList)
        {
            if (item.parent == "root")
            {
                //print($"re {item.name}");
                var go = new GameObject(item.name);
                go.transform.position = data4Load.GetPosition(item.name);
                go.transform.rotation = data4Load.GetRotation(item.name);
                go.transform.localScale = data4Load.GetScale(item.name);
                go.transform.SetParent(LoadPlace.transform);
                break;
            }
        }
        foreach (var item in data4Load.saveList)
        {
            if (item.parent != "root" & item.parent != "main")
            {
                    //Addressables.LoadAssetAsync<GameObject>(item.name).Completed += OnLoadAsset;
                     var asynAction = Addressables.LoadAssetAsync<GameObject>
                        (item.name);
                    
                    asynAction.Completed += OnLoadAsset;
                //yield return new WaitForEndOfFrame();
            }
        }
        //yield break;
    }

    private void FixedUpdate()
    {
        if (!Loadet)
        {
            Loadet = true;
            //StartCoroutine(
            //    );
            Spawn();
        }

        if (data4Load.saveList.Length == getChildcount()) {
            if (LoadPlace.GetComponent<ClassroomBeh>()==null)
            {
                LoadPlace.AddComponent<ClassroomBeh>();
            }
            if (LoadPlace.GetComponent<ClassroomBeh>() != null)
            {
                //LoadScreen.Instance.changeState(StateOfLoading.hide);
                DestroyImmediate(this.gameObject);
                DestroyImmediate(this);
            }
        }

    }
    void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {

            var spawnObject = handle.Result;
            var obj = data4Load.GetObjByNam(spawnObject.name);

            spawnObject = Instantiate(spawnObject);//,(Clone)
            spawnObject.name = spawnObject.name.Replace("(Clone)", "");//.Trim("(Clone)");


            //var cur = data4Load.getByHashCode();
            var cur = getUnspawnObj(spawnObject);

            spawnObject.transform.position = new Vector3(cur.position[0], cur.position[1], cur.position[2]);//data4Load.GetPosition(spawnObject.name);
            spawnObject.transform.rotation = new Quaternion(cur.rotation[0], cur.rotation[1], cur.rotation[2], cur.rotation[3]);//data4Load.GetRotation(spawnObject.name);
            spawnObject.transform.localScale   = new Vector3(cur.scale[0], cur.scale[1], cur.scale[2]);//data4Load.GetScale(spawnObject.name);
            
            spawnObject.transform.SetParent(getParent(obj.parent));
            cur.spawn = true;
        }
        if (handle.Status == AsyncOperationStatus.Failed) {
            Debug.LogWarning("Spawn object faild");
        }
    }

    private ObjData getUnspawnObj(GameObject go)
    {
        ObjData[] arro = data4Load.FindArrByName(go.name);
        foreach (var item in arro)
        {
            if (!item.spawn)
            {
                return item;
            }
        }
        
        return null;
    }

    private GameObject[] findArr(string name)
    {
        var t = new List<GameObject>();
        var foundObjects = FindSceneObjectsOfType (typeof(GameObject));
        foreach (var item in foundObjects)
        {
            if (item.name==name) {
                t.Add(item as GameObject);
            }
        }
        return t.ToArray();
    }
    
    Transform getParent(string s) {
            //print(s);
        foreach (Transform item in LoadPlace.transform)
        {
            if (item.gameObject.name == s)
            {
                return item;
            }
        }
        return null;
    }

    int getChildcount() {
        int c = 0;
        if (LoadPlace !=null )
        {
            c++;
        }
        foreach (Transform item in LoadPlace.transform)
        {
            c++;
            c+=item.childCount;
        }
        return c;
    }
}



  