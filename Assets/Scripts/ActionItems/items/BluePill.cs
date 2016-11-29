using Assets.Scripts.AI.Pathfinding;
using Assets.Scripts.NodeGrid.Occupants.Specifics;
using UnityEngine;

public class BluePill : PropOccupant
{

     public override void Start()
    {
        base.Start();
        Position = CurrentNode.Position;
    }


    public HexNode CurrentNode
    {
        get { return GetCurrentNode(); }
        set
        {
            SetCurrentNode(value);
            Position = CurrentNode.Position;
        }
    }

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public Quaternion Rotation
    {
        get { return transform.rotation; }
        set { transform.rotation = value; }
    }

    public string Name
    {
        get { return gameObject.name; }
        set { gameObject.name = value; }
    }
}
