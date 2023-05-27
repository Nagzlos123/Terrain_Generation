using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    [SerializeField] private int xSize = 20;
    [SerializeField] private int zSize = 20;
    [SerializeField] private float amplitude1 = 2f;
    [SerializeField] private float frequency1 = 0.3f;
    [SerializeField] private float amplitude2 = 1f;
    [SerializeField] private float frequency2 = 0.4f;
    [SerializeField] private float amplitude3 = 3f;
    [SerializeField] private float frequency3 = 0.2f;
    [SerializeField] private float noiseStrength = 1f;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= zSize; x++)
            {
                //float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                float y = amplitude1 * Mathf.PerlinNoise(x * frequency1, z * frequency1)

                    + amplitude2 * Mathf.PerlinNoise(x * frequency2, z * frequency2)

                    + amplitude3 * Mathf.PerlinNoise(x * frequency3, z * frequency3)

                        * noiseStrength;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }

        }

        triangles = new int[xSize * zSize * 6];
        int verIndex = 0;
        int trianIndex = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int i = 0; i < xSize; i++)
            {
               
                triangles[trianIndex + 0] = verIndex + 0;
                triangles[trianIndex + 1] = verIndex + xSize + 1;
                triangles[trianIndex + 2] = verIndex + 1;
                triangles[trianIndex + 3] = verIndex + 1;
                triangles[trianIndex + 4] = verIndex + xSize + 1;
                triangles[trianIndex + 5] = verIndex + xSize + 2;
                verIndex++;
                trianIndex += 6;
            }
            verIndex++;
        }
    
 
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
