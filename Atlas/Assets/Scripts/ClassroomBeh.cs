using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ClassroomBeh : MonoBehaviour
{
    public Material regularMat;
    public Material chosenMat;
    public Material backgroundMat;

    public GameObject chosenObj;
    private GameObject emptyObj;
    public List<BoneBih> objOnScene;

    //public BoneBih just;

    void Awake() {
        emptyObj = new GameObject("empty");
        chosenObj = emptyObj;
    }
    // Start is called before the first frame update
    void Start()
    {
        var chekObj = Observable.EveryFixedUpdate()
            .Subscribe(
            s=> {
                if (objOnScene.Count!=transform.childCount) {
                    getBoneBihScrpt();
                    //print(transform.childCount+" "+objOnScene.Count);
                }
                materialControl();
            })
            .AddTo(this);
    }
                
                




    private void getBoneBihScrpt()
    {
        foreach (Transform obj in transform)
        {
            var t = obj.GetComponent<BoneBih>();
            if (t==null) 
            {
                //print("TR TRTR");
                obj.gameObject.AddComponent<BoneBih>();
            }
            bool chek = true;
            foreach (Transform item in transform)
            {
                if (item.name==obj.name) {
                    chek = true;
                }
            }
            if (chek) { 
                //print("TRTRTR "+t);
                objOnScene.Add(
                obj.GetComponent<BoneBih>()
                );
            }
        }
    }


    private void materialControl()
    {
        foreach(BoneBih bh in objOnScene)
        {
            //if (chosenObj.name == "empty")
            //{
            //    bh.changeMaterial(regularMat);
            //}
            if (bh.chosen) {
                if (chosenObj.name != bh.gameObject.name & chosenObj.name != "empty") {
                    var pbh = chosenObj.GetComponent<BoneBih>();
                    //print(just.gameObject.name);
                    pbh.unchek();
                    chosenObj = bh.gameObject;
                }
                chosenObj = bh.gameObject;
                bh.changeMaterial(chosenMat);
            }


            if (!bh.chosen)
            {
                //chosenObj = bh.gameObject;
                if (chosenObj.name == "empty")
                {
                    bh.changeMaterial(regularMat);
                }
                if (chosenObj.name != "empty") {
                    bh.changeMaterial(backgroundMat);
                }
                if (chosenObj.name == bh.gameObject.name) {
                    chosenObj = emptyObj;
                }
            }
        }
    }
                


}
