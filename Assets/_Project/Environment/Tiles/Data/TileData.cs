using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Tiles")]
[System.Serializable]
public class TileData : ScriptableObject
{
    public Enums.TileType TileType;
    public Material tileMaterial;
}
