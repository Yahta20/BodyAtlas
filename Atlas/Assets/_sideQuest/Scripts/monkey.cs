using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monkey : MonoBehaviour
{

    [SerializeField]
    private AssetData assetData;

    private void OnMouseDown()
    {
        var s = $"This is {assetData.AssetName}\n It is {assetData.Description}";
        print(s);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
