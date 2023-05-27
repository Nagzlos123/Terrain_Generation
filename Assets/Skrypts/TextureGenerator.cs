using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator 
{
  public static Texture2D TextureFromColor(Color [] colors, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colors);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int hight = heightMap.GetLength(1);

        
        Color[] colors = new Color[width * hight];
        for (int y = 0; y < hight; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colors[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }

        return TextureFromColor(colors, width, hight);
    }
}
