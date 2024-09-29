using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public struct IntrBtn: IPointerEnterHandler,IPointerExitHandler{
    public Text label    {get; private set;}
    public Button  button{get; private set;}

    public UnityAction enter{get; private set;}
    public UnityAction exit { get; private set; }


    public IntrBtn(Text x, Button y,UnityAction en, UnityAction ex)
    {
        label= x;
        button= y;
        enter = en;
        exit = ex;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        exit?.Invoke();
        UnityEngine.MonoBehaviour.print("dert");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        enter?.Invoke();
        UnityEngine.MonoBehaviour.print("dart");
    }
    public void On(Text t)
    {
        label = t;
    }


}
    


