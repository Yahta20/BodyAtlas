using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;



public class ClassroomBeh : MonoBehaviour
{
    public static ClassroomBeh Instance;

    //public Material regularMat;
    //public Material chosenMat;
    public Material backgroundMat;

    public GameObject chosenObj;
    private GameObject emptyObj;
    public List<BoneBih> objOnScene             {get; private set;}

    public IObservable <bool> isChosenObject    {get; private set;}
    public IObservable <string> isChangedObj    {get; private set;}

    //public BoneBih just;
    // Start is called before the first frame update
    //print(transform.childCount+" "+objOnScene.Count);
    //print("TR TRTR");
    //print("TRTRTR "+t);
    //if (chosenObj.name == "empty")
    //{
    //    bh.changeMaterial(regularMat);
    //}
    //print(just.gameObject.name);
    //chosenObj = bh.gameObject;

    void Awake() {
        GameEnviroment.Singelton.setLanguage(0);
        Instance = this;
        emptyObj = new GameObject("empty");
        chosenObj = emptyObj;
        objOnScene = new List<BoneBih>();
        isChosenObject = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                if (chosenObj.name != "empty")
                {
                    return true;
                }
                return false;
            });
        isChangedObj = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                return chosenObj.name;
            });
        
    }

    void Start()
    {
        var chekObj = Observable.EveryFixedUpdate()
            .Subscribe(
            s=> {
                if (objOnScene.Count!=transform.childCount) {
                    getBoneBihScrpt();
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
            if (bh.chosen) {
                if (chosenObj.name != bh.gameObject.name & chosenObj.name != "empty") {

                    var pbh = chosenObj.GetComponent<BoneBih>();
                    pbh.unchek();
                    chosenObj = bh.gameObject;
                }
                chosenObj = bh.gameObject;
                //bh.changeMaterial(chosenMat);
                bh.setChosenMaterial();

            }

            if (!bh.chosen)
            {
                if (chosenObj.name == "empty")
                {
                    //bh.changeMaterial(regularMat);
                    bh.setChosenMaterial();
                    bh.setStartRot();
                }
                if (chosenObj.name != "empty") {
                    bh.changeMaterial(backgroundMat);
                    bh.setStartRot();
                    //bh.setBackMaterial();
                }
                if (chosenObj.name == bh.gameObject.name) {
                    //
                    chosenObj = emptyObj;
                }
            }
        }
    }
                


}
