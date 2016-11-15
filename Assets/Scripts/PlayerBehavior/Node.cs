using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public List<Node> Neighbours;
    public int X;
    public int Y;

    public Node()
    {
        Neighbours = new List<Node>();
    }

    public float DistanceTo(Node n)
    {
        if (n == null)
        {
            Debug.LogError("WTF?");
        }
            return Vector2.Distance(
                new Vector2(X, Y),
                new Vector2(n.X, n.Y)
            );
    }

}
