using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PreparationBeh : MonoBehaviour
{
    public TextAsset DataToLoad;
    public listOfData data4Load;
    private bool Loadet = false;
    public GameObject LoadPlace;


    void Awake()
    {
        if (LoadPlace==null)
        {
            LoadPlace = new GameObject("LoadPlace");
        }
        Loadet = false;
        
        data4Load = JsonUtility.FromJson<listOfData>(DataToLoad.text);
        
        var main = data4Load.GetObjByPar("main");
        LoadPlace.transform.position = data4Load.GetPosition(main.name);
        LoadPlace.transform.rotation = data4Load.GetRotation(main.name);
        LoadPlace.transform.localScale = data4Load.GetScale(main.name);
        LoadPlace.name = main.name;
        foreach (var item in data4Load.saveList)
        {
            if (item.parent=="root")
            {
                //print($"re {item.name}");
                var go = new GameObject(item.name);
                go.transform.position  =data4Load.GetPosition   (item.name);
                go.transform.rotation  =data4Load.GetRotation   (item.name);
                go.transform.localScale=data4Load.GetScale      (item.name);
                go.transform.SetParent(LoadPlace.transform);
            }
        }
        foreach (var item in data4Load.saveList)
        {
            if (item.parent != "root" & item.parent != "main")
            {
                Addressables.LoadAssetAsync<GameObject>(item.name).Completed += OnLoadAsset;
            }
        }
    }

    private void FixedUpdate()
    {
        if (data4Load.saveList.Length == getChildcount()) {
            if (LoadPlace.GetComponent<ClassroomBeh>()==null)
            {
                LoadPlace.AddComponent<ClassroomBeh>();
            }
            if (LoadPlace.GetComponent<ClassroomBeh>() != null)
            {
                DestroyImmediate(this.gameObject);
                DestroyImmediate(this);

            }
            
        }
          
    }
            







    //private void OnLoadTextAsset(AsyncOperationHandle<TextAsset> handle)
    //{
    //    if (handle.Status == AsyncOperationStatus.Succeeded)
    //    {
    //        DataToLoad = handle.Result;
    //        data4Load = JsonUtility.FromJson<listOfData>(DataToLoad.text);
    //        SpawnObj();
    //    }
    //    if (handle.Status == AsyncOperationStatus.Failed)
    //    {
    //        Debug.LogWarning("Spawn object faild");
    //    }
    //}
    void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var spawnObject = handle.Result;
            var obj = data4Load.GetObjByNam(spawnObject.name);

            spawnObject = Instantiate(spawnObject);//,(Clone)
            spawnObject.name = spawnObject.name.Replace("(Clone)", "");//.Trim("(Clone)");
            
            spawnObject.transform.position      = data4Load.GetPosition(spawnObject.name);
            spawnObject.transform.rotation      = data4Load.GetRotation(spawnObject.name);
            spawnObject.transform.localScale    = data4Load.GetScale(spawnObject.name);
            //print($"re {obj.name}");
            spawnObject.transform.SetParent(getParent(obj.parent));
            //if (obj.parent == "root")
            //{
            //}


        }
        if (handle.Status == AsyncOperationStatus.Failed) {
            Debug.LogWarning("Spawn object faild");
        }
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