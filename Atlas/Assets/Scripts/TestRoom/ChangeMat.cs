using UnityEngine;




public class ChangeMat : MonoBehaviour
{
    MeshFilter meshFilter;

    [ContextMenu("ctep")]
    void Change2Next() {
        if (meshFilter != null) { meshFilter = GetComponent<MeshFilter>(); }
        print(meshFilter.mesh.subMeshCount);
        
    
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
