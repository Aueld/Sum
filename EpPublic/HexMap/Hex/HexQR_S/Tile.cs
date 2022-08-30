using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    // Hex Ÿ�� ��ȯ

    //[SerializeField]
    //private GlowHighlight highlight;

    private TileCoordinate hexCoordinate;

    public Vector3Int HexCoords => hexCoordinate.GetHexCoords();


    private void Awake()
    {
        hexCoordinate = GetComponent<TileCoordinate>();
        //highlight = GetComponent<GlowHighlight>();
    }

    //public void EnableHighlight()
    //{
    //    highlight.ToggleGlow(true);
    //}
    //public void DisableHighlight()
    //{
    //    highlight.ToggleGlow(false);
    //}
}
