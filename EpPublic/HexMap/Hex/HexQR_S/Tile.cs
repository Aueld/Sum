using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    // Hex 타일 반환
    public Vector3Int offsetCoordiantes;

    public int index;
    public int floor;

    [SerializeField]
    private TileType tileType;

    private GlowHighlight highlight;

    public Vector3Int HexCoords => offsetCoordiantes;

    // 코스트 가중치
    // 요구하는 코스트 이하의 타일은 표시 안함
    public int GetCost()
        => tileType switch
        {
            TileType.Obstacle => 99,
            TileType.Water => 99, 
            TileType.Difficult => 20,
            TileType.Default => 10,
            TileType.Plain => 5,

            _ => throw new System.Exception("없는 타일 타입")
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