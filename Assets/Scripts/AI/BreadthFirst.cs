using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Assets.Scripts.AI;

public class BreadthFirst
{
    private Queue<HexNode>   _frontier;
    private HashSet<HexNode> _cameFrom;


    public BreadthFirst(HexNode start)
    {
        _frontier = new Queue<HexNode>();
        _frontier.Enqueue(start);

        _cameFrom = new HashSet<HexNode>();
        _cameFrom.Add(start);
    }

    public void Step()
    {
        var current = _frontier.Dequeue();
        foreach (var next in current.Neighbors)
        {
            if (!_cameFrom.Contains(next))
            {
                _frontier.Enqueue(next);
                _cameFrom.Add(next);
            }
        }
    }

    public void Search()
    {
        while (_frontier.Count > 0)
        {
            Step();
        }
    }
}
