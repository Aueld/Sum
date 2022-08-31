using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // ����� ���� ��ȯ

    [SerializeField]
    private Camera mainCamera;

    public LayerMask selectionMask;
    public TileGrid tileGrid;

    private List<Vector3Int> neighbours = new List<Vector3Int>();

    private void Awake()
    {
        if(mainCamera == null)
            mainCamera = Camera.main;
    }

    public void HandeClick(Vector3 mousePosition)
    {
        GameObject result;
        if(FindTarget(mousePosition, out result))
        {
            Tile selectTile = result.GetComponent<Tile>();

            selectTile.DisableHighlight();

            foreach (Vector3Int neighbour in neighbours)
            {
                tileGrid.GetTileAt(neighbour).DisableHighlight();
            }

            // �ֺ� 6�� Ÿ�� 
            //neighbours = tileGrid.GetNeighboursFor(selectTile.HexCoords);
            
            // �ڽ�Ʈ ��ŭ ����� �Ÿ� Ÿ��
            BFSResult bfsResult = GraphSearch.BFSGetRange(tileGrid, selectTile.HexCoords, 20);
            neighbours = new List<Vector3Int>(bfsResult.GetRangePosition());


            foreach (Vector3Int neighbour in neighbours)
            {
                tileGrid.GetTileAt(neighbour).EnableHighlight();
            }


            //Debug.Log($"Ŭ�� Ÿ�� : {selectTile.HexCoords} : ");
            //foreach (Vector3Int neighbourPos in neighbours)
            //{
            //    Debug.Log(neighbourPos);
            //}
        }
    }

    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        
        if (Physics.Raycast(ray, out hit, 100, selectionMask))
        {
            result = hit.collider.gameObject;
            return true;
        }

        result = null;
        return false;
    }
}
