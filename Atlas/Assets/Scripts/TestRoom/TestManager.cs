using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UniRx;

public class TestManager : MonoBehaviour
{
    public static TestManager Instance;

    public RectTransform rtMainPanel;

    public bool[] stepOfTest = new bool[10];

    public List<GameObject> gameObjectsOnScene = new List<GameObject>();

    public bool initTest    ;
    public bool finishTest  ;

    public enum TypeOfTest {
        Chousing    = 0,
        Random      = 1,
        Bones       = 2,
        Points      = 3,
        Finish      = 4
    }

    public TypeOfTest currentState;

    void Awake()
    {
        
        Instance = this;
        currentState = TypeOfTest.Chousing;
        initTest    =false;
        finishTest  =false;
    }


    void Start()
    {
        //var screan = UIManager.Instance.screenSize.
        //    Where(w => w != Vector2.zero).
        //    //DistinctUntilChanged().
        //    Subscribe(s => {
        //        updateFoo(s);
        //    }).
        //    AddTo(this);

        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                if (initTest)
                {
                    if (gameObjectsOnScene.Count != 0) {
                        gameObjectsOnScene[0].SetActive(true);
                        if (gameObjectsOnScene.Count != 1) { 
                            for (int i = 1; i < gameObjectsOnScene.Count; i++)
                            {
                                gameObjectsOnScene[i].SetActive(false);
                            }
                        }
                    }

                }
                switch (currentState)
                {
                    case TypeOfTest.Chousing:
                        rtMainPanel.anchoredPosition =new Vector2(rtMainPanel.sizeDelta.x,0);
                        
                        break;
                    case TypeOfTest.Random:
                        if (!initTest) { 
                        MakeRandomQuest();
                        }
                        
                        rtMainPanel.anchoredPosition = new Vector2(Mathf.Lerp(rtMainPanel.anchoredPosition.x,0,Time.deltaTime),0);
                        break;
                    case TypeOfTest.Bones:
                        if (!initTest)
                        {
                        MakeBoneQuest();
                        }
                        rtMainPanel.anchoredPosition = new Vector2(0, 0);
                        break;
                    case TypeOfTest.Points:
                        if (!initTest)
                        {
                            MakePointQuest();
                        }
                        rtMainPanel.anchoredPosition = new Vector2(Mathf.Lerp(rtMainPanel.anchoredPosition.x,0,Time.deltaTime),0);
                        break;
                    case TypeOfTest.Finish:

                        rtMainPanel.anchoredPosition = new Vector2(rtMainPanel.sizeDelta.x,0);
                        break;
                }

            })
            .AddTo(this);
    }

    private void MakePointQuest()
    {
       
    }

    private void MakeBoneQuest()
    {

        var CountOfBones = LangManage.instance.bones.Count-1;
        string[] postedBones = new string[10];
        
        var AllBones = LangManage.instance.bones;
        int[] Bone2Post = new int[10];
        
        for (int i = 0; i < Bone2Post.Length; i++)
        {
            var randBones = Random.Range(0, CountOfBones);
            //proverka povtorenia
            for (int q = 0; q <= i; q++)
            {
                if (randBones==Bone2Post[q]) {
                   randBones = Random.Range(0, CountOfBones);
                    q = 0;
                }
            }
            postedBones[i] = AllBones[randBones].Name[Lang.lat];
            Bone2Post[i] = randBones;
            Addressables.LoadAssetAsync<GameObject>(postedBones[i]).Completed += OnLoadAsset;
            //var s = $"{i}:={Bone2Post[i]}=>{postedBones[i]}";
            //print(s);
        }
        initTest = true;
    }




    void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            gameObjectsOnScene.Add(handle.Result);
            gameObjectsOnScene[gameObjectsOnScene.Count - 1] = Instantiate(gameObjectsOnScene[gameObjectsOnScene.Count - 1], this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            gameObjectsOnScene[gameObjectsOnScene.Count - 1].name = handle.Result.name;
            //gerbObject = handle.Result;
            //gerbObject = Instantiate(gerbObject, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
        }
    }


    public void CutPast() {
        if (gameObjectsOnScene.Count>0) { 
        Destroy(gameObjectsOnScene[0]);
        gameObjectsOnScene.RemoveAt(0);
        }
        if (gameObjectsOnScene.Count == 0) {
            currentState = TypeOfTest.Finish;
        }
    }





    private void MakeRandomQuest()
    {
      
    }

    public void updateFoo(Vector2 size) { 
    }
        
    public void setState(TypeOfTest l)
    {
        currentState = l;
    }
}

