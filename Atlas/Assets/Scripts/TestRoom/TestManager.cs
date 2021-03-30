using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UniRx;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    public static TestManager Instance;

    public RectTransform rtMainPanel;
    public Text quest;
    public RectTransform rtQPlane;

    public string[]     postedBones = new string[10];
    public List<string> chosenBones = new List<string>();
    public List<GameObject> gameObjectsOnScene = new List<GameObject>();




    public GameObject QPrefab;
    
    public bool initTest    ;
    public bool finishTest  ;
    public bool chekTest;

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
        initTest     = false;
        finishTest   = false;
        chekTest     = false;
    }

    void Start()
    {
        var screan = UIManager.Instance.screenSize.
            Where(w => w != Vector2.zero).
            //DistinctUntilChanged().
            Subscribe(s => {
                updateFoo(s);
            }).
            AddTo(this);

        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                if (initTest)
                {

                    //выделение главных костей
                    if (gameObjectsOnScene.Count != 0) {

                        gameObjectsOnScene[0].SetActive(true);
                        if (!finishTest) {
                            CreateAnswers();
                            finishTest = true;
                        }

                        if (gameObjectsOnScene.Count != 1) { 
                            for (int i = 1; i < gameObjectsOnScene.Count; i++)
                            {
                                gameObjectsOnScene[i].SetActive(false);
                            }
                        }
                        if (gameObjectsOnScene.Count == 10) 
                        { 
                        
                            if (!chekTest) {
                                for (int i = 0; i < gameObjectsOnScene.Count; i++)
                                {
                                    postedBones[i] = gameObjectsOnScene[i].name;
                                }
                                chekTest = true;
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
        ClearList();
        
    }

    private void MakeRandomQuest()
    {
      
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
            ResultBeh.Instance.ClearListQuestions();
            ResultBeh.Instance.CreatingResults(postedBones, chosenBones);
        }
    }
            


    public void updateFoo(Vector2 size) {
        rtMainPanel.sizeDelta           = new Vector2(size.x*0.25f,0);
        quest.rectTransform.sizeDelta   = new Vector2(0, size.y * 0.07f);
        rtQPlane.sizeDelta              = new Vector2(0, size.y - size.y * 0.07f);
    }

    public void setState(TypeOfTest l)
    {
        currentState = l;
    }

    public void ClearList() {
        if (rtQPlane.transform.childCount != 0)
        {
            foreach (Transform t in rtQPlane.transform)
            {
                Destroy(t.gameObject);
            }
        }
    }

    public void setAnswer(string answ) {
        if (chosenBones.Count <= postedBones.Length) {
            chosenBones.Insert(chosenBones.Count,answ);//Add(answ);
        }
        CutPast();
        ClearList();
        CreateAnswers();
    }

    public void CreateAnswers()
    {
        if (gameObjectsOnScene.Count!=0) { 

            var correct         = gameObjectsOnScene[0].name;
            var correctList     = new string[5];
            correctList[0]      = correct;
            var CountOfBones    = LangManage.instance.bones.Count - 1;
            var AllBones        = LangManage.instance.bones;
        
            for (int i = 1; i < correctList.Length; i++)
            {
                var randBones = Random.Range(0, CountOfBones);
                if (AllBones[randBones].Name[Lang.lat] == correct) {
                    i = 1;
                }
                correctList[i] = AllBones[randBones].Name[Lang.lat];

                for (int q = 1; q <= i; q++)
                {
                    if (AllBones[randBones].Name[Lang.lat] == correctList[q])
                    {
                        randBones = Random.Range(0, CountOfBones);
                        q = 1;
                    }
                }
                //var s = $"{i} = {correctList[i]}";
                //print(s);
            }
            //finishTest = false;
            //print("list "+correctList.Length);
            PublishAnswers(correctList);
        }
    }
            
    public void PublishAnswers(string[] list)
    {
        for (int i = 0; i < list.Length; i++)
        { 
            var rand = Random.Range(i, list.Length);
            var buff = list[rand];
            list[rand] = list[i];
            list[i] = buff;
        }

            for (int i = 0; i < list.Length; i++)
        {
            var go = Instantiate(QPrefab);
            var sgo = go.GetComponent<QuestionBeh>();
            sgo.setName(list[i]);
            go.transform.SetParent(rtQPlane);  //SetParent(rtQPlane);
        }
    }



}

