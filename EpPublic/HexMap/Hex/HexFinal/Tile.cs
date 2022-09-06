using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    // Hex Ÿ�� ��ȯ
    public Vector3Int offsetCoordiantes;

    public bool visit;
    public bool isCanMove;
    public int index;
    public int floor;

    public TileType tileType;

    private GlowHighlight highlight;

    public Vector3Int HexCoords => offsetCoordiantes;

    // A star
    // ���� ���� ~ ����
    public int G { get; private set; }
    // ���� ~ ���� ����
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
    // �ڽ�Ʈ ����ġ
    // �䱸�ϴ� �ڽ�Ʈ ������ Ÿ���� ǥ�� ����
    //public int GetCost()
    //    => tileType switch
    //    {
    //        TileType.Obstacle => 99,
    //        TileType.Water => 99,
    //        TileType.Default => 10,
    //        TileType.Plain => 5,

    //        _ => throw new System.Exception("���� Ÿ�� Ÿ��")
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