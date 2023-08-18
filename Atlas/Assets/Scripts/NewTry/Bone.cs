using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{

    public MeshRenderer m_render;
    public Collider m_colider;
    Material c_material;
    public Material t_material;
    

    void Awake()
    {
        m_colider = GetComponent<Collider>();
        m_render = GetComponent<MeshRenderer>();
        c_material = m_render.material;
    }

    private void OnEnable()
    {
        Control.Instance.OnChangePoint += onChangePoint;
        //Control.Instance.OnMarkPoint += onMarkPoint;

    }

    private void onMarkPoint(Transform t)
    {
        
    }

    private void onChangePoint(GameObject obj)
    {
        SetView(obj==gameObject|gameObject.transform.IsChildOf(obj.transform));
    }
        



    private void OnMouseDown()
    {
        Control.Instance.ChangePoint(this);
    }
        

    public void SetView(bool b)
    {
        m_render.material = b==true?c_material: t_material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setmaterial(Material trasperent)
    {
        t_material = trasperent;
    }
}
