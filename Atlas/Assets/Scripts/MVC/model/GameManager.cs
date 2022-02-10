using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lang
{
    lat = 0,
    ua = 1,
    ru = 2,
    en = 3,
};
public enum LangUI
{
    ua = 0,
    eng = 1,
    ru = 2
};
public enum stateOfChose {
    Partition = 0,
    Subpartitions = 1,
    Item=2,
    ItemPoints=3
}
[System.Serializable]
public class studyStruct {
    public string[] Title;
    public studySubgect[] AnatomyPart;

    public Dictionary<Lang, string> TitleDic { get; private set; } = new Dictionary<Lang, string>();
    public void fillName()
    {
        TitleDic.Add(Lang.lat, Title[0]);
        TitleDic.Add(Lang.ua,  Title[1]);
        TitleDic.Add(Lang.ru,  Title[2]);
        TitleDic.Add(Lang.en,  Title[3]);
    }
    public string[] GetAllPart() {
        var list = new List<string>();
        foreach (var item in AnatomyPart)
        {
            list.Add(item.Partition);
        }
        return list.ToArray();
    }
    public studySubgect GetSubgect(string name) {  
        foreach (var item in AnatomyPart)
        {
            if (item.Partition == name)
            {
                return item;
            } 
        }
        return null;
    }
    public void fillAllTitle() {
        fillName();
        foreach (var item in AnatomyPart)
        {
            item.fillName();
            foreach (var sub in item.Subpartitions)
            {
                sub.fillName();
            }
        }
    }
    public Dictionary<Lang, string> getSubgectDic(string name) {
        foreach (var item in AnatomyPart)
        {
            if (name == item.Partition)
            {
                return item.TitleDic;
            }
        }
        return null;
    }
    public Dictionary<Lang, string> getSubPartDic(string name)
    {
        foreach (var item in AnatomyPart)
        {
            if (GameManager.Instance.currentPartition == item.Partition)
            {
                foreach (var sub in item.Subpartitions)
                {
                    if (sub.Name==name)
                    {
                        return sub.TitleDic;
                    }
                }
            }
        }
        return null;
    }
    public string[] getItemlist(string s) {
        if (GameManager.Instance.currentPartition == "")
        {
            return null;
        }
        var sts     = new studySubgect();
        var subpart = new subpartitions();
        foreach (var item in AnatomyPart)
        {
            if (GameManager.Instance.currentPartition == item.Partition)
            {
                sts = item;
                break;
            }
        }
        foreach (var item in sts.Subpartitions)
        {
            if (item.Name == s)
            {
                return item.ItemList;
            }
        }
        return null;
    }
    public string[] getBoneState(string name) {
        var retlist = new List<string>();
        foreach (var part in AnatomyPart)
        {
            foreach (var Subpart in part.Subpartitions)
            {
                foreach (var item in Subpart.ItemList)
                {
                    if (item==name)
                    {
                        retlist.Add(part.Partition);
                        retlist.Add(Subpart.Name);
                        retlist.Add(name);
                        return retlist.ToArray();
                    }
                }
            }
        }
        return retlist.ToArray(); 
    }
}

[System.Serializable]
public class studySubgect
{
    public string[] Title;
    public string Partition;
    public subpartitions[] Subpartitions;

