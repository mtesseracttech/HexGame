using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour
{

    public GameObject SelectedUnit;

    public TileType[] TileTypes;

    int[,] _tiles;
    Node[,] _graph;


   public int _mapSizeX = 40;
   public int _mapSizeY = 40;

    void Start()
    {
        // Setup the selectedUnit's variable
        SelectedUnit.GetComponent<Unit>().TileX = (int)SelectedUnit.transform.position.x;
        SelectedUnit.GetComponent<Unit>().TileY = (int)SelectedUnit.transform.position.y;
        SelectedUnit.GetComponent<Unit>().Map = this;

        GenerateMapData();
        GeneratePathfindingGraph();
        GenerateMapVisual();
    }

    void GenerateMapData()
    {
        // Allocate our map tiles
        _tiles = new int[_mapSizeX, _mapSizeY];

        int x, y;

        // Initialize our map tiles to be grass
        for (x = 0; x < _mapSizeX; x++)
        {
            for (y = 0; y < _mapSizeX; y++)
            {
                _tiles[x, y] = 0;
            }
        }

        // Make a big swamp area
        for (x = 3; x <= 10; x++)
        {
            for (y = 0; y < 8; y++)
            {
                _tiles[x, y] = 1;
            }
        }

        // Let's make a u-shaped mountain range
        _tiles[4, 4] = 2;
        _tiles[5, 4] = 2;
        _tiles[6, 4] = 2;
        _tiles[7, 4] = 2;
        _tiles[8, 4] = 2;

        _tiles[4, 5] = 2;
        _tiles[4, 6] = 2;
        _tiles[8, 5] = 2;
        _tiles[8, 6] = 2;

    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {

        TileType tt = TileTypes[_tiles[targetX, targetY]];

        if (UnitCanEnterTile(targetX, targetY) == false)
            return Mathf.Infinity;

        float cost = tt.MovementCost;

        if (sourceX != targetX && sourceY != targetY)
        {
            // We are moving diagonally!  Fudge the cost for tie-breaking
            // Purely a cosmetic thing!
            cost += 0.001f;
        }

        return cost;

    }

    void GeneratePathfindingGraph()
    {
        // Initialize the array
        _graph = new Node[_mapSizeX, _mapSizeY];

        // Initialize a Node for each spot in the array
        for (int x = 0; x < _mapSizeX; x++)
        {
            for (int y = 0; y < _mapSizeX; y++)
            {
                _graph[x, y] = new Node();
                _graph[x, y].X = x;
                _graph[x, y].Y = y;
            }
        }

        // Now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < _mapSizeX; x++)
        {
            for (int y = 0; y < _mapSizeX; y++)
            {
                // Try left
                if (x > 0)
                {
                    _graph[x, y].Neighbours.Add(_graph[x - 1, y]);
                    if (y > 0)
                        _graph[x, y].Neighbours.Add(_graph[x - 1, y - 1]);
                    if (y < _mapSizeY - 1)
                        _graph[x, y].Neighbours.Add(_graph[x - 1, y + 1]);
                }

                // Try Right
                if (x < _mapSizeX - 1)
                {
                    _graph[x, y].Neighbours.Add(_graph[x + 1, y]);
                    if (y > 0)
                        _graph[x, y].Neighbours.Add(_graph[x + 1, y - 1]);
                    if (y < _mapSizeY - 1)
                        _graph[x, y].Neighbours.Add(_graph[x + 1, y + 1]);
                }

                // Try straight up and down
                if (y > 0)
                    _graph[x, y].Neighbours.Add(_graph[x, y - 1]);
                if (y < _mapSizeY - 1)
                    _graph[x, y].Neighbours.Add(_graph[x, y + 1]);
            }
        }
    }

    void GenerateMapVisual()
    {
        for (int x = 0; x < _mapSizeX; x++)
        {
            for (int y = 0; y < _mapSizeX; y++)
            {
                TileType tt = TileTypes[_tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.TileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.TileX = x;
                ct.TileY = y;
                ct.Map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    public bool UnitCanEnterTile(int x, int y)
    {
        // terrain flags here to see if they are allowed to enter the tile.

        return TileTypes[_tiles[x, y]].IsWalkable;
    }

    public void GeneratePathTo(int x, int y)
    {
        // Clear out our unit's old path.
        SelectedUnit.GetComponent<Unit>().CurrentPath = null;

        if (UnitCanEnterTile(x, y) == false)
        {
            // We probably clicked on a mountain or something, so just quit out.
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // Setup the "Q" -- the list of nodes we haven't checked yet.
        List<Node> unvisited = new List<Node>();

        Node source = _graph[
                            SelectedUnit.GetComponent<Unit>().TileX,
                            SelectedUnit.GetComponent<Unit>().TileY
                            ];

        Node target = _graph[
                            x,
                            y
                            ];

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in _graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            // "u" is going to be the unvisited node with the smallest distance.
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;  // Exit the while loop!
            }

            unvisited.Remove(u);

            foreach (Node v in u.Neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(u.X, u.Y, v.X, v.Y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        // If we get there, the either we found the shortest route
        // to our target, or there is no route at ALL to our target.

        if (prev[target] == null)
        {
            // No route between our target and the source
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        // Step through the "prev" chain and add it to our path
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        // Right now, currentPath describes a route from out target to our source
        // So we need to invert it!

        currentPath.Reverse();

        SelectedUnit.GetComponent<Unit>().CurrentPath = currentPath;
    }

}
