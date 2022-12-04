using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point (int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
    public static bool operator ==(Point x, Point y)
    {
        return x.X == y.X && x.Y == y.Y;
    }
    public static bool operator != (Point x, Point y)
    {
        return x.X != y.X || y.Y != x.Y;
    }
    public static Point operator -(Point x, Point y)
    {
        return new Point(x.X - y.X, x.Y - y.Y);
    }
}
