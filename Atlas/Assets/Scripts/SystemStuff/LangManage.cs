using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UniRx;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class LangManage : MonoBehaviour
{
    public static LangManage instance;
    public Lang currentLang;
    public LangUI currentUILang;
    public List <BoneNameClass> bones = new List<BoneNameClass>();
    public TextAsset inputData;
    private JObject json2work;

    private void Awake()
    {
        instance = this;
        string jsonRaw = inputData.ToString();
        json2work = JObject.Parse(jsonRaw);
        int numberOfBones = json2work["BONES"].Count();
        for (int i=0; i< json2work["BONES"].Count();i++) {
            var lat=json2work["BONES"][i]["lat"].Value<string>();
            var ua =json2work["BONES"][i]["ua"] .Value<string>();
            var en =json2work["BONES"][i]["en"] .Value<string>();
            var ru =json2work["BONES"][i]["ru"] .Value<string>();
            var bonvar = new BoneNameClass(
                lat,
                ua ,
                en ,
                ru);
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
    }









        


    
}
