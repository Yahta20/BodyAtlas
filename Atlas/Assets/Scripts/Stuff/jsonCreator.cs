using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class ObjData {

        public string name;

        public string parent;

        public float[] position;
        public float[] rotation;
        public float[] scale;
        public int HashCode;
        public bool spawn = false;
        
        public ObjData(
            string n,
            Transform t
            ) {

            name = n;
           
            position = new float[3] 
            { t.position.x, t.position.y, t.position.z};
            
            rotation = new float[4] 
            {  t.rotation.x, t.rotation.y, t.rotation.z,t.rotation.w };
            
            scale = new float[3] 
            { t.localScale.x, t.localScale.y, t.localScale.z };
        }

        public void UpdateParentScale(Vector3 scl) {
            scale[0] *= scl[0];
            scale[1] *= scl[1];
            scale[2] *= scl[2];
        
        }

    public bool comparison(Transform t) {

        if (position[0] == t.position.x &
            position[1] == t.position.y &
            position[2] == t.position.z &
            rotation[0] == t.rotation.x &
            rotation[1] == t.rotation.y &
            rotation[2] == t.rotation.z &
            rotation[3] == t.rotation.w &
            name == t.gameObject.name
            
            )
        {
            if (null != t.gameObject.GetComponentInParent<GameObject>())
            {
                if (parent == t.gameObject.GetComponentInParent<GameObject>().name
                    )
                {
                    return true;
                }
            }
            return true;
        }


        return false;
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

    public ObjData GetObjByNam(string s)
    {
        foreach (var item in saveList)
        {
            if (item.name == s)
            {
                return item;
            }
        }
        return null;
    }

    public ObjData GetObjByPar(string s) {
        foreach (var item in saveList)
        {
            if (item.parent ==  s)
            {
                return item;
            }
        }
        return null;
    }

    public int getChildCount(string s) { 
        int chc = 0;
        foreach (var item in saveList)
        {
            if (item.parent == s)
            {
                chc++;
            }
        }
        return chc;
    }

    public ObjData getByHashCode(int i)
    {
        foreach (var item in saveList)
        {
            if (item.HashCode == i)
            {
                return item;
            }
        }
        return null;
    }

    public ObjData[] FindArrByName(string name)
    {
        var list = new List<ObjData>();
        foreach (var item in saveList)
        {
            if (item.name == name)
            {
                list.Add(item);
            }
        }
        return list.ToArray();
    }
}






public class jsonCreator : MonoBehaviour
{
    public GameObject ObjectToSave;
    public string nameOfFile;
    public listOfData lod = new listOfData();
    void Start()
    {
        nameOfFile = nameOfFile == "" ? "data" : nameOfFile;
        nameOfFile += ".json";
        var path = Path.Combine(Application.dataPath, nameOfFile);

        lod = new listOfData();
        List<ObjData> PrepData = new List<ObjData>();

        var og = new ObjData(
            "root",
            ObjectToSave.transform
            );
        og.parent = "main";
        PrepData.Add(og);
        foreach (Transform item in ObjectToSave.transform)
        {
            var od = new ObjData(
            item.gameObject.name,
            item
            );
            od.parent = "root";
            var scl = item.transform.lossyScale;
            od.UpdateParentScale(scl);
            PrepData.Add(od);
            foreach (Transform subitem in item)
            {
                var odd = new ObjData(
                    subitem.gameObject.name,
                    subitem
                    );
                odd.parent = item.gameObject.name;
            
                var sscl = subitem.transform.lossyScale;
                odd.UpdateParentScale(scl);
                PrepData.Add(odd);
            }
        }
        lod.saveListcreaton(PrepData);



        var s = JsonUtility.ToJson(lod,true);
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream fileStream = new FileStream(path,
                                       FileMode.OpenOrCreate,
                                       FileAccess.ReadWrite,
                                       FileShare.None);
            if (fileStream.CanWrite)
            {
                byte[] arr = System.Text.Encoding.Default.GetBytes(s);
                fileStream.Write(arr, 0, arr.Length);
            }
            fileStream.Close();
            print("fin");
        }
        catch (System.Exception e)
        {
            print($"Pizda togo sho {e.ToString()}");
        }
    }
}       
