using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PreparationBeh : MonoBehaviour
{
    public TextAsset DataToLoad;
    public listOfData data4Load;

    private Transform currentTransform;
    private bool Loadet = false;

    void Awake()
    {

        currentTransform = gameObject.transform;

        if (DataToLoad != null) { 
             data4Load = JsonUtility.FromJson<listOfData>(DataToLoad.text);
        }
        //if (DataToLoad == null) {
        //    GameEnviroment.Singelton.currentstate;
        //
        //}

        
    }

    void Start()
    {
        foreach (var item in data4Load.saveList)
        {
            Addressables.LoadAssetAsync<GameObject>(item.name).Completed += OnLoadAsset;
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == data4Load.saveList.Length & !Loadet) {
            Loadet = true;
            transform.localScale = Vector3.one*0.36f;
            gameObject.AddComponent<ClassroomBeh>();
            //print($"Loadet is {Loadet}");
        }
    }


    void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
        
            var spawnObject = handle.Result;
            // удалить клоне с имени обьекта
            //var name = spawnObject - "(clone)";


            spawnObject = Instantiate(spawnObject);//,(Clone)
            spawnObject.name = spawnObject.name.Replace("(Clone)", "");//.Trim("(Clone)");
            spawnObject.transform.SetParent(transform);

            //data4Load.GetPosition(spawnObject.name),
            //data4Load.GetRotation(spawnObject.name), transform);
            spawnObject.transform.localScale = data4Load.GetScale(spawnObject.name);
            //Comendant.Instance.AddObgetOnScene(gerbObject);

        }
        if (handle.Status == AsyncOperationStatus.Failed) {
            Debug.LogWarning("Spawn object faild");
        }
    }
}
