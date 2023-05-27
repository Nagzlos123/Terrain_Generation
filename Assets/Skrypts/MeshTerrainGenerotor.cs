using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshTerrainGenerotor 
{
   public static MeshData GenerateMesh(float[,] heightMap, float heightMuti, AnimationCurve meshAnimationCurve, int levelOfDetale)
    {
        AnimationCurve animationCurve = new AnimationCurve(meshAnimationCurve.keys);
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        int meshSimplification = (levelOfDetale == 0) ? 1 : levelOfDetale * 2;
        int verticesPerLine = (width - 1) / meshSimplification + 1;

        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
        int verIndex = 0;

        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (width - 1) / 2f;

        for (int y = 0; y < height; y+= meshSimplification)
        {
            for (int x = 0; x < width; x+= meshSimplification)
            {
                meshData.vertices[verIndex] = new Vector3(topLeftX + x, meshAnimationCurve.Evaluate(heightMap[x, y]) * heightMuti, topLeftZ - y);
                meshData.uvs [verIndex] = new Vector2(x/(float)width, y/(float)height);

                if(x < width -1 && y < height -1)
                {
                    meshData.AddTtriangle(verIndex, verIndex + verticesPerLine + 1, verIndex + verticesPerLine);
                    meshData.AddTtriangle(verIndex + verticesPerLine + 1, verIndex, verIndex + 1);
                }
                verIndex++;
            }
            
        }
        return meshData;
    }
 
}
public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    int trianIndex = 0;
    public Vector2[] uvs;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTtriangle(int a, int b, int c)
    {
        triangles[trianIndex + 0] = a;
        triangles[trianIndex + 1] = b;
        triangles[trianIndex + 2] = c;
        trianIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
