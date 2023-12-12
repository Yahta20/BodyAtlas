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
            //print (ss.Length);
            var lp = new LocPoint(ss);
            if (ss[1] != ss[1]) print(ss[0]);
            //ss[1] != ss[2] ? print(ss[0]) : ;
            locs.Add(lp);

        }
    }
    public string GetLocalText(string key) {

        string s;
        //chekDup(key)
        var e =locs.Find(p => p.KEY == chekDup(key)) !=null? locs.Find(p => p.KEY == chekDup(key)) :new LocPoint(); // eror from missing obj
            s = e.GetByLang(language)!=null? e.GetByLang(language):$"{key} not find";

            
        return s;
    }
    public void changeLang() {

        switch (language)
        {
            case SystemLanguage.Afrikaans:
                break;
            case SystemLanguage.Arabic:
                break;
            case SystemLanguage.Basque:
                break;
            case SystemLanguage.Belarusian:
                break;
            case SystemLanguage.Bulgarian:
                break;
            case SystemLanguage.Catalan:
                break;
            case SystemLanguage.Chinese:
                break;
            case SystemLanguage.Czech:
                break;
            case SystemLanguage.Danish:
                break;
            case SystemLanguage.Dutch:
                break;
            case SystemLanguage.English:
                language = SystemLanguage.Unknown;
                break;
            case SystemLanguage.Estonian:
                break;
            case SystemLanguage.Faroese:
                break;
            case SystemLanguage.Finnish:
                break;
            case SystemLanguage.French:
                break;
            case SystemLanguage.German:
                break;
            case SystemLanguage.Greek:
                break;
            case SystemLanguage.Hebrew:
                break;
            case SystemLanguage.Icelandic:
                break;
            case SystemLanguage.Indonesian:
                break;
            case SystemLanguage.Italian:
                break;
            case SystemLanguage.Japanese:
                break;
            case SystemLanguage.Korean:
                break;
            case SystemLanguage.Latvian:
                break;
            case SystemLanguage.Lithuanian:
                break;
            case SystemLanguage.Norwegian:
                break;
            case SystemLanguage.Polish:
                break;
            case SystemLanguage.Portuguese:
                break;
            case SystemLanguage.Romanian:
                break;
            case SystemLanguage.Russian:
                break;
            case SystemLanguage.SerboCroatian:
                break;
            case SystemLanguage.Slovak:
                break;
            case SystemLanguage.Slovenian:
                break;
            case SystemLanguage.Spanish:
                break;
            case SystemLanguage.Swedish:
                break;
            case SystemLanguage.Thai:
                break;
            case SystemLanguage.Turkish:
                break;
            case SystemLanguage.Ukrainian:
                language = SystemLanguage.English;      
                break;
            case SystemLanguage.Vietnamese:
                break;
            case SystemLanguage.ChineseSimplified:
                break;
            case SystemLanguage.ChineseTraditional:
                break;
            case SystemLanguage.Unknown:
                language = SystemLanguage.Ukrainian;
                break;
            case SystemLanguage.Hungarian:
                break;
            default:
                break;
        }
        OnChangeLang?.Invoke();
    }


        //language = s;
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
        content.Add(SystemLanguage.Unknown,   data[2]);
        content.Add(SystemLanguage.English,   data[3]);
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