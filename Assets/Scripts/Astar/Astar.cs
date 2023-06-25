using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public static class Astar
{
    private static Dictionary<Point, Node> _nodes;

    private static void CreateNodes()
    {
        _nodes = new Dictionary<Point, Node>();
        foreach (TileScript tiles in LevelManager.Instance.Tiles.Values)
        {
            Point gridPosition = tiles.GridPosition;
            bool walkable = !Physics2D.OverlapPoint(new Vector2(gridPosition.X, gridPosition.Y));
            Node node = new Node(tiles);
            node.isWalkable = walkable;
            _nodes.Add(gridPosition, node);
        }
    }
    public static Stack<Node> GetPath(Point start, Point goal)
    {
        if (_nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();
        HashSet<Node> closedList = new HashSet<Node>();
        Stack<Node> finalPath = new Stack<Node>();
        Node currentNode = _nodes[start];
        openList.Add(currentNode);
        while (openList.Count > 0)
        {

            //sorts by list and orders based on the f value
            currentNode = openList.OrderBy(n => n.F).First();

            if (currentNode == _nodes[goal])
            {
                finalPath.Push(currentNode);
                while (currentNode.parent != null)
                {
                    finalPath.Push(currentNode.parent);
                    currentNode = currentNode.parent;
                }
                break;
            }

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighborPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);
                    if (LevelManager.Instance.InBounds(neighborPos) && LevelManager.Instance.Tiles[neighborPos].Walkable && neighborPos != currentNode.GridPosition)
                    {
                        Vector2 neighborWorldPos = new Vector2(neighborPos.X, neighborPos.Y);
                        int gCost = 0;
                        //this should mean direct neighbors should have a g cost of 10
                            if (Math.Abs(x - y) == 1 && !LevelManager.Instance.Tiles[neighborPos].Walkable && !LevelManager.Instance.Tiles[neighborPos].isWithinTowerRange)
                            {
                                gCost = 10;
                            }
                            else if (Math.Abs(x - y) == 1 && LevelManager.Instance.Tiles[neighborPos].Walkable && !LevelManager.Instance.Tiles[neighborPos].isWithinTowerRange)
                            {
                                gCost = 14;
                            }
                            //this means diagonal neighbors have a g cost of 14
                            else if (LevelManager.Instance.Tiles[neighborPos].isWithinTowerRange)
                            { //give a large cost to nodes within range of a tower
                                gCost = 300;
                            }
                            Node neighbor = _nodes[neighborPos];
                            if (openList.Contains(neighbor))
                            {
                                if (currentNode.G + gCost < neighbor.G)
                                {
                                    neighbor.CalcValues(currentNode, _nodes[goal], gCost);
                                }
                            }
                            if (!closedList.Contains(neighbor))
                            {
                                openList.Add(neighbor);
                                neighbor.CalcValues(currentNode, _nodes[goal], gCost);
                            }
                            //Debug.Log(neighborPos.X + " " + neighborPos.Y);
                    }
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

        }
        return finalPath;
    }
  
}
