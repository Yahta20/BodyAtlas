using UnityEngine;


[CreateAssetMenu(fileName ="New Asset",menuName="Asset Data",order =(50*0)+1)]
public class AssetData : ScriptableObject
{
    [SerializeField]
    private string assetName;
    [SerializeField]
    private string description;
    
    public string AssetName { get { return assetName; } }
    public string Description { get { return description; } }
}