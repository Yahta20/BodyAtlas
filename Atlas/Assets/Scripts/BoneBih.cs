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
    public string NameOfBones;
    private Vector2 screenBounds;
    private Transform startPos;

    void Awake() {
        boneCollider = this.gameObject.GetComponent<Collider>();
        foreach (Transform t in transform) {
            specPlases.Add(t);
        }
        chosen = false;
        currentRender = GetComponent<Renderer>();
        startPos = this.transform;
    }
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

                    string output = String.Format("Name {0}, Screen2World {1},posit {2},", name,screenBounds,startPos.position);
                    print(output);   
                }
            })
            .AddTo(this);


    }

    private void updateFoo()
    {
        
    }

    public void unchek() {
        chosen = false;    
    }

    public void changeMaterial(Material mat)
    {
      //  currentMat = mat;
        currentRender.material = mat;
        
    }

       


}
