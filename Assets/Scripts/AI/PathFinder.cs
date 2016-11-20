//using System.Collections.Generic;
//using System.Drawing;

/**
 * Implements a basic pathfinder for experimentation
 */
/*
public class PathFinder
{
    private Node _currentNode;

    private bool _done;
    private List<Node> _doneList;
    private Node _endNode;
    private List<Node> _lastPathFound;
    private Node _startNode;

    private List<Node> _todoList;

    public void SetStartNode(Node pStartNode)
    {
        if (_startNode != null) resetNode(_startNode);
        _startNode = pStartNode;
        resetPathFinder();
    }

    public void SetEndNode(Node pEndNode)
    {
        if (_endNode != null) resetNode(_endNode);
        _endNode = pEndNode;
        resetPathFinder();
    }

    public bool Step()
    {
        //return false;

        //are we able to find a path??
        if (_done || _startNode == null || _endNode == null || _todoList.Count == 0)
        {
            _done = true;
            return true;
        }

        //we are not done, start and end are set and there is at least 1 item on the open list...

        //check if we were already processing nodes, if so color the last processed node as black because it is on the closed list
        if (_currentNode != null)
        {
            _currentNode.SetColor(Color.DarkSlateGray);
        }

        //get a node from the open list
        _currentNode = _todoList[0];
        _todoList.RemoveAt(0);

        _currentNode.SetColor(Color.Orange);

        //and move that node to the closed list (one way or another, we are done with it...)
        _doneList.Add(_currentNode);
        _currentNode.info = "";

        //is this our node? yay done...
        if (_currentNode == _endNode)
        {
            generatePath();
            _done = true;
        }
        else
        {
            //get all children and process them
            for (var i = 0; i < _currentNode.GetConnectionCount(); i++)
            {
                var connectedNode = _currentNode.GetConnectionAt(i);


                if (_doneList.IndexOf(connectedNode) == -1 && _todoList.IndexOf(connectedNode) == -1)
                {
                    //connectedNode.parentNode = _currentNode;


                    if (connectedNode.costCurrent < _currentNode.costCurrent)
                    {
                        connectedNode.parentNode = _currentNode;
                    }
                    connectedNode.SetColor(Color.Blue);

                    connectedNode.costCurrent = _currentNode.costCurrent +
                                                _currentNode.position.DistanceTo(connectedNode.position);
                    connectedNode.costEstimate = connectedNode.position.DistanceTo(_endNode.position);


                    _todoList.Add(connectedNode);
                }
            }

            _todoList.Sort();
            updateNodeInfo();
        }

        return _done;
    }

    public bool IsDone()
    {
        return _done;
    }

    public List<Node> GetLastFoundPath()
    {
        return _lastPathFound;
    }

    private void updateNodeInfo()
    {
        //update the info for all open nodes
        for (var i = 0; i < _todoList.Count; i++)
        {
            var node = _todoList[i];

            var info = "";
            info += "IDX: " + i;
            info += "\nCST: " + node.costCurrent.ToString("#");
            info += "\nEST: " + node.costEstimate.ToString("#");
            info += "\nTOT: " + (node.costCurrent + node.costEstimate).ToString("#");

            node.info = info;
        }

        //show which one will be picked next frame
        if (_todoList.Count > 0) _todoList[0].SetColor(Color.CornflowerBlue);
    }

    private void generatePath()
    {
        _lastPathFound = new List<Node>();

        var node = _endNode;

        while (node != null)
        {
            node.SetColor(Color.Green);
            if (node.parentNode != null) node.parentLink.color = (uint) Color.Green.ToArgb();
            _lastPathFound.Add(node);

            node = node.parentNode;
        }

        _lastPathFound.Reverse();

        _startNode.SetColor(Color.DarkGreen);
        _endNode.SetColor(Color.DarkRed);
    }

    private void resetPathFinder()
    {
        if (_todoList != null) _todoList.ForEach(resetNode);
        if (_doneList != null) _doneList.ForEach(resetNode);

        _todoList = new List<Node>();
        _doneList = new List<Node>();
        _done = false;
        _lastPathFound = null;
        _currentNode = null;

        //setup for next path
        if (_startNode != null)
        {
            _todoList.Add(_startNode);
            _startNode.SetColor(Color.DarkGreen);
            _startNode.costCurrent = _startNode.costEstimate = 0;
        }

        if (_endNode != null)
        {
            _endNode.SetColor(Color.DarkRed);
        }
    }

    private void resetNode(Node pNode)
    {
        pNode.SetColor(Color.LightGray);
        pNode.info = "";
        if (pNode.parentNode != null)
        {
            pNode.parentLink.color = (uint) Color.LightGray.ToArgb();
            pNode.parentNode = null;
        }
    }
}
*/