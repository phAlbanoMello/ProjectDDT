using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTilesTerrain : MonoBehaviour
{
    public GameObject[] TileObs;
    public Transform startPosition;
    public int Area;
    public float tileSpacing; // The spacing between each tile in world units
    public Type[] types;
    public List<Tile> TilesList;
    public float heightVariationRate; // The rate of height variations
    public float maxHeightVariation;

    private float tileSize = 1; // The size of each tile in world units
    // Start is called before the first frame update
    void Start()
    {
        TilesList = new List<Tile>();

        float totalTerrainSize = Area * (tileSize + tileSpacing); // Calculate the total size of the terrain

        for (int x = 0; x < Area; x++)
        {
            for (int z = 0; z < Area; z++)
            {
                Tile newTile = new Tile(types[Random.Range(0, types.Length - 1)]);
                newTile.coordinate = new Vector2(x, z);
                TilesList.Add(newTile);

                // Calculate the position for each spawned TileOb based on the tile size, spacing, and terrain size
                float xPos = (x * (tileSize + tileSpacing)) - (totalTerrainSize / 2) + (tileSize / 2) + startPosition.position.x;
                float zPos = (z * (tileSize + tileSpacing)) - (totalTerrainSize / 2) + (tileSize / 2) + startPosition.position.z;

                float heightVariation = Random.Range(-maxHeightVariation, maxHeightVariation);
                float yPos = startPosition.position.y + heightVariation * heightVariationRate;

                Vector3 spawnPosition = new Vector3(xPos, yPos, zPos);

                // Spawn TileOb for each newTile at the calculated position
                GameObject spawnedTileOb = Instantiate(TileObs[(int)newTile.type], spawnPosition, TileObs[(int)newTile.type].transform.localRotation, gameObject.transform);
                // Set the name of the spawned TileOb to include the index of the newTile
                spawnedTileOb.name = "TileOb_" + x + "_" + z;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}   

public enum Type
{
    Grass,
    Lava,
    Water,
    Rock
}
