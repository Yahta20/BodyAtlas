using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class BoneBih : MonoBehaviour
{
    //public Material currentMat { get; private set; }
    public Collider boneCollider;
    public List<Transform> specPlases { get; private set; }
    public bool chosen { get; private set; }
    public Renderer currentRender { get; private set;}
    public string NameOfBone;
    public Material defMat;
    public Material backMat;

    private Quaternion startRot;
    private Vector2 screenBounds;
    private Transform startPos;
    

    void Awake() {
        boneCollider = this.gameObject.GetComponent<Collider>();
        specPlases = new List<Transform>();
        startRot = gameObject.transform.rotation;
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

        //print(transform.transform.name);
    }


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



    private void makeBackMat()
    {
        backMat.SetInt("_Mode",3);
        backMat.color = new Color(backMat.color.r, backMat.color.g, backMat.color.b, 0.5f);
        backMat.name = "Background";
        backMat = new Material(backMat);
    }

    private void updateFoo()
    {
        currentRender.enabled = false;
        currentRender.enabled = true;
    }

    public void setStartRot() {
        gameObject.transform.rotation = startRot;
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
        //currentRender.
    }

    public void setChosenMaterial()
    {
        //  currentMat = mat;
        currentRender.material = defMat;
        currentRender.enabled = true;
    }





}
