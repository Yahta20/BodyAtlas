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
    public Renderer currentRender { get; private set; }

    void Awake() {
        boneCollider = this.gameObject.GetComponent<Collider>();
        //currentMat = 
        foreach (Transform t in transform) {
            specPlases.Add(t);
            //print(t.name);
        }
        chosen = false;
        //currentMat = GetComponent<Renderer>().material;
        currentRender = GetComponent<Renderer>();
    }
       // Start is called before the first frame update
    void Start()
    {
        boneCollider.OnMouseDownAsObservable()
            .Subscribe(s=> { 
               //print("clic");
               chosen = chosen == false ? true : false;
            });
        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                updateFoo();

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
