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
    public Renderer currentRender { get; private set; }
    public string NameOfBone;
    public Material defMat;
    public Material backMat;

    private Quaternion startRot;
    private Vector2 screenBounds;
    private Transform startPos;


    void Awake()
    {
        boneCollider = this.gameObject.GetComponent<Collider>();
        specPlases = new List<Transform>();
        startRot = gameObject.transform.rotation;
        if (this.transform.childCount != 0)
        {
            foreach (Transform t in this.transform)
            {
                specPlases.Add(t);
            }
        }

        NameOfBone = gameObject.name;
        chosen = false;
        currentRender = GetComponent<Renderer>();
        defMat = currentRender.material;
        backMat = new Material(defMat);
        makeBackMat();

        startPos = this.transform;
        setBackMaterial();
    }

    void Start()
    {


        //var mov = Moving.Instance;
        //mov.GetCollider
        //    .Where(c =>c!=null )
        //    .Subscribe(s => {
        //        if ( s==boneCollider) //s.gameObject.layer == 8 &
        //        {
        //            chosen = chosen == false ? true : false;
        //
        //            if (chosen)
        //            {
        //                GameManager.Instance.setCurrentBone(gameObject.name);
        //            }
        //            else
        //            {
        //
        //                topPanel.Instance.upHierarchy();
        //            }
        //            rightPanel.Instance.init = false;
        //        }
        //    }
        //).AddTo(this);




        boneCollider.OnMouseDownAsObservable()
            .Subscribe(s => {
                chosen = chosen == false ? true : false;

                if (chosen)
                {
                    GameManager.Instance.setCurrentBone(gameObject.name, gameObject.transform.GetInstanceID());
                }
                else
                {
                    topPanel.Instance.upHierarchy();
                }
                rightPanel.Instance.init = false;
            });
        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                updateFoo();
            })
            .AddTo(this);
    }

    public Vector3 getPoint(string s)
    {
        foreach (Transform item in transform)
        {
            if (item.gameObject.name == s)
            {
                return item.position;
            }
        }
        return Vector3.zero;
    }




    private void makeBackMat()
    {
        backMat.SetInt("_Mode", 3);
        backMat.color = new Color(backMat.color.r, backMat.color.g, backMat.color.b, 0.5f);
        backMat.name = "Background";
        backMat = new Material(backMat);
    }

    private void updateFoo()
    {
        currentRender.enabled = false;
        currentRender.enabled = true;
    }

    public void setStartRot()
    {
        gameObject.transform.rotation = startRot;
    }

    public void setChek()
    {
        chosen = true;
    }
    public void unchek()
    {
        chosen = false;
    }

    public void changeMaterial(Material mat)
    {
        currentRender.material = mat;
    }

    public void setBackMaterial()
    {
        currentRender.material = backMat;
    }

    public void setChosenMaterial()
    {
        currentRender.material = defMat;
        currentRender.enabled = true;
    }
}






