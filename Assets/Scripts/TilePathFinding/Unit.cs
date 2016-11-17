using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{

    // tileX and tileY represent the correct map-tile position
    public int TileX;
    public int TileY;

    public TileMap Map;

    // Our pathfinding info.  Null if we have no destination ordered.
    public List<Node> CurrentPath = null;

    // How far this unit can move in one turn. Note that some tiles cost extra.
    public int _moveSpeed = 2;
    public float _remainingMovement = 2;

    void Update()
    {
        // Draw our debug line showing the pathfinding!
        if (CurrentPath != null)
        {
            int currNode = 0;

            while (currNode < CurrentPath.Count - 1)
            {

                Vector3 start = Map.TileCoordToWorldCoord(CurrentPath[currNode].X, CurrentPath[currNode].Y) +
                    new Vector3(0, 0, -0.5f);
                Vector3 end = Map.TileCoordToWorldCoord(CurrentPath[currNode + 1].X, CurrentPath[currNode + 1].Y) +
                    new Vector3(0, 0, -0.5f);

                Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }

        // Have we moved our visible piece close enough to the target tile that we can
        if (Vector3.Distance(transform.position, Map.TileCoordToWorldCoord(TileX, TileY)) < 0.1f)
            AdvancePathing();

        // Smoothly animate towards the correct map tile.
        transform.position = Vector3.Lerp(transform.position, Map.TileCoordToWorldCoord(TileX, TileY), 5f * Time.deltaTime);
    }

    // Advances pathfinding progress by one tile.
    void AdvancePathing()
    {
        if (CurrentPath == null)
            return;

        if (_remainingMovement <= 0)
            return;

        // Teleport to our correct "current" position, in case we
        // haven't finished the animation yet.
        transform.position = Map.TileCoordToWorldCoord(TileX, TileY);

        // Get cost from current tile to next tile
        _remainingMovement -= Map.CostToEnterTile(CurrentPath[0].X, CurrentPath[0].Y, CurrentPath[1].X, CurrentPath[1].Y);

        // Move us to the next tile in the sequence
        TileX = CurrentPath[1].X;
        TileY = CurrentPath[1].Y;

        // Remove the old "current" tile from the pathfinding list
        CurrentPath.RemoveAt(0);

        if (CurrentPath.Count == 1)
        {
            CurrentPath = null;
        }
    }

    public void NextTurn()
    {
        while (CurrentPath != null && _remainingMovement > 0)
        {
            AdvancePathing();
        }

        // Reset our available movement points.
        _remainingMovement = _moveSpeed;
    }
}
