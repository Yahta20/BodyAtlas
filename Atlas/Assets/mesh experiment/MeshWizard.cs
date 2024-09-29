using UnityEngine;

public class MeshWizard : MonoBehaviour
{

    public float explosionRadius = 5.0f;
    public MeshFilter meshFilter;
    public Mesh mesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
//        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Info")]
    void Meshinfo() {
        print($"{mesh.bounds} | {mesh.triangles.Length}");
    }


    private void OnDrawGizmos()

    {
        if (meshFilter==null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }


        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        foreach (var item in meshFilter.mesh.vertices)
        {
            Gizmos.DrawSphere(item, explosionRadius);
        }


    }




}
