using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Hexa : MonoBehaviour
{
    // Hex 타일 반환

    [SerializeField]
    private GlowHighlight highlight;

    private HexCoordinate hexCoordinate;

    public Vector3Int HexCoords => hexCoordinate.GetHexCoords();


    private void Awake()
    {
        hexCoordinate = GetComponent<HexCoordinate>();
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
