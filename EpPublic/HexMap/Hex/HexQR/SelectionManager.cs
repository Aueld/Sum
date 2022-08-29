using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // 사용자 선택 반환

    [SerializeField]
    private Camera mainCamera;

    public LayerMask selectionMask;
    public HexGrid hexGrid;

    List<Vector3Int> neighbours = new List<Vector3Int>();

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
            Hexa selectHex = result.GetComponent<Hexa>();

            selectHex.DisableHighlight();

            foreach (Vector3Int neighbour in neighbours)
            {
                hexGrid.GetTileAt(neighbour).DisableHighlight();
            }

            neighbours = hexGrid.GetNeighboursFor(selectHex.HexCoords);

            foreach (Vector3Int neighbour in neighbours)
            {
                hexGrid.GetTileAt(neighbour).EnableHighlight();
            }


            Debug.Log($"클릭 타일 : {selectHex.HexCoords} : ");
            foreach (Vector3Int neighbourPos in neighbours)
            {
                Debug.Log(neighbourPos);
            }
        }
    }

    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit, selectionMask))
        {
            result = hit.collider.gameObject;
            return true;
        }

        result = null;
        return false;
    }
}
