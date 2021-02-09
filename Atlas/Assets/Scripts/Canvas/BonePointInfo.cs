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

    public void setNumber(string s) {
        Number.text= s;
    }

    public void setName(string s) { 
        Name.text = s;
    }
    

}
