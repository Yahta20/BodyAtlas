using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class ObjData {

        public string name;
        public float[] position;
        public float[] rotation;
        public float[] scale;
        
        public ObjData(
            string n,
            Transform t
            ) {

            name = n;

            position = new float[3] 
            { t.localPosition.x,t.localPosition.y, t.localPosition.z };
            
            rotation = new float[4] 
            { t.rotation.w, t.rotation.x, t.rotation.y, t.rotation.z };
            
            scale = new float[3] 
            { t.localScale.x, t.localScale.y, t.localScale.z };

        }
    }

[System.Serializable]
public class listOfData {
    public ObjData[] saveList;
    
    public void saveListcreaton(List<ObjData> od) {

        saveList = new ObjData[od.Count];
    
        saveList = od.ToArray();
    
    }

    public Vector3 GetPosition(string name) {
        foreach (var item in saveList)
        {
            if (item.name==name)
            {
                return new Vector3(item.position[0], item.position[1], item.position[2]); 
            }
        }
        return Vector3.zero;
    }
    public Quaternion GetRotation(string name)
    {
        foreach (var item in saveList)
        {
            if (item.name == name)
            {
                return new Quaternion(item.rotation[0], item.rotation[1], item.rotation[2], item.rotation[3]);
            }
        }
        return Quaternion.identity;
    }
    public Vector3 GetScale(string name)
    {
        foreach (var item in saveList)
        {
            if (item.name == name)
            {
                return new Vector3(item.scale[0], item.scale[1], item.scale[2]);
            }
        }
        return Vector3.one;
    }
}
    


        


public class jsonCreator : MonoBehaviour
{
    public GameObject ObjectToSave;
    public string nameOfFile;
    
    void Start()
    {
        nameOfFile = nameOfFile == "" ? "data" : nameOfFile;
        nameOfFile += ".json";
        var path = Path.Combine(Application.dataPath, nameOfFile);

        var lod = new listOfData();
        List<ObjData> eee = new List<ObjData>();
        for (int i = 0; i < ObjectToSave.transform.childCount; i++)
        {
            var od = new ObjData(
            ObjectToSave.transform.GetChild(i).gameObject.name,
            ObjectToSave.transform.GetChild(i)
            );
            eee.Add(od);
            //lod.saveList.Add(od);
        }
        lod.saveListcreaton(eee);
        var s = JsonUtility.ToJson(lod,true);

        try
        {
            File.WriteAllText(path,s);
        }
        catch (System.Exception e)
        {
            print($"Pizda togo sho {e.ToString()}");
        }
            

    }
}       