using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildCit : MonoBehaviour {

    public GameObject[] buildings;
    public GameObject xstreets;
    public GameObject zstreets;
    public GameObject crossroad;
    public int mapWidth = 100;
    public int mapHeigth = 100;
    int buildingsFootprint = 5;
    int[,] mapgrid;
    // Use this for initialization
    void Start()
    {

        mapgrid = new int[mapWidth, mapHeigth];

        for (int h = 0; h < mapHeigth; h++)
        {
            for (int w = 0; w < mapWidth; w++)
            {
                mapgrid[w, h] = (int)(Mathf.PerlinNoise(w / 10.0f, h / 10.0f) * 10);
            }
        }

        int x = 0;
        for (int n = 0; n < 50; n++)
        {
            for (int h = 0; h < mapHeigth; h++)
            {
                mapgrid[x, h] = -1;
            }
            x += Random.Range(3, 3);
            if (x >= mapWidth)
                break;
        }

        int z = 0;
        for (int n = 0; n < 10; n++)
        {
            for (int w = 0; w < mapWidth; w++)
            {
                if (mapgrid[w, z] == -1)
                    mapgrid[w, z] = -3;
                else
                    mapgrid[w, z] = -2;
            }
            z += Random.Range(2, 10);
            if (x >= mapHeigth)
                break;
        }

        Terrain myTerrain = Terrain.activeTerrain;

        for (int h = 0; h < mapHeigth; h++)
        {
            for (int w = 0; w < mapWidth; w++)
            {
                int result = mapgrid[w, h];
                Vector3 pos = new Vector3(w * buildingsFootprint, 0, h * buildingsFootprint);
                float height = myTerrain.terrainData.GetHeight(w * buildingsFootprint + 10, h * buildingsFootprint + 10);
                if (height != 0)
                {
                    Debug.logger.LogWarning("!!!!!!!!!!!!!!!!! NOT NULL!!!!!!!!!!!!!", 0);
                }
                else { 
                    if (result < -2)
                        Instantiate(crossroad, pos, crossroad.transform.rotation);
                    else if (result < -1)
                        Instantiate(xstreets, pos, xstreets.transform.rotation);
                    else if (result < 0)
                        Instantiate(zstreets, pos, zstreets.transform.rotation);
                    else if (result < 1)
                        Instantiate(buildings[0], pos, Quaternion.identity);
                    else if (result < 2)
                        Instantiate(buildings[1], pos, Quaternion.identity);
                    else if (result < 4)
                        Instantiate(buildings[2], pos, Quaternion.identity);
                    else if (result < 6)
                        Instantiate(buildings[3], pos, Quaternion.identity);
                    else if (result < 7)
                        Instantiate(buildings[4], pos, Quaternion.identity);
                    else if (result < 10)
                        Instantiate(buildings[5], pos, Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
