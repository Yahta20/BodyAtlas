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


    void Awake()
    {
        var s = "GameEnviroment";
        var t = s.IndexOf("sw");
        print(t);
        //if (DataToLoad != null) { 
        //     data4Load = JsonUtility.FromJson<listOfData>(DataToLoad.text);
        //}
            var data = GameEnviroment.Singelton.currentstate.ToString()+".json";
            Addressables.LoadAssetAsync<TextAsset>(data).Completed += OnLoadTextAsset;
        //if (DataToLoad == null) {
        //}
    }
    void Update()
    {
        if (transform.childCount == data4Load.saveList.Length & !Loadet) {
            Loadet = true;
            this.enabled = false;
            transform.localScale = Vector3.one*0.15f;
            //gameObject.AddComponent<ClassroomBeh>();
            print($"Loadet is {Loadet}");
        }
    }
    private void OnLoadTextAsset(AsyncOperationHandle<TextAsset> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            DataToLoad = handle.Result;
            data4Load = JsonUtility.FromJson<listOfData>(DataToLoad.text);
            SpawnObj();
        }
        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogWarning("Spawn object faild");
        }
    }
    void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var spawnObject = handle.Result;
            spawnObject = Instantiate(spawnObject);//,(Clone)
            spawnObject.name = spawnObject.name.Replace("(Clone)", "");//.Trim("(Clone)");
            //spawnObject.transform.position= data4Load.GetPosition(spawnObject.name);
            //spawnObject.transform.rotation = data4Load.GetRotation(spawnObject.name);
            //spawnObject.transform.localScale = data4Load.GetScale(spawnObject.name);
            spawnObject.transform.SetParent(transform);
        }
        if (handle.Status == AsyncOperationStatus.Failed) {
            Debug.LogWarning("Spawn object faild");
        }
    }

    void SpawnObj() {
        foreach (var item in data4Load.saveList)
        {
            Addressables.LoadAssetAsync<GameObject>(item.name).Completed += OnLoadAsset;
        }
    }
}