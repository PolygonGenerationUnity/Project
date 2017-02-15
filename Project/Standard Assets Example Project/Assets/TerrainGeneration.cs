using System.IO;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour {

    private Terrain myTerrain;
    private bool isUsed = false;
    private Texture2D heightmap;

    void Start () {
        myTerrain = Terrain.activeTerrain;
        heightmap = new Texture2D(512,512);
        heightmap.LoadImage(File.ReadAllBytes("C:\\heightmap.jpeg"));
    }
	
	void Update () {

        if (Input.GetKey("up"))
        {
            if (!myTerrain)
            {
                Debug.LogError(gameObject.name + " has no terrain assigned in the inspector");
                return;
            }


            if (isUsed) { return; }

            TerrainData TD = myTerrain.terrainData;
            float[,] HeightMap = new float[TD.heightmapWidth, TD.heightmapHeight];
            for (int x = 0; x < 512; x++)
            {
                for (int y = 0; y < 512; y++)
                {
                    HeightMap[x, y] = heightmap.GetPixel(x,y).grayscale / 12.5f;
                }
            }
            isUsed = true;
            TD.SetHeights(0, 0, HeightMap);
        }

        if (Input.GetKey("down"))
        {
            if (!myTerrain)
            {
                Debug.LogError(gameObject.name + " has no terrain assigned in the inspector");
                return;
            }

            TerrainData TD = myTerrain.terrainData;
            float[,] HeightMap = new float[TD.heightmapWidth, TD.heightmapHeight];
            for (int x = 0; x < TD.heightmapWidth; x++)
            {
                for (int y = 0; y < TD.heightmapHeight; y++)
                {
                    HeightMap[x, y] = 0f;
                }
            }
            isUsed = false;
            TD.SetHeights(0, 0, HeightMap);
        }
    }
}