    public Dictionary<Lang, string> TitleDic { get; private set; } 
        = new Dictionary<Lang, string>();
    public void fillName()
    {
        TitleDic.Add(Lang.lat, Title[0]);
        TitleDic.Add(Lang.ua, Title[1]);
        TitleDic.Add(Lang.ru, Title[2]);
        TitleDic.Add(Lang.en, Title[3]);
    }
    public string[] GetAllPart()
    {
        var list = new List<string>();
        foreach (var item in Subpartitions)
        {
            list.Add(item.Name);
        }
        return list.ToArray();
    }
    public subpartitions GetSubgect(string name)
    {

        foreach (var item in Subpartitions)
        {
            if (item.Name == name)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class subpartitions
{
    public string[] Title;
    public string Name;
    public string[] ItemList;

    public Dictionary<Lang, string> TitleDic { get; private set; } = new Dictionary<Lang, string>();
    public void fillName()
    {
        TitleDic.Add(Lang.lat,Title[0]);
        TitleDic.Add(Lang.ua, Title[1]);
        TitleDic.Add(Lang.ru, Title[2]);
        TitleDic.Add(Lang.en, Title[3]);
    }
    public string[] GetAllPart()
    {
        var list = new List<string>();
        foreach (var item in ItemList)
        {
            list.Add(item);
        }
        return list.ToArray();
    }
    public string GetSubgect(string name)
    {
        foreach (var item in ItemList)
        {
            if (item == name)
            {
                return item;
            }
        }
        return null;
    }
}

public class GameManager : MonoBehaviour
{
    public Lang currentLang { get; private set; }
    public LangUI UILang { get; private set; }
    public stateOfChose currentChose { get; private set; }


    [Space]
    public static GameManager Instance;
    public TextAsset inputData;

    public studyStruct anatomy = new studyStruct();

    public string currentPartition = "";
    public string currentSubpartitions = "";
    public string currentItem = "";
    public string currentItemPoints = "";

    public int GOID;

    public static GameManager Singelton
    {
        get
        {
            if (Instance == null)
            {
                Instance = new GameManager();
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
        anatomy = JsonUtility.FromJson<studyStruct>(inputData.text);
        anatomy.fillAllTitle();
        currentLang = Lang.lat;
        UILang = LangUI.ua;
        currentChose = stateOfChose.Partition;
    }
    public string[] getState() {
        return new string[4] {
            currentPartition    ,
            currentSubpartitions,
            currentItem         ,
            currentItemPoints
        };
    }

    public stateOfChose getNextState() {
        switch (currentChose)
        {
            case stateOfChose.Partition:
                return stateOfChose.Subpartitions;
                break;
            case stateOfChose.Subpartitions:
                return stateOfChose.Item;
                break;
            case stateOfChose.Item:
                return stateOfChose.ItemPoints;
                break;
            case stateOfChose.ItemPoints:
                return stateOfChose.ItemPoints;
                break;
        }
        return currentChose;
    }
    public void setState(stateOfChose s) {
        currentChose = s;
    }
    void FixedUpdate()
    {
        if (currentPartition == "")
        {
            currentChose = stateOfChose.Partition;
        }
        else {
            if (currentSubpartitions =="")
            {
                currentChose = stateOfChose.Subpartitions;
            }
            else
            {
                if (currentItem == "")
                {
                    currentChose = stateOfChose.Item;
                }
                else {
                    currentChose = stateOfChose.ItemPoints;
                }
            }
        }
    }
    public void setLanguage(int i)
    {
        int b = i % 4;
        switch (b)
        {
            case (0):
                currentLang = Lang.lat;
                break;
            case (1):
                currentLang = Lang.ua;
                break;
            case (2):
                currentLang = Lang.ru;
                break;
            case (3):
                currentLang = Lang.en;
                break;
        }
    }
    public int getLanguage()
    {
        switch (currentLang)
        {
            case (Lang.lat):
                //languageInfo = Lang.lat;
                return 0;
                break;
            case (Lang.ua):
                //languageInfo = Lang.ua;
                return 1;
                break;
            case (Lang.ru):
                //languageInfo = Lang.ru;
                return 2;
                break;
            case (Lang.en):
                //languageInfo = Lang.en;
                return 3;
                break;
            default:
                currentLang = Lang.lat;
                return 0;
        }
    }
    public void resetData() {
        currentPartition = "";
        currentSubpartitions = "";
        currentItem = "";
        currentItemPoints = "";
        GOID = 0;
        currentChose = stateOfChose.Partition;
    }

    public void setCurrentBone(string name,int id) {
        var newState =  anatomy.getBoneState(name);
        currentPartition        = newState[0];
        currentSubpartitions    = newState[1];
        currentItem             = newState[2];
        currentItemPoints = "";
        GOID = id;
    }
}




    
