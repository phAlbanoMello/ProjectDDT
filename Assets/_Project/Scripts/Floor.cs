using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    private Tilemap tilemap;
    private List<Tile> tiles = new List<Tile>();

    public void Init()
    {
        tilemap = GetComponent<Tilemap>();

       Tile[] list = GetComponentsInChildren<Tile>();
        foreach (Tile tile in list)
        {
            tiles.Add(tile);
        }
    }

    public Tile GetTile(Vector3Int cellPositon)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.gameObject.transform.position == tilemap.CellToWorld(cellPositon))
            {
                return tile;
            }
        }
        return null;
    }

    public Tile[] GetTiles()
    {
        return tiles.ToArray();
    }
}
