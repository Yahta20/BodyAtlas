using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour,IComparable<Bone>
{

    public MeshRenderer m_render;
    public Collider m_colider;
    Material c_material;
    Color c_color;
    public Material t_material;
    

    void Awake()
    {
        m_colider = GetComponent<Collider>();
        m_render = GetComponent<MeshRenderer>();
        c_material = m_render.material;
        c_color =c_material.color;
    }

    private void OnEnable()
    {
        Control.Instance.OnChangePoint += onChangePoint;
    }
     
    private void onMarkPoint(Transform t)
    {
        
    }

    private void onChangePoint(GameObject obj)
    {
        SetView(obj==gameObject|gameObject.transform.IsChildOf(obj.transform));
    }


    public bool IsThisBone(GameObject go) {
        return true;
    }

    private void OnMouseDown()
    {
        if (c_material == m_render.material)
        {
            Control.Instance.ChangePoint(this);
        }
    }

    private void OnMouseOver()
    {
        if (Control.Instance.Postparat!=this.gameObject)
        {
            c_material.color = Color.magenta;
        }
    }
    
    private void OnMouseExit()
    {
        c_material.color = c_color;
    }
    public void SetView(bool b)
    {
        m_render.material = b==true?c_material: t_material;
    }

    void Update()
    {
        
    }

    public void Setmaterial(Material trasperent)
    {
        t_material = trasperent;
    }

    public int CompareTo(Bone other)
    {
        return this.gameObject.name.CompareTo(other.gameObject.name)
        ;
    }
}
