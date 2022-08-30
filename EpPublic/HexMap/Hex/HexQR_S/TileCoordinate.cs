using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoordinate : MonoBehaviour
{
    public Vector3Int offsetCoordiantes;

    public int index;
    public int floor;

    internal Vector3Int GetHexCoords()
        => offsetCoordiantes;
}
