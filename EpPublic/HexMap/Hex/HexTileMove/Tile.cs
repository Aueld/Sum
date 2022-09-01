using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    // Hex 타일 반환
    public Vector3Int offsetCoordiantes;

    public bool visit;
    public bool isCanMove;
    public int index;
    public int floor;

    public TileType tileType;

    private GlowHighlight highlight;

    public Vector3Int HexCoords => offsetCoordiantes;

    // A star
    // 시작 지점 ~ 현재
    public int G { get; private set; }
    // 현재 ~ 도착 지점
    public int H { get; private set; }
    public float F
        => G + H;
    public void SetG(int g)
    {
        G = g;
    }
    public void SetH(int h)
    {
        H = h;
    }
    public Tile Connection { get; private set; }
    public int GetDistance(Tile target)
    {
        return (int)(target.transform.position - transform.position).magnitude;
    }
    public void SetConnection(Tile tile)
    {
        Connection = tile;
    }
    public List<Tile> Neighbors { get; set; }

    #region cost
    // 코스트 가중치
    // 요구하는 코스트 이하의 타일은 표시 안함
    //public int GetCost()
    //    => tileType switch
    //    {
    //        TileType.Obstacle => 99,
    //        TileType.Water => 99,
    //        TileType.Default => 10,
    //        TileType.Plain => 5,

    //        _ => throw new System.Exception("없는 타일 타입")
    //    };

    //public bool IsObstacle()
    //{
    //    return this.tileType == TileType.Obstacle;
    //}
    #endregion

    private void Awake()
    {
        highlight = GetComponent<GlowHighlight>();
    }

    public void EnableHighlight()
    {
        highlight.ToggleGlow(true);
    }
    public void DisableHighlight()
    {
        highlight.ToggleGlow(false);
    }
    
}

public enum TileType
{
    plain = 0,
    beach = 1,
    forest = 2,
    lake = 998,
    sea = 999
}