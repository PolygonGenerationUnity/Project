using System.IO;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour {

    private Terrain myTerrain;
    private bool isUsed = false;
    private Texture2D heightmap;

    void Start () {
        myTerrain = Terrain.activeTerrain;
        heightmap = new Texture2D(2049,2049);
        heightmap.LoadImage(File.ReadAllBytes("C:\\Heightmap2.png"));
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
            for (int x = 0; x < 2049; x++)
            {
                for (int y = 0; y < 2049; y++)
                {
                    HeightMap[x, y] = heightmap.GetPixel(x,y).grayscale;
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
