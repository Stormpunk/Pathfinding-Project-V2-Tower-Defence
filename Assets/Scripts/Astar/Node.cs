using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node 
{
    public Point GridPosition { get; private set; }

    public TileScript TileRef { get; private set; }
    public Vector2 worldPosition { get; set; }
    public Node parent { get; private set; } 
    public int G { get; set; }
    public int H { get; set; }
    public int F { get; set; }

    public Node (TileScript tileRef)
    {
        TileRef = tileRef;
        GridPosition = tileRef.GridPosition;
        worldPosition = tileRef.worldPosition;
    }
    public void CalcValues(Node parent, Node goal, int gScore)
    {
        this.parent = parent;
        this.G = parent.G + gScore;
        this.H = (Math.Abs(GridPosition.X - goal.GridPosition.X) + (Math.Abs(goal.GridPosition.Y - GridPosition.Y)))* 10;
        this.F = G + H;
    }
}
