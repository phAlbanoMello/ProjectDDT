using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public List<Floor> Floors;
    private Grid grid;

    private void Start()
    {
        foreach (var floor in Floors)
        {
            floor.Init();
        }
    }
}
