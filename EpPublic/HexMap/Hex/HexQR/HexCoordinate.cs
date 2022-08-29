using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoordinate : MonoBehaviour
{
    // À§Ä¡
    public static float xOffset = 0.75f, yOffset = 1, zOffset = 0.86f;

    [SerializeField]
    private Vector3Int offsetCoordiantes;

    internal Vector3Int GetHexCoords()
        => offsetCoordiantes;

    private void Start()
    {
        Invoke(nameof(Delay), 1);
    }

    private void Delay()
    {
        offsetCoordiantes = ConvertPostionToOffset(transform.position);
    }

    private Vector3Int ConvertPostionToOffset(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / xOffset);
        int y = Mathf.RoundToInt(position.y / yOffset);
        int z = Mathf.CeilToInt(position.z / zOffset);
        
        return new Vector3Int(x, y, z);
    }
}
