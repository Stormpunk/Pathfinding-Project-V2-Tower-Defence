using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Astar
{
    private static Dictionary<Point, Node> _nodes;

    private static void CreateNodes()
    {
        _nodes = new Dictionary<Point, Node>();
        foreach (TileScript tiles in LevelManager.Instance.Tiles.Values)
        {
            _nodes.Add(tiles.GridPosition, new Node(tiles));
        }
    }
    public static void GetPath(Point start)
    {
        if (_nodes == null)
        {
            CreateNodes();
        }
        HashSet<Node> openList = new HashSet<Node>();
        Node currentNode = _nodes[start];
        openList.Add(currentNode);
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Point neighborPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);
                if(LevelManager.Instance.InBounds(neighborPos) && LevelManager.Instance.Tiles[neighborPos].Walkable && neighborPos != currentNode.GridPosition)
                {
                    Node neighbor = _nodes[neighborPos];
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                    neighbor.CalcValues(currentNode);
                }
                Debug.Log(neighborPos.X + " " + neighborPos.Y);
            }
        }
    }
}
