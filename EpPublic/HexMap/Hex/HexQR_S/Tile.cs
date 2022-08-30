using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    // Hex Ÿ�� ��ȯ
    public Vector3Int offsetCoordiantes;

    public int index;
    public int floor;

    [SerializeField]
    private TileType tileType;

    private GlowHighlight highlight;

    public Vector3Int HexCoords => offsetCoordiantes;

    // �ڽ�Ʈ ����ġ
    // �䱸�ϴ� �ڽ�Ʈ ������ Ÿ���� ǥ�� ����
    public int GetCost()
        => tileType switch
        {
            TileType.Obstacle => 99,
            TileType.Water => 99, 
            TileType.Difficult => 20,
            TileType.Default => 10,
            TileType.Plain => 5,

            _ => throw new System.Exception("���� Ÿ�� Ÿ��")
        };

    public bool IsObstacle()
    {
        return this.tileType == TileType.Obstacle;
    }

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
    None,
    Default,
    Difficult,
    Plain,
    Water,
    Obstacle
}