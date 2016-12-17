using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AI.Pathfinding;

class BreadthFirst
{
    private Queue<HexNode>   _frontier;
    private HashSet<HexNode> _visited;
    private bool             _done;

    public void Search(HexNode start, int expansionLimit)
    {
        _done = false;
        start.Expansion = 0;
        _frontier = new Queue<HexNode>();
        _frontier.Enqueue(start);

        _visited = new HashSet<HexNode>();
        _visited.Add(start);

        while (_frontier.Count > 0)
        {
            HexNode current = _frontier.Dequeue();

            foreach (HexNode next in current.Neighbors)
            {
                if (!_visited.Contains(next))
                {
                    next.Expansion = current.Expansion + 1;
                    if (next.Expansion < expansionLimit)
                    {
                        _frontier.Enqueue(next);
                        _visited.Add(next);
                    }
                }
            }
        }
        _done = true;
    }

    public bool Done
    {
        get { return _done; }
    }

    public List<HexNode> Nodes
    {
        get
        {
            if (Done) {return _visited.ToList();}
            return null;
        }
    }
}