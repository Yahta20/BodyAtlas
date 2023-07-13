using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BoneList {

    public bones[] BONES;
    public string FindBone(string BoneName)
    {
        foreach (var item in BONES)
        {
            if (BoneName == item.lat)
            {
                switch (GameEnviroment.Singelton.languageInfo)
                {
                    case Lang.lat:
                        return item.lat;
                        
                    case Lang.ua:
                        return item.ua;
                        
                    case Lang.ru:
                        return item.ru;
                       
                    case Lang.en:
                        return item.en;
                        
                    default:
                        break;
                }
            }
        }
        return "none";
    }
    public bones getBone(string name) {
        foreach (var item in BONES)
        {
            if (item.lat == name)
            {
                return item;
            }
        }
        return null;
    }
    public void fillAllDict() {
        foreach (var bone in BONES)
        {
            bone.fillName();
            foreach (var p in bone.point)
            {
                p.fillName();
            }
        }
    }
    public Dictionary<Lang, string> getBoneDic(string name)
    {
        foreach (var item in BONES)
        {
            if (name == item.lat)
            {
                return item.bonesDic;
            }
        }
        return null;
    }
    public Dictionary<Lang, string> getPointDic(string name)
    {
        foreach (var item in BONES)
        {
            foreach (var it in item.point)
            {
                if (name == it.lat)
                {
                    return it.pointDic;
                }
            }
        }
        return null;
    }
    public int getBNomber(string bName) {
        int nomb = 0;

        foreach (var bone in BONES)
        {
            if (bone.lat == bName)
            {
                return nomb;
            }
            nomb++;
        }
        return nomb; 
    }

   
}
    
[System.Serializable]
public class bones
{
    public string lat;
    public string ua ;
    public string en ;
    public string ru ;

    public Point[] point;
    public Dictionary<Lang, string> bonesDic {get;private set;} = new Dictionary<Lang, string>();
    public string[] getLatPoints() {
        var l = new List<string>();
        foreach (var item in point)
        {
            l.Add(item.lat);
        }
        return l.ToArray();
    }
    public void fillName() {  
        bonesDic.Add(Lang.lat, lat);
        bonesDic.Add(Lang.ua,  ua );
        bonesDic.Add(Lang.en,  en );
        bonesDic.Add(Lang.ru,  ru );
    }
    public string FindPoint(string PointName)
    {
            foreach (var item in point)
            {
                if (PointName == item.lat)
                {
                    switch (GameEnviroment.Singelton.languageInfo)
                    {
                        case Lang.lat:
                            return item.lat;

                        case Lang.ua:
                            return item.ua;

                        case Lang.ru:
                            return item.ru;

                        case Lang.en:
                            return item.en;

                        default:
                            break;
                    }
                }
            }
        return "none";
    }
}

public sealed class LangEnv : MonoBehaviour
{
    public static LangEnv Instance;
    public TextAsset inputData;
    [Space]
    public BoneList currentBone = new BoneList();

    public string[] ignoreList { get; private set; } 
        = new string[] { "spatia interossea metatarsi", "osseus" };

    public static LangEnv Singelton
    {
        get
        {
            if (Instance == null)
            {
                Instance = new LangEnv();
            }
            return Instance;
        }
    }

    void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        currentBone = JsonUtility.FromJson<BoneList>(inputData.text);
        currentBone.fillAllDict();
    }

    public bool ChekIgnore(string s) {

        foreach (var item in ignoreList)
        {
            if (s==item)
            {
                return false;
            }
        }
        return true;
    }

}