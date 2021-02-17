using System;
using System.Collections.Generic;

public class BoneNameClass 
{
    public string latName { get; private set; }
    public string uaName  { get; private set; }
    public string enName  { get; private set; }
    public string ruName  { get; private set; }

    public List<string> latdotsOfBones=new List<string>();
    public List<string> rudotsOfBones =new List<string>();
    public List<string> endotsOfBones =new List<string>();
    public List<string> uadotsOfBones =new List<string>();

    public Dictionary<Lang, string> Name { get; private set; }

    public List<Dictionary<Lang, string>> Points { get; private set; }

    public BoneNameClass(string ilatName,
                         string iuaName,
                         string ienName,
                         string iruName
        ) 
    {
        Name = new Dictionary<Lang, string>();
        Points = new List<Dictionary<Lang, string>>();


        latName= ilatName;
        uaName = iuaName;
        enName = ienName;
        ruName = iruName;
        
        Name.Add(Lang.lat,latName);
        Name.Add(Lang.ua, uaName);
        Name.Add(Lang.en, enName);
        Name.Add(Lang.ru, ruName);

    }

    public void appendPoint(string ilatPoint,
                            string  iuaPoint,
                            string  ienPoint,
                            string  iruPoint)
    {
        latdotsOfBones  .Add(ilatPoint);
        rudotsOfBones   .Add(iuaPoint);
        endotsOfBones   .Add(ienPoint);
        uadotsOfBones   .Add(iruPoint);

        var dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, ilatPoint);
        dictPage.Add(Lang.ua,   iuaPoint);
        dictPage.Add(Lang.en,   ienPoint);
        dictPage.Add(Lang.ru,   iruPoint);
        Points.Add(dictPage);
    }

    public int getCountOfPoints() {
        return latdotsOfBones.Count;  
    }

    //public string getNameOfBone() {
    //    return latName;
    //}

    public string getNameOfPoint(int Num){
        return latdotsOfBones[Num];
    }

}
