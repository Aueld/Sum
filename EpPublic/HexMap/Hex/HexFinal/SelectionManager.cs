using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // 사용자 선택 반환

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Unit unit;

    public LayerMask selectionMask;
    public TileGrid tileGrid;

    private List<Vector3Int> neighbours = new List<Vector3Int>();
    Tile unitOn;

    private List<Tile> visitTile = new List<Tile>();

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Start()
    {
        Invoke(nameof(LightOn), 1f);
    }

    public void HandleClick(Vector3 mousePosition)
    {
        if (unit.isMove)
            return;
        
        // 유닛 아래에 있는 발판
        unitOn = unit.GetTile();
        unitOn.visit = true;

        neighbours = tileGrid.GetNeighboursFor(unitOn.HexCoords);

        // 방문한 타일 리스트
        visitTile.Add(unit.GetTile());
        // 리스트에서 중복되는 타일이 쌓이지 않도록 제거합니다
        visitTile = visitTile.Distinct().ToList();

        GameObject result;

        if (FindTarget(mousePosition, out result))
        {
            Tile selectTile = result.GetComponent<Tile>();

            if (selectTile.isCanMove == false || (int)selectTile.tileType > 500)
                return;

            neighbours = tileGrid.GetNeighboursFor(unitOn.HexCoords);

            foreach (Vector3Int neighbour in neighbours)
            {
                tileGrid.GetTileAt(neighbour).visit = false;
            }
            foreach(Tile tile in visitTile)
            {
                tile.visit = true;
            }

            selectTile.visit = true;

            List<Vector3> pathList = new List<Vector3>();

            pathList = FindPath(tileGrid, unit.GetTile(), selectTile);

            unit.SetOffset(selectTile.HexCoords);
            unit.MoveThrougPath(pathList);
        }

        LightOn();
    }

    private void LightOn()
    {
        Tile unitOn = unit.GetTile();
        List<Tile> isCanMoveTile;

        isCanMoveTile = GraphSearch.CheckTiles(tileGrid, unitOn.HexCoords);

        for (int i = 0; i < isCanMoveTile.Count; i++)
        {
            isCanMoveTile[i].EnableHighlight();
        }

        neighbours = tileGrid.GetNeighboursFor(unitOn.HexCoords);

        foreach (Vector3Int neighbour in neighbours)
        {
            // TileType 500 이하의 타일은 움직일 수 있는 타일
            if ((int)tileGrid.GetTileAt(neighbour).tileType < 500)
            {
                tileGrid.GetTileAt(neighbour).visit = true;
                tileGrid.GetTileAt(neighbour).isCanMove = true;
                tileGrid.GetTileAt(neighbour).EnableHighlight();
            }
        }
    }

    private void LightOff()
    {
        foreach (Tile tile in tileGrid.GetAllTiles())
        {
            tile.DisableHighlight();
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

            // 500 이하의 움직일 수 있는 타일인지
            foreach (var neighbor in current.Neighbors.Where(t => t.visit && (int)t.tileType < 500 && !processed.Contains(t)))
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

