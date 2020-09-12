using UnityEngine;
using System.Collections;
 
public class TerrainTerraforming : MonoBehaviour
{
    private float[,,] alphaData;
    private TerrainData tData;
    private float percentage;

    private const int DESERT = 0; //These numbers depend on the order in which
    private const int GRASS = 1; //the textures are loaded onto the terrain
    void Start()
    {
        tData = Terrain.activeTerrain.terrainData;

        alphaData = tData.GetAlphamaps(0, 0, tData.alphamapWidth, tData.alphamapHeight);

        SetPercentage(20);
    }

    public void SetPercentage(double perc)
    {
        percentage = (float)perc / 100f;
        for (int x = 0; x < tData.alphamapWidth; x++)
        {
            for (int y = 0; y < tData.alphamapHeight; y++)
            {
                int month = Random.Range(0, 1);
                alphaData[x, y, month] = 1;
            }
        }
        
        tData.SetAlphamaps(0, 0, alphaData);
    }
}