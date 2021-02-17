using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class BonePointInfo : MonoBehaviour
{
    public Text Number;
    public Text Name;
    public Dictionary<Lang, string> TransLang {get; private set;}

    public void setNumber(string s) {
        Number.text= s;
    }

    public void setName(string s) { 
        Name.text = s;
        gameObject.name = s;
    }
    public void setGoName(string s)
    {
        //Name.text = s;
        gameObject.name = s;
    }
    public void setTranslate(Dictionary<Lang, string> d) {
        TransLang = d;
    }

}
