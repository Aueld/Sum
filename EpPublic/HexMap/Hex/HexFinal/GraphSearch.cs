using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphSearch
{
    public static List<Tile> list_onTiles;

    public static List<Tile> CheckTiles(TileGrid tileGrid, Vector3Int startPoint)
    {
        list_onTiles = new List<Tile>();

        Dictionary<Vector3Int, Vector3Int?> visiteNodes = new Dictionary<Vector3Int, Vector3Int?>();
        Queue<Vector3Int> nodesToVisitQueue = new Queue<Vector3Int>();
        nodesToVisitQueue.Enqueue(startPoint);
        visiteNodes.Add(startPoint, null);

        while (nodesToVisitQueue.Count > 0)
        {
            Vector3Int currentNode = nodesToVisitQueue.Dequeue();
            foreach (Vector3Int neighourPosition in tileGrid.GetNeighboursFor(currentNode))
            {

                Tile nTile = tileGrid.GetTileAt(neighourPosition);
                if (nTile.isCanMove)
                {
                    if (!visiteNodes.ContainsKey(neighourPosition))
                    {
                        visiteNodes[neighourPosition] = currentNode;
                        nodesToVisitQueue.Enqueue(neighourPosition);
                        list_onTiles.Add(nTile);
                    }
                }
                else
                {
                    visiteNodes[neighourPosition] = currentNode;
                }
            }
        }
        return list_onTiles;
    }
}

