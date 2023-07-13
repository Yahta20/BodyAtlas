using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageModul : MonoBehaviour
{
    public static LanguageModul Instance { get; private set; }
    public TextAsset Langpack;
    public LangSet content;

    void Awake()
    {
        Instance = this;
        content = JsonUtility.FromJson<LangSet>(Langpack.text);
        content.fillAllDict();
    }
}
public class LangSet {

    public Point[] BONES;
    public void fillAllDict()
    {
        foreach (var bone in BONES)
        {
            bone.fillName();
        }
    }


}