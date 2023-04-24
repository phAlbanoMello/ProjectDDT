using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile : MonoBehaviour
{
    public Type type;
    public Vector2 coordinate;
    public bool selected;

    public Tile(Type type)
    {
        this.type = type;
    }
}
