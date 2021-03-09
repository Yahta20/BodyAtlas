using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public enum SideOfScreen { 
     top=0,
     right=1,
     bottop=2,
     left=3
}


public class boobleMaker : MonoBehaviour
{

    public static boobleMaker Instance;
    public Canvas mainCanvas;

    public RectTransform rtPanel;
    public GameObject Prefab;
    public List<GameObject>PrefabList;

    [Range(0, 0.15f)]
    public float scale;
    void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        rtPanel = GetComponent<RectTransform>();

    }

    void Start()
    {
            clearContent();
            MakelistOfBones();
            var RPI = rightPanel.Instance;
            var croom = ClassroomBeh.Instance;
            var screan = UIManager.Instance.screenSize.
                Where(w => w != Vector2.zero).
                Subscribe(s => {
                    updateFoo(s);
                }).
                AddTo(this);
    }
        


                





    
    private void updateFoo(Vector2 screenSize)
    {

        var TopPanel = topPanel.Instance.rtPanel;
        var RightPanel = rightPanel.Instance.rtPanel;
        rtPanel.sizeDelta = new Vector2(screenSize.x+RightPanel.anchoredPosition.x- RightPanel.sizeDelta.x, screenSize.y);
        ListManager();

    }


    public void ListManager() {
        if (PrefabList.Count>0) {
            int index = 0;
            int[] numbers = new int[4] { 0,0,0,0};
            foreach (var item in PrefabList)
            {
                var scriptGO = item.GetComponent<InfoPointBeh>();

                var step = PrefabList.Count /4;
                var region = step>0? index/ step: step;
                scriptGO.index = index;
                switch (region)
                {
                    case (0):
                        scriptGO.setPivot(new Vector2(0, 1));
                        scriptGO.setSetAncors(new Vector2(0, 1), new Vector2(0, 1));
                        scriptGO.sos = SideOfScreen.top;
                        scriptGO.index = numbers[0];
                        scriptGO.scaleSize = scale;
                        numbers[0]++;
                        break;
                    
                    case (1):
                        scriptGO.setPivot(new Vector2(1, 1));
                        scriptGO.setSetAncors(new Vector2(1, 1), new Vector2(1, 1));
                        scriptGO.sos = SideOfScreen.right;
                        scriptGO.index = numbers[1];
                        scriptGO.scaleSize = scale;
                        numbers[1]++;
                        break;

                    case (2):
                        scriptGO.setPivot(new Vector2(1, 0));
                        scriptGO.setSetAncors(new Vector2(1, 0), new Vector2(1, 0));
                        scriptGO.sos = SideOfScreen.bottop;
                        scriptGO.index = numbers[2];
                        scriptGO.scaleSize = scale;
                        numbers[2]++;
                        break;
                    
                    case (3):
                        scriptGO.setPivot(new Vector2(0, 0));
                        scriptGO.setSetAncors(new Vector2(0, 0), new Vector2(0, 0));
                        scriptGO.sos = SideOfScreen.left;
                        scriptGO.index = numbers[3];
                        scriptGO.scaleSize = scale;
                        numbers[3]++;
                        break;
                    
                    default:
                        scriptGO.setPivot(new Vector2(0, 0));
                        scriptGO.setSetAncors(new Vector2(0, 0), new Vector2(0, 0));
                        scriptGO.sos = SideOfScreen.left;
                        scriptGO.index = numbers[3];
                        scriptGO.scaleSize = scale;
                        numbers[3]++;
                        break;
                }
                index++;
            }
        }
    }

    public void CreateUI(List<string> srtList) {
        clearContent();
        PublishList(srtList);
    }

    private void clearContent()
    {
        if (transform.childCount != 0)
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }
        PrefabList.Clear();
    }

    private void MakelistOfBones()
    {
        var list = ClassroomBeh.Instance.objOnScene;
        var listOname = new List<string>();
        foreach (var item in list)
        {
            var name = item.gameObject.name;
            if (name != "Skeleton")
            {
                listOname.Add(name);
            }
        }
    }

    private void PublishList(List<string> srtList)
    {

        int numer = 1;
        foreach (var str in srtList)
        {

            if (name != "osseus")
            {
             //   listOname.Add(name);
            GameObject go = Instantiate(Prefab);
            PrefabList.Add(go);
            var t = go.GetComponent<InfoPointBeh>();

            go.transform.SetParent(transform);
            go.name = str;
            t.setSetAncors(new Vector2 (0,-numer*50));
            t.UItext.text = numer.ToString();
            numer++;

            }
        }
    }





    


    private void MakeFromList(List<string> srtList) {
        if (srtList.Count==0) {
            GameObject go = Instantiate(Prefab);
            PrefabList.Add(go);
            var t = go.GetComponent<InfoPointBeh>();
            go.transform.SetParent(transform);
            t.UItext.text = "none";
        }

    }

    private void MakelistOfPoints()
    {
        var listOname = new List<string>();
        foreach (var item in ClassroomBeh.Instance.chosenObj.GetComponent<BoneBih>().specPlases)
        {
            listOname.Add(item.name);
        }
        //PublishList(listOname, false);
    }

}


    // public GameObject ChosenOne;
    // Start is called before the first frame update
            //var publishing = ClassroomBeh.Instance.isChangedObj.
            //DistinctUntilChanged().
            //Subscribe(s=> {
            //    CreateUI();
            //
            //}).
            //AddTo(this);
                //var showString = $"index:{index};\tstep:{step};\tregion:{region};";
                //print(showString);
                //scriptGO
        //if (list.Count == 0) {
        //    MakelistOfBones();
        //}
        //PublishList(listOname, true);
        //if (LangManage.instance.bones!=null) {
        //   //print(LangManage.instance.bones.Count);
        //}
        //t.setName(LangManage.instance.FindBone(str));
        //t.setName(LangManage.instance.FindPoint(str));
        //t.setNumber(numer.ToString());
        //if (bone)
        //{
        //    t.setTranslate(LangManage.instance.FindBoneDic(str));
        //}
        //else
        //{
        //    t.setTranslate(LangManage.instance.FindPointDic(str));
        //}
        //updateFoo();