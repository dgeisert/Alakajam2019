using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateIsland : MonoBehaviour
{

    public class Tri
    {
        public int a, b, c;
        public Tri(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public bool Contains(int i)
        {
            return a == i || b == i || c == i;
        }
    }
    public List<Vector3> vertsSplats;
    public List<Vector2> uvsSplats;
    int vert = 0;
    public List<Color> colorListSplats;
    public List<int> trianglesSplats;
    public List<Vector3> vertsSplatsB;
    public List<Vector2> uvsSplatsB;
    int vertB = 0;
    public List<Color> colorListSplatsB;
    public List<int> trianglesSplatsB;
    public GameObject rockObject, trunkObject, leafObject;
    public int size = 10, randomSeed = 0;
    public Material mat, highlightMaterial, highlightConsumerMaterial;
    public int x, z, exploreCost;

    public int index;
    void Start()
    {
        MeshSetup();
    }

    void MeshSetup()
    {
        trianglesSplats = new List<int>();
        colorListSplats = new List<Color>();
        vertsSplats = new List<Vector3>();
        Random.InitState(randomSeed);
        ProceduralSplat[] pss = GetComponentsInChildren<ProceduralSplat>();
        foreach (ProceduralSplat ps in pss)
        {
            if (!ps.is_tree)
            {
                ps.Init(this, ps.transform.position, ps.transform.localScale.x, ps.is_tree);
            }
        }
        AssignMesh(rockObject, vertsSplats, colorListSplats, trianglesSplats);
        trianglesSplats = new List<int>();
        colorListSplats = new List<Color>();
        vertsSplats = new List<Vector3>();
        foreach (ProceduralSplat ps in pss)
        {
            if (ps.is_tree)
            {
                ps.Init(this, ps.transform.position, ps.transform.localScale.x, ps.is_tree);
            }
        }
        AssignMesh(trunkObject, vertsSplats, colorListSplats, trianglesSplats);
        AssignMesh(leafObject, vertsSplatsB, colorListSplatsB, trianglesSplatsB);
    }

    List<Tri> checkedTris;
    int poiCount;

    Mesh assignMesh;
    void AssignMesh(GameObject go, List<Vector3> verts, List<Color> colors, List<int> tris)
    {
        assignMesh = new Mesh();
        MeshFilter mf = go.GetComponent<MeshFilter>();
        mf.mesh = assignMesh;
        assignMesh.SetVertices(verts);
        assignMesh.SetTriangles(tris, 0);
        assignMesh.SetColors(colors);
        assignMesh.RecalculateNormals();
        go.AddComponent<MeshCollider>();
    }
}