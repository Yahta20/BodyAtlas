using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class BoneBih : MonoBehaviour
{
    public Collider boneCollider;
    public List<Transform> specPlases;
    public bool chosen { get; private set; }
    //public Material currentMat { get; private set; }
    public Renderer currentRender { get; private set;}
    public string NameOfBone;
    private Vector2 screenBounds;
    private Transform startPos;
    public Material defMat;
    public Material backMat;

    void Awake() {
        boneCollider = this.gameObject.GetComponent<Collider>();
        specPlases = new List<Transform>();
        if (this.transform.childCount!=0) { 
            foreach (Transform t in this.transform) {
                specPlases.Add(t);
            }
        }
        NameOfBone = gameObject.name;
        
        chosen = false;
        currentRender = GetComponent<Renderer>();
        defMat  = currentRender.material;
        backMat = new Material(defMat);
        makeBackMat();

        startPos = this.transform;
        setBackMaterial();
    }

    private void makeBackMat()
    {
        backMat.SetInt("_Mode",3);
        backMat.color = new Color(backMat.color.r, backMat.color.g, backMat.color.b, 0.5f);
        backMat.name = "Background";
        //backMat = new Material(backMat);
    }

    //string output = String.Format("Name {0}, Screen2World {1},posit {2},", name,screenBounds,startPos.position);
    //print(output);   
    //currentMat = 
    //print(t.name);
    //currentMat = GetComponent<Renderer>().material;
    // Start is called before the first frame update
    //print("clic");

    void Start()
    {
        boneCollider.OnMouseDownAsObservable()
            .Subscribe(s=> { 
               chosen = chosen == false ? true : false;
            });
        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                updateFoo();
                if (chosen) {
                    screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

                }
            })
            .AddTo(this);


    }

    private void updateFoo()
    {
        currentRender.enabled = false;
        currentRender.enabled = true;
    }

    public void unchek() {
        chosen = false;    
    }

    public void changeMaterial(Material mat)
    {
      //  currentMat = mat;
        currentRender.material = mat;
    }

    public void setBackMaterial()
    {
        //  currentMat = mat;
        currentRender.material = backMat;
    }
    public void setChosenMaterial()
    {
        //  currentMat = mat;
        currentRender.material = defMat;
        currentRender.enabled = true;
    }




}
