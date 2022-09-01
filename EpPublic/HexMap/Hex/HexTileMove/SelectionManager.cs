using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // ����� ���� ��ȯ

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Unit unit;

    public LayerMask selectionMask;
    public TileGrid tileGrid;

    private List<Vector3Int> neighbours = new List<Vector3Int>();

    private bool SelectMode = false;

    private List<Tile> visitTile = new List<Tile>();

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    public void HandleClick(Vector3 mousePosition)
    {
        if (unit.isMove)
            return;

        if (SelectMode == false)
        {
            SelectMode = true;

            // ���� �Ʒ��� �ִ� ����
            Tile unitOn = unit.GetTile();

            unitOn.DisableHighlight();

            foreach (Vector3Int neighbour in neighbours)
            {
                tileGrid.GetTileAt(neighbour).DisableHighlight();
            }

            neighbours = tileGrid.GetNeighboursFor(unitOn.HexCoords);

            List<Tile> isCanMoveTile;

            isCanMoveTile = GraphSearch.CheckTiles(tileGrid, unitOn.HexCoords);

            for (int i = 0; i < isCanMoveTile.Count; i++)
            {
                isCanMoveTile[i].EnableHighlight();
            }

            // visit ���� true�� ��� Ÿ���� �˻��ϴ� �ͺ���
            // ����Ʈ�� ���� �湮�� Ÿ�ϸ� Ű�°� ���ϰ� �� �Ǿ ���ܵξ����ϴ�
            //if (visitTile != null)
            //{
            //    foreach (var tile in visitTile)
            //    {
            //        tile.isCanMove = true;
            //        tile.EnableHighlight();
            //    }
            //}

            foreach (Vector3Int neighbour in neighbours)
            {
                // TileType 500 ������ Ÿ���� ������ �� �ִ� Ÿ��
                if ((int)tileGrid.GetTileAt(neighbour).tileType < 500)
                {
                    tileGrid.GetTileAt(neighbour).isCanMove = true;
                    tileGrid.GetTileAt(neighbour).EnableHighlight();
                }
            }

            // �湮�� Ÿ��
            unit.GetTile().visit = true;

            // �湮�� Ÿ�� ����Ʈ
            visitTile.Add(unit.GetTile());
            // ����Ʈ���� �ߺ��Ǵ� Ÿ���� ������ �ʵ��� �����մϴ�
            visitTile = visitTile.Distinct().ToList();

            return;
        }
        else
        {
            SelectMode = false;

            GameObject result;

            if (FindTarget(mousePosition, out result))
            {
                Tile selectTile = result.GetComponent<Tile>();

                selectTile.DisableHighlight();

                if (selectTile.isCanMove == false || (int)selectTile.tileType > 500)
                    return;

                if (visitTile != null)
                {
                    foreach (var tile in visitTile)
                    {
                        tile.DisableHighlight();
                    }
                }

                
                foreach (Vector3Int neighbour in neighbours)
                {
                    //tileGrid.GetTileAt(neighbour).isCanMove = false;
                    tileGrid.GetTileAt(neighbour).DisableHighlight();
                }

                List<Vector3> pathList = new List<Vector3>();

                pathList = FindPath(tileGrid, unit.GetTile(), selectTile);

                unit.SetOffset(selectTile.HexCoords);
                unit.MoveThrougPath(pathList);
            }
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

    public List<Vector3> FindPath(TileGrid tileGrid, Tile startTile, Tile targetTile)
    {
        var toSearch = new List<Tile>() { startTile };
        var processed = new List<Tile>();

        while (toSearch.Any())
        {
            Tile current = toSearch[0];
            foreach (Tile t in toSearch)
                if (t.F < current.F || t.F == current.F && t.H < current.H)
                    current = t;

            processed.Add(current);
            toSearch.Remove(current);

            if (current == targetTile)
            {
                Tile currentPathTile = targetTile;
                List<Vector3> path = new List<Vector3>();

                int count = 100;

                while (currentPathTile != startTile)
                {
                    path.Add(currentPathTile.transform.position);
                    currentPathTile = currentPathTile.Connection;
                    count--;
                }

                return path;
            }

            List<Vector3Int> nei = tileGrid.GetNeighboursFor(current.HexCoords);

            current.Neighbors = new List<Tile>();

            foreach(var ne in nei)
            {
                current.Neighbors.Add(tileGrid.GetTileAt(ne));
            }

            // 500 ������ ������ �� �ִ� Ÿ������
            foreach (var neighbor in current.Neighbors.Where(t => (int)t.tileType < 500 && !processed.Contains(t)))
            {
                bool inSearch = toSearch.Contains(neighbor);

                int costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.G)
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (!inSearch)
                    {
                        neighbor.SetH(neighbor.GetDistance(targetTile));
                        toSearch.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }
}

