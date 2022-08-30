using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    Dictionary<Vector3Int, Tile> hexTileDict = new Dictionary<Vector3Int, Tile>();
    Dictionary<Vector3Int, List<Vector3Int>> hexTileNeighboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();


    private void Start()
    {
        // 불러오기
        Invoke(nameof(Delay), 1.5f);
    }

    private void Delay()
    {
        foreach (Tile hex in FindObjectsOfType<Tile>())
        {
            hexTileDict[hex.HexCoords] = hex;
        }
    }

    public Tile GetTileAt(Vector3Int hexCoordinate)
    {
        Tile result = null;
        hexTileDict.TryGetValue(hexCoordinate, out result);
        return result;
    }

    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
    {
        if (hexTileDict.ContainsKey(hexCoordinates) == false)
            return new List<Vector3Int>();

        if (hexTileNeighboursDict.ContainsKey(hexCoordinates))
            return hexTileNeighboursDict[hexCoordinates];

        hexTileNeighboursDict.Add(hexCoordinates, new List<Vector3Int>());

        foreach (Vector3Int dierection in Direction.GetDirectionList(hexCoordinates.z))
        {

            if (hexTileDict.ContainsKey(hexCoordinates + dierection))
            {
                hexTileNeighboursDict[hexCoordinates].Add(hexCoordinates + dierection);
            }
        }

        return hexTileNeighboursDict[hexCoordinates];
    }
}

public static class Direction
{
    public static List<Vector3Int> directionsOffsetOdd = new List<Vector3Int>
    {
        new Vector3Int(0, 0, 1),
        new Vector3Int(1, 0, 0),
        new Vector3Int(1, 0, -1),
        new Vector3Int(0, 0, -1),
        new Vector3Int(-1, 0, -1),
        new Vector3Int(-1, 0, 0)
    };

    public static List<Vector3Int> directionsOffsetEven = new List<Vector3Int>
    {
        new Vector3Int(0, 0, 1),
        new Vector3Int(1, 0, 1),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, 0, -1),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(-1, 0, 1)
    };

    public static List<Vector3Int> GetDirectionList(int z)
        => z % 0.75f == 0 ? directionsOffsetEven : directionsOffsetOdd;
}
