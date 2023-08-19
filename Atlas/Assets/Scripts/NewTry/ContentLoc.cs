using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLoc : MonoBehaviour
{

    public static ContentLoc Instance { get; private set; }
    public TextAsset inputData;
    [SerializeField]
    List<LocPoint> locs = new List<LocPoint>();
    public SystemLanguage language { get; private set; } = SystemLanguage.Ukrainian;

    public event Action OnChangeLang;
    void Awake()
    {
        Instance = this;
        var sp = inputData.text.Split('\n');
        foreach (var s in sp) {

            var ss = s.Split(",");
            var lp = new LocPoint(ss);
            locs.Add(lp);
            //foreach (var v in ss)
            //{
            //
            //
            //}
        }
    }
    public string GetLocalText(string key) {

        string s;
        //chekDup(key)
        var e =locs.Find(p => p.KEY == chekDup(key)) !=null? locs.Find(p => p.KEY == chekDup(key)) :new LocPoint(); // eror from missing obj
            s = e.GetByLang(language)!=null? e.GetByLang(language):$"{key} not find";

            
        return s;
    }
    public void changeLang(SystemLanguage s) { 
        language = s;
        OnChangeLang?.Invoke();
    }
    string chekDup(string a) {
        if (
                 (  a.StartsWith("R_") |
                    a.StartsWith("L_"))
                 )
        {
            return a.Substring(2);    
        }
        return a;

    }


}

        

[Serializable]
class LocPoint {

    public string ID;
    public string KEY;
    Dictionary<SystemLanguage, string> content = new Dictionary<SystemLanguage, string>();
    public LocPoint() { }
    public LocPoint(string[] data) { 
        content = new Dictionary<SystemLanguage, string>();
        ID = data[0];
        KEY = data[1];
        content.Add(SystemLanguage.Unknown, data[2]);
        content.Add(SystemLanguage.English, data[3]);
        content.Add(SystemLanguage.Ukrainian, data[4]);




    }
    public string GetByLang(SystemLanguage sl) {
        string ret;
        if (content.TryGetValue(sl,out ret))
        {
            return ret;
        }
        return "ERorR";//content[sl];
    }

    public string GetByKey(string key)
    {
        
        return "ERorR";//content[sl];
    }

}