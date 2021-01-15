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
    public Material currentMat { get; private set; }

    void Awake() {
        boneCollider = this.gameObject.GetComponent<Collider>();
        //currentMat = 
        foreach (Transform t in transform) {
            specPlases.Add(t);
            //print(t.name);
        }
        chosen = false;
        currentMat = GetComponent<Renderer>().material;

    }
       // Start is called before the first frame update
    void Start()
    {
        boneCollider.OnMouseDownAsObservable()
            .Subscribe(s=> { 
               //print("clic");
               chosen = chosen == false ? true : false;
            });
    }

       

    void changeMaterial(Material mat)
    {
        currentMat = mat;
    }

}
