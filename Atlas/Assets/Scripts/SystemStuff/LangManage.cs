using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using Newtonsoft.Json.Linq;
using UniRx;
//using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public sealed class LangManage : MonoBehaviour
{
    public static LangManage instance;
    public Lang currentLang;
    public LangUI currentUILang;
    public List <BoneNameClass> bones {get; private set;}
    public TextAsset inputData;
    //private JObject json2work;

    private void Awake()
    {

        instance = this;
        
        bones = new List<BoneNameClass>();
        string jsonRaw = inputData.ToString();
        //json2work = JObject.Parse(jsonRaw);
        //int numberOfBones = json2work["BONES"].Count();
        /*
        for (int i=0; i< json2work["BONES"].Count();i++) {
            var lat=json2work["BONES"][i]["lat"].Value<string>();
            var ua =json2work["BONES"][i]["ua"] .Value<string>();
            var en =json2work["BONES"][i]["en"] .Value<string>();
            var ru =json2work["BONES"][i]["ru"] .Value<string>();
            var bonvar = new BoneNameClass(lat,ua,en,ru);
            for (int p = 0; p < json2work["BONES"][i]["point"].Count(); p++) {
                var plat = json2work["BONES"][i]["point"][p]["lat"].Value<string>();
                var pua =  json2work["BONES"][i]["point"][p]["ua"] .Value<string>();
                var pen =  json2work["BONES"][i]["point"][p]["en"] .Value<string>();
                var pru =  json2work["BONES"][i]["point"][p]["ru"] .Value<string>();
                bonvar.appendPoint(
                    plat,
                    pua ,
                    pen ,
                    pru);
            }
            bones.Add(bonvar);
        }
         */
        //print(bones.Count);
        //print(bones[1].getCountOfPoints());
        //print(bones[1].getNameOfBone());
        //print(bones[1].getNameOfPoint(3));
        
    }

    public string FindBone(string BoneName) {
        foreach (var item in bones)
        {
            if (BoneName==item.latName) {
                return item.Name[GameEnviroment.Singelton.languageInfo];
            }
        }
        return "none";
    }
        

    public string FindBone4Test(string BoneName)
    {
        foreach (var item in bones)
        {
            if (BoneName == item.latName)
            {
                return item.Name[GameEnviroment.Singelton.languageInfo];
            }
            if (BoneName == item.uaName)
            {
                return item.Name[GameEnviroment.Singelton.languageInfo];
            }
            if (BoneName == item.ruName)
            {
                return item.Name[GameEnviroment.Singelton.languageInfo];
            }
            if (BoneName == item.enName)
            {
                return item.Name[GameEnviroment.Singelton.languageInfo];
            }
        }

        return "none";
    }

    public string FindPoint(string PointName)
    {
        foreach (var bone in bones)
        {

            foreach (var point in bone.Points)
            {
                
                if (PointName == point[Lang.lat])
                {
                    return point[GameEnviroment.Singelton.languageInfo];
                }

            }
        }
        return "none";
    }

    public string FindPoint4Test(string PointName)
    {
        foreach (var bone in bones)
        {
            foreach (var point in bone.Points)
            {
                if (PointName == point[Lang.lat])
                {
                    return point[GameEnviroment.Singelton.languageInfo];
                }
                if (PointName == point[Lang.en])
                {
                    return point[GameEnviroment.Singelton.languageInfo];
                }
                if (PointName == point[Lang.ua])
                {
                    return point[GameEnviroment.Singelton.languageInfo];
                }
                if (PointName == point[Lang.ru])
                {
                    return point[GameEnviroment.Singelton.languageInfo];
                }
            }
        }
        return "none";
    }

    public Dictionary<Lang, string> FindBoneDic(string BoneName)
    {

    
        foreach (var item in bones)
        {
            if (BoneName == item.Name[Lang.lat])
            {
                return item.Name;
            }
        }

        return new Dictionary<Lang, string>() ;
    }

    public Dictionary<Lang, string> FindPointDic(string PointName)
    {
        foreach (var bone in bones)
        {
            foreach (var point in bone.Points)
            {
                if (PointName == point[Lang.lat])
                {
                    return point;
                }
            }
        }
        return new Dictionary<Lang, string>(); 
    }

}