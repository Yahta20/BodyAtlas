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

    public string[]     postedBones  = new string[10];
    public string[]     postedPoints = new string[10];

    public List<string> chosenBones = new List<string>();

    public List<GameObject> gameObjectsOnScene = new List<GameObject>();

    public GameObject QPrefab;
    
    public bool initTest    ;
    public bool finishTest  ;
   
    bool chekBones = false;
    public bubblePointTest bbt;


    private Lang curlang;

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
       
        curlang = GameManager.Singelton.currentLang;
        quest.text = TextUI.Singelton.getLabel("Test of questions about points on bones");
        bbt.Activation(false);
    }
    private void langUpdate()
    {
        if (curlang != GameManager.Singelton.currentLang)
        {
            quest.text = TextUI.Singelton.getLabel("Test of questions about points on bones");
            curlang = GameManager.Singelton.currentLang;
        }
    }
    void Start()
    {
        //LoadScreen.Instance.changeState(StateOfLoading.hide);
        var screan = UIManager.Instance.screenSize.
            Where(w => w != Vector2.zero).
            //DistinctUntilChanged().
            Subscribe(s => {
                updateFoo(s);
                langUpdate();
            }).
            AddTo(this);

        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
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

                            //LoadScreen.Instance.changeState(StateOfLoading.show);

                        }
                        rtMainPanel.anchoredPosition = new Vector2(0, 0);
                        break;
                    case TypeOfTest.Points:
                        if (!initTest)
                        {
                            MakePointQuest();
                            //LoadScreen.Instance.changeState(StateOfLoading.show);
                            bbt.Activation(true);
                        }
                        rtMainPanel.anchoredPosition = new Vector2(Mathf.Lerp(rtMainPanel.anchoredPosition.x,0,Time.deltaTime),0);
                        break;
                    case TypeOfTest.Finish:
                        bbt.Activation(false);
                        rtMainPanel.anchoredPosition = new Vector2(rtMainPanel.sizeDelta.x,0);
                        break;
                }

                if (initTest)
                {
                    //выделение главных костей
                    if (gameObjectsOnScene.Count != 0) {

                        gameObjectsOnScene[0].SetActive(true);
                        if (gameObjectsOnScene[0].GetComponent<BonTestBih>()==null) { 
                            gameObjectsOnScene[0].AddComponent<BonTestBih>();
                        } 
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
                        if (gameObjectsOnScene.Count == 10| gameObjectsOnScene.Count == 1) 
                        {
                            if (!chekBones)
                            {
                                //LoadScreen.Instance.changeState(StateOfLoading.hide);
                                sortAnswers4point();
                            }
                        }
                    }
                }
            })
            .AddTo(this);
    }
     
    private void chekIgnors(ref bool[] chekArr)
    {
        for (int i = 0; i < LangEnv.Singelton.ignoreList.Length; i++)
        {
            chekArr[
            LangEnv.Singelton.currentBone.getBNomber(LangEnv.Singelton.ignoreList[i])
                ] = true;
        }
    }

    private void MakeRandomQuest()
    {
        var CountOfBones = LangManage.instance.bones.Count - 1;
        var AllBones = LangManage.instance.bones;
        int[] Bone2Post = new int[10];
        initTest = true;
        ClearList();
    }

    void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            gameObjectsOnScene.Add(handle.Result);
            gameObjectsOnScene[gameObjectsOnScene.Count - 1] 
                =   Instantiate(gameObjectsOnScene[gameObjectsOnScene.Count - 1], 
                                this.gameObject.transform.position, 
                                Quaternion.identity, 
                                this.gameObject.transform);
            gameObjectsOnScene[gameObjectsOnScene.Count - 1].name = handle.Result.name;
        }
    }

    public void CutPast() {
        if (gameObjectsOnScene.Count>0) { 
            Destroy(gameObjectsOnScene[0]);
            gameObjectsOnScene.RemoveAt(0);
        }
        if (gameObjectsOnScene.Count == 0) {
            ResultBeh.Instance.ClearListQuestions();
            if (currentState == TypeOfTest.Bones)
            {
                ResultBeh.Instance.CreatingResults(postedBones, chosenBones);
            }
            if (currentState == TypeOfTest.Points)
            {
                ResultBeh.Instance.CreatingResults(postedPoints, chosenBones);
            }
        currentState = TypeOfTest.Finish;
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

    private void MakeBoneQuest()
    {

        var CountOfBones = LangEnv.Singelton.currentBone.BONES.Length;
        var AllBones = LangEnv.Singelton.currentBone.BONES;
        
        int[] Bone2Post = new  int[10];
        var chekArr = new bool[CountOfBones];

        chekIgnors(ref chekArr);

        for (int i = 0; i < Bone2Post.Length; i++)
        {
            var randBones = Random.Range(0, CountOfBones);
            while (true)
            {
                if (!chekArr[randBones])
                {
                    chekArr[randBones] = true;
                    break;
                }
                randBones = Random.Range(0, CountOfBones);
            }
            postedBones[i] = AllBones[randBones].bonesDic[Lang.lat];
            Bone2Post  [i] = randBones;

            //print($"{postedBones[i]}/{postedPoints[i]}");
            Addressables.LoadAssetAsync<GameObject>(postedBones[i]).Completed += OnLoadAsset;
        }
        initTest = true;
        ClearList();
    }
    private void MakePointQuest()
    {      
        var CountOfBones = LangEnv.Singelton.currentBone.BONES.Length;
        var AllBones = LangEnv.Singelton.currentBone.BONES;
        var chekArr = new bool[CountOfBones];

        chekIgnors(ref chekArr);

        for (int i = 0; i < postedBones.Length; i++)
        {
            var randBones = Random.Range(0, CountOfBones);
            while (true)
            {
                if (!chekArr[randBones])
                {
                    if (AllBones[randBones].point.Length>1)
                    {
                    chekArr[randBones] = true;
                    break;
                    }
                }
                randBones = Random.Range(0, CountOfBones);
            }
            var randPoint   = Random.Range(0, AllBones[randBones].point.Length);
            postedBones[i]  = AllBones[randBones].bonesDic[Lang.lat];
            postedPoints[i] = AllBones[randBones].point[randPoint].lat;
            //print($"{postedBones[i]}/{postedPoints[i]}");
            Addressables.LoadAssetAsync<GameObject>(postedBones[i]).Completed += OnLoadAsset;
            }
        initTest = true;
        ClearList();
    }
    public void CreateAnswers()
    {
        switch (currentState)
        {
        
        case TypeOfTest.Random:
           
            break;
        case TypeOfTest.Bones:
            if (gameObjectsOnScene.Count != 0)
            {
                
                var correctList = new string[5];
                correctList[0]  = gameObjectsOnScene[0].name;

                var CountOfBones    = LangEnv.Singelton.currentBone.BONES.Length;
                var AllBones        = LangEnv.Singelton.currentBone.BONES;

                var chekArr = new bool[CountOfBones];
                    chekArr[
                    LangEnv.Singelton.currentBone.getBNomber(gameObjectsOnScene[0].name)
                    ] = true;
                    chekIgnors(ref chekArr);
                var randBones       = Random.Range(0, CountOfBones);
                for (int i = 1; i < correctList.Length ; i++)
                {
                    //proverka povtorenia
                    randBones = Random.Range(0, CountOfBones);
                    while (true)
                    {
                        if (!chekArr[randBones])
                        {
                            chekArr[randBones] = true;
                            break;
                        }
                        randBones = Random.Range(0, CountOfBones);
                    }
                        correctList[i] = AllBones[randBones].bonesDic[Lang.lat];
                }
                PublishAnswers(correctList);
            }
            break;

        case TypeOfTest.Points:
            if (gameObjectsOnScene.Count != 0)
            {
                
                var correctList = new string[5];
                var bonename = gameObjectsOnScene[0].name;

                correctList[0] = postedPoints[10 - gameObjectsOnScene.Count];
                var CountOfBones = LangEnv.Singelton.currentBone.BONES.Length;
                var AllBones = LangEnv.Singelton.currentBone.BONES;

                var lngMng = LangEnv.Instance;

                var chekArr = new bool[CountOfBones];
                    chekArr[LangEnv.Singelton.currentBone.getBNomber(gameObjectsOnScene[0].name)
                    ] = true;
                    chekIgnors(ref chekArr);
                
                var randBones = Random.Range(0, CountOfBones);
                var randPoint = Random.Range(0, AllBones[randBones].point.Length);
                for (int i = 1; i < correctList.Length;  ++i)
                {
                    //proverka povtorenia
                    randBones = Random.Range(0, CountOfBones);
                    while (true)
                    {
                        if (!chekArr[randBones])
                        {
                            if (AllBones[randBones].point.Length>1)
                            {
                                    //chekArr[randBones] = true;
                                    randPoint = Random.Range(0, AllBones[randBones].point.Length);
                                    int t = 0;
                                for (int j = 0; j < i; j++)
                                {
                                    if (correctList[j]==AllBones[randBones].point[randPoint].lat)
                                    {
                                        break;
                                    }
                                        t++;
                                }
                                if (t==i)
                                {
                                    break;
                                }
                            }
                        }
                        randBones = Random.Range(0, CountOfBones);
                    }
//                    randPoint = Random.Range(0, AllBones[randBones].point.Length);
                    correctList[i] = AllBones[randBones].point[randPoint].lat; //bonesDic[Lang.lat];
                }
                PublishAnswers(correctList);
            }
            break;
        }
    }
                                
                                    



                                
                          

                    
                        

                    
                    
                        
    public Vector3 getBonepoint() {
        if (currentState == TypeOfTest.Points &
            gameObjectsOnScene.Count>0
            )
            foreach (var item in gameObjectsOnScene)
            {
                if (item.activeInHierarchy)
                {
                    foreach (Transform t in item.transform)
                    {
                        if (postedPoints[chosenBones.Count]==t.name)
                        {
                            return t.position;
                        }
                    }
                }
            }
        return Vector3.zero;
    }  

    private void sortAnswers4point()
    {
        for (int j = 0; j < postedBones.Length; j++)
        {
            for (int i = j; i < gameObjectsOnScene.Count; i++)
            {
                if (i!=j)
                {
                    if (gameObjectsOnScene[i].name==postedBones[j])
                    {
                        var baf = gameObjectsOnScene[j]; //tot chtonugen
                        gameObjectsOnScene[j] = gameObjectsOnScene[i];
                        gameObjectsOnScene[i] = baf;
                    }
                }
            }
        }
        chekBones = true;
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
            if (currentState== TypeOfTest.Bones)
            {
                sgo.SetDicName(LangEnv.Singelton.currentBone.getBoneDic(list[i]));
            }
            if (currentState == TypeOfTest.Points)
            {
                sgo.SetDicName(LangEnv.Singelton.currentBone.getPointDic(list[i]));
            }
            go.transform.SetParent(rtQPlane);  //SetParent(rtQPlane);
        }
    }
}