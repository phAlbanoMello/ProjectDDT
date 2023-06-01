using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Tile : MonoBehaviour
{
    public Tilemap Tilemap { get; private set; }
    public Enums.TileType tileType;
    private Vector3 coordinate;
    public bool selected;

    public TileData TileData;
    private Material material;

    public Tile(Enums.TileType type)
    {
        this.tileType = type;
    }

    private void Start()
    { 
        material = GetComponent<MeshRenderer>().material;
        Tilemap = transform.parent.GetComponent<Tilemap>();
        coordinate = Tilemap.WorldToCell(transform.position);
    }

    private void Update()
    {
        material.SetFloat("_Selected", selected ? 1.0f : 0.0f);
    }
}
