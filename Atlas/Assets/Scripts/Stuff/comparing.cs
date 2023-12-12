using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class comparing : MonoBehaviour
{
    public TextAsset asset1;
    public TextAsset asset2;

    void Awake()
    {
        var s1 = asset1.text.Split('\n');
        var s2 = asset2.text.Split('\n');
        var tc = new deprivate();
        
        foreach (var s in s1)
        {
            var ss = s.Split(",");
            //print(ss[1]+ ss[2]+ss[3]+ss[4]);
            tc.trytoadd(new string[] { ss[1], ss[2], ss[3], ss[4] });
        
        }
        foreach (var s in s2)
        {
            var ss = s.Split(",");
            //print(ss[1]+ ss[2]+ ss[3]+ ss[4]);
            tc.trytoadd(new string[] { ss[1], ss[2], ss[3], ss[4] });
        }
        tc.writetext();

        
        /*
        foreach (var s in s1) 
        { 
        
        }
         */




        /*
            var ss = s.Split(",");
            var lp = new LocPoint(ss);
            if (ss[1] != ss[1]) print(ss[0]);
            //ss[1] != ss[2] ? print(ss[0]) : ;
            locs.Add(lp);
            //foreach (var v in ss)
            //{
            //
            //
            //}
         */


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class deprivate {

        public List<string> key;
        public List<string> lat;
        public List<string> eng;
        public List<string> ukr;


        public deprivate() {
            key= new  List<string>();
            lat= new  List<string>();
            eng= new  List<string>();
            ukr = new List<string>();




        }


        public void trytoadd(string[] arr) {
            //print(key.Contains(arr[0]));
            if (!key.Contains(arr[0])) 
            { 
                key.Add(arr[0]);
                lat.Add(arr[1]);
                eng.Add(arr[2]);
                ukr.Add(arr[3]);
            }
        }

        public void writetext() {
            var nameOfFile = "Result";
            nameOfFile += ".csv";
            var path = Path.Combine(Application.dataPath, nameOfFile);

            var s = "";
            s += "Id,Key,Latina(lat),English(en),Ukrainian(Ua)\n";
            for (int i = 0; i < key.Count; i++)
            {
                var temp = $"{i+1},{key[i]},{lat[i]},{eng[i]},{ukr[i]}\n";
                print (temp);
                s += temp;
            } 


            //var s = JsonUtility.ToJson(lod, true);
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
            
}