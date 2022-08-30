using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphSearch
{
    public static BFSResult BFSGetRange(TileGrid tileGrid, Vector3Int startPoint, int movementPoints)
    {
        Dictionary<Vector3Int, Vector3Int?> visiteNodes = new Dictionary<Vector3Int, Vector3Int?>();
        Dictionary<Vector3Int, int> costSoFar = new Dictionary<Vector3Int, int>();
        Queue<Vector3Int> nodesToVisitQueue = new Queue<Vector3Int>();

        nodesToVisitQueue.Enqueue(startPoint);
        costSoFar.Add(startPoint, 0);

        visiteNodes.Add(startPoint, null);

        while (nodesToVisitQueue.Count > 0)
        {
            Vector3Int currentNode = nodesToVisitQueue.Dequeue();

            foreach(Vector3Int neighourPosition in tileGrid.GetNeighboursFor(currentNode))
            {
                if (tileGrid.GetTileAt(neighourPosition).IsObstacle())
                    continue;

                int nodeCost = tileGrid.GetTileAt(neighourPosition).GetCost();
                int currentCost = costSoFar[currentNode];
                int newCost = currentCost + nodeCost;

                if(newCost <= movementPoints)
                {
                    if (!visiteNodes.ContainsKey(neighourPosition))
                    {
                        visiteNodes[neighourPosition] = currentNode;
                        costSoFar[neighourPosition] = newCost;
                        nodesToVisitQueue.Enqueue(neighourPosition);
                    }
                    else if (costSoFar[neighourPosition] > newCost)
                    {
                        costSoFar[neighourPosition] = newCost;
                        visiteNodes[neighourPosition] = currentNode;
                    }
                }
            }
        }
        return new BFSResult { visitedNodesDict = visiteNodes };
    }

    public static List<Vector3Int> GeneratePathBFS(Vector3Int current, Dictionary<Vector3Int, Vector3Int?> visitedNodesDict)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(current);
        while (visitedNodesDict[current] != null)
        {
            path.Add(visitedNodesDict[current].Value);
            current = visitedNodesDict[current].Value;
        }
        path.Reverse();
        return path.Skip(1).ToList();
    }
}

public struct BFSResult
{
    public Dictionary<Vector3Int, Vector3Int?> visitedNodesDict;

    public List<Vector3Int> GetPathTo(Vector3Int destination)
    {
        if (visitedNodesDict.ContainsKey(destination) == false)
            return new List<Vector3Int>();
        return GraphSearch.GeneratePathBFS(destination, visitedNodesDict);
    }

    public bool IsTilePositionInRange(Vector3Int position)
    {
        return visitedNodesDict.ContainsKey(position);
    }


    public IEnumerable<Vector3Int> GetRangePosition()
        => visitedNodesDict.Keys;
}
