using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

public class HealthKit : Items
{

    private HexNode _targetNode;
    

    public override void Start()
    {
        base.Start();
        Position = CurrentNode.Position;
    }

    public override void Interact()
    {
        Debug.Log("interacting with healthKIt");
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
}
