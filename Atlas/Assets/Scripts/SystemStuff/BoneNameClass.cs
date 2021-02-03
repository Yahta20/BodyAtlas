using System;
using System.Collections.Generic;

public class BoneNameClass 
{
    public string latName;
    public string uaName;
    public string enName;
    public string ruName;

    public List<string> latdotsOfBones=new List<string>();
    public List<string> rudotsOfBones =new List<string>();
    public List<string> endotsOfBones =new List<string>();
    public List<string> uadotsOfBones =new List<string>();


    public BoneNameClass(string ilatName,
                         string iuaName,
                         string ienName,
                         string iruName
        ) 
    {
        latName= ilatName;
        uaName = iuaName;
        enName = ienName;
        ruName = iruName;
    }

    public void appendPoint(string ilatPoint,
                            string  iuaPoint,
                            string  ienPoint,
                            string  iruPoint)
    {
        latdotsOfBones.Add(ilatPoint);
        rudotsOfBones.Add(iuaPoint);
        endotsOfBones.Add(ienPoint);
        uadotsOfBones.Add(iruPoint);
    } 

}
