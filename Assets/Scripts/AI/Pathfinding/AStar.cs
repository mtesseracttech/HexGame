using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AStar
    {
        public static List<HexNode> Search(HexNode start, HexNode end)
        {
            List<HexNode> openList = new List<HexNode>();
            List<HexNode> closedList = new List<HexNode>();
            openList.Add(start);

            while (openList.Count > 0)
            {
                openList.Sort();
                HexNode q = openList[openList.Count - 1];
                openList.RemoveAt(openList.Count - 1);
                foreach (var neighbor in q.Neighbors)
                {
                    neighbor.Parent = q;
                    if (neighbor != end)
                    {
                        //Calculating the costs
                        neighbor.g = q.g + Vector3.Distance(q.GetPosition(), neighbor.GetPosition());
                        neighbor.h = Vector3.Distance(end.GetPosition(), neighbor.GetPosition());
                        neighbor.f = neighbor.g + neighbor.h;
                        if (openList.Contains(neighbor))
                        {
                            bool skip = false;
                            foreach (var node in openList)
                            {
                                if (node == neighbor && node.f <= neighbor.f) skip = true;
                            }
                            if (!skip) //if skip hasn't been set to true yet, do the second check, otherwise don't bother
                            {
                                foreach (var node in closedList)
                                {
                                    if (node == neighbor && node.f <= neighbor.f) skip = true;
                                }
                            }
                            if(skip) continue;

                            openList.Add(neighbor);
                        }
                    }
                    else
                    {
                        return GetPath(neighbor);
                    }
                }
                closedList.Add(q);
            }
            return null;
        }

        private static List<HexNode> GetPath(HexNode endNode)
        {
            List<HexNode> path = new List<HexNode>();
            HexNode node = endNode;
            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Reverse();
            return path;
        }

        /*
        initialize the open list
        initialize the closed list
        put the starting node on the open list (you can leave its f at zero)

            while the open list is not empty
        find the node with the least f on the open list, call it "q"
        pop q off the open list
        generate q's 8 successors and set their parents to q
        for each successor
        if successor is the goal, stop the search
        successor.g = q.g + distance between successor and q
            successor.h = distance from goal to successor
            successor.f = successor.g + successor.h

        if a node with the same position as successor is in the OPEN list \
        which has a lower f than successor, skip this successor
        if a node with the same position as successor is in the CLOSED list \
        which has a lower f than successor, skip this successor
                otherwise, add the node to the open list
            end
            push q on the closed list
        end
        */
    }
}