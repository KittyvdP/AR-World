using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaterGenerator : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;

    public int xSize = 20;
    public int zSize = 20;

    //public GameObject Bottom;
    //private Vector3[] bottomVertices;
    //private int[] bottomTriangles;
    //private Vector2[] bottomUvs;
    //private Mesh bottomMesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        //bottomMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //Bottom.GetComponent<MeshFilter>().mesh = bottomMesh;

        CreateShape();
        UpdateMesh();
        
        //CreateBottom();

        CorrectPosition();
    }

    private void CorrectPosition()
    {
        transform.position = new Vector3(-(xSize / 2), transform.position.y, -(zSize / 2));
    }

    //private void CreateBottom()
    //{
    //    bottomVertices = new Vector3[]
    //    {
    //        new Vector3(0, 0, 0),
    //        new Vector3(0, 0, zSize),
    //        new Vector3(xSize, 0, 0),
    //        new Vector3(xSize, 0, zSize)
    //    };

    //    bottomTriangles = new int[]
    //    {
    //        0, 1, 2,
    //        1, 3, 2
    //    };

    //    bottomMesh.Clear();
    //    bottomMesh.vertices = bottomVertices;
    //    bottomMesh.triangles = bottomTriangles;
        
    //    bottomMesh.RecalculateNormals();
    //}

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
            }
        }

    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
    }
}
